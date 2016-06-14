using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Common.Utils;
using System.Net.Http;
using Arda.Common.ViewModels.Main;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class WorkloadController : Controller
    {
        [HttpGet]
        public async Task<JsonResult> ListWorkloadsByUser()
        {
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            var workloads = new List<string>();

            try
            {
                var existentWorkloads = await Util.ConnectToRemoteService<List<WorkloadsByUserViewModel>>(HttpMethod.Get, Util.KanbanURL + "api/workload/listworkloadbyuser", uniqueName, "");

                var dados = existentWorkloads.Select(x => new { data = x._WorkloadID, value = x._WorkloadTitle + " (Started in " + x._WorkloadStartDate.ToString("dd/MM/yyyy") + " and Ending in " + x._WorkloadEndDate.ToString("dd/MM/yyyy") + ")" })
                                             .Distinct()
                                             .ToList();

                return Json(dados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Add(ICollection<IFormFile> WBFiles, WorkloadViewModel workload)
        {
            //Owner:
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            //Complete WB fields:
            workload.WBCreatedBy = uniqueName;
            workload.WBCreatedDate = DateTime.Now;
            //Iterate over files:
            try
            {
                if (WBFiles.Count > 0)
                {
                    var fileList = new List<Tuple<Guid, string, string>>();
                    var Configuration = new ConfigurationBuilder().AddJsonFile("secrets.json").Build();
                    var connectionString = Configuration["Storage:AzureBLOB:ConnectionString"];
                    var containerName = Configuration["Storage:AzureBLOB:ContainerName"];
                    // Retrieve storage account information from connection string
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                    // Create a blob client for interacting with the blob service.
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    // Create a container for organizing blobs within the storage account.
                    CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                    foreach (var file in WBFiles)
                    {
                        if (file.Length > 0)
                        {
                            //Get file properties:
                            var filePath = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var fileName = new FileInfo(filePath).Name;
                            var fileExt = new FileInfo(filePath).Extension;
                            var fileID = Util.GenerateNewGuid();
                            var fileNameUpload = string.Concat(fileID, fileExt);
                            //Upload the file:
                            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileNameUpload);
                            await blockBlob.UploadFromFileAsync(filePath);
                            //Retrieve the url:
                            string fileURL = blockBlob.Uri.ToString();
                            //GUID, URL and Name:
                            fileList.Add(Tuple.Create(fileID, fileURL, fileName));
                        }
                    }
                    //Adds the file lists to the workload object:
                    workload.WBFilesList = fileList;
                }
                var response = await Util.ConnectToRemoteService(HttpMethod.Post, Util.KanbanURL + "api/workload/add", uniqueName, "", workload);

                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetWorkload(Guid workloadID)
        {
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            try
            {
                var workload = await Util.ConnectToRemoteService<WorkloadViewModel>(HttpMethod.Get, Util.KanbanURL + "api/workload/details?=" + workloadID, uniqueName, "");
                return Json(workload);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(Guid workloadID)
        {
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            try
            {
                await Util.ConnectToRemoteService(HttpMethod.Delete, Util.KanbanURL + "api/workload/delete?=" + workloadID, uniqueName, "");
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet]
        public JsonResult GetGuid()
        {
            return Json(Util.GenerateNewGuid());
        }
    }
}
