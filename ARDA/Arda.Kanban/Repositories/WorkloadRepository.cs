using Arda.Common.ViewModels.Main;
using Arda.Common.Interfaces.Kanban;
using Arda.Kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arda.Common.Models.Kanban;

namespace Arda.Kanban.Repositories
{
    public class WorkloadRepository : IWorkloadRepository
    {
        KanbanContext _context;

        public WorkloadRepository(KanbanContext context)
        {
            _context = context;
        }


        public bool AddNewWorkload(WorkloadViewModel workload)
        {
            try
            {
                //Load Activity:
                var activity = _context.Activities.First(a => a.ActivityID == workload.WBActivity);
                //Load Metrics:
                var metricList = new List<WorkloadBacklogMetric>();
                foreach (var mId in workload.WBMetrics)
                {
                    var metric = _context.Metrics.First(m => m.MetricID == mId);
                    metricList.Add(new WorkloadBacklogMetric()
                    {
                        Metric = metric
                    });
                }
                //Load Technologies:
                var technologyList = new List<WorkloadBacklogTechnology>();
                foreach (var tId in workload.WBTechnologies)
                {
                    var technology = _context.Technologies.First(t => t.TechnologyID == tId);
                    technologyList.Add(new WorkloadBacklogTechnology()
                    {
                        Technology = technology
                    });
                }
                //Load Users:
                var userList = new List<WorkloadBacklogUser>();
                foreach (var uniqueName in workload.WBUsers)
                {
                    var user = _context.Users.First(u => u.UniqueName == uniqueName);
                    userList.Add(new WorkloadBacklogUser()
                    {
                        User = user
                    });
                }
                //Associate Files:
                var filesList = new List<File>();
                foreach (var f in workload.WBFilesList)
                {
                    filesList.Add(new File()
                    {
                        FileID = f.Item1,
                        FileLink = f.Item2,
                        FileName = f.Item3,
                        FileDescription = string.Empty,
                    });
                }
                //Create workload object:
                var workloadToBeSaved = new WorkloadBacklog()
                {
                    WBActivity = activity,
                    WBAppointments = null,
                    WBComplexity = (Complexity)workload.WBComplexity,
                    WBCreatedBy = workload.WBCreatedBy,
                    WBCreatedDate = workload.WBCreatedDate,
                    WBDescription = workload.WBDescription,
                    WBEndDate = workload.WBEndDate,
                    WBExpertise = (Expertise)workload.WBExpertise,
                    WBFiles = filesList,
                    WBID = workload.WBID,
                    WBIsWorkload = workload.WBIsWorkload,
                    WBMetrics = metricList,
                    WBStartDate = workload.WBStartDate,
                    WBStatus = (Status)workload.WBStatus,
                    WBTechnologies = technologyList,
                    WBTitle  = workload.WBTitle,
                    WBUsers = userList
                };

                _context.WorkloadBacklogs.Add(workloadToBeSaved);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        public bool DeleteWorkloadByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool EditWorkload(WorkloadViewModel workload)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkloadViewModel> GetAllWorkloads()
        {
            throw new NotImplementedException();
        }

        public WorkloadViewModel GetWorkloadByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<WorkloadsByUserViewModel> GetWorkloadsByUser(string uniqueName)
        {
            try
            {
                var response = (from wb in _context.WorkloadBacklogs
                                join wbu in _context.WorkloadBacklogUsers on wb.WBUsers.SingleOrDefault().WBUserID equals wbu.WBUserID
                                join uk in _context.Users on wbu.User.UniqueName equals uk.UniqueName
                                where uk.UniqueName.Equals(uniqueName)
                                orderby wb.WBTitle
                                select new WorkloadsByUserViewModel
                                {
                                    _WorkloadID = wb.WBID,
                                    _WorkloadTitle = wb.WBTitle,
                                    _WorkloadStartDate = wb.WBStartDate,
                                    _WorkloadEndDate = wb.WBEndDate
                                }).ToList();

                if (response != null)
                {
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        IEnumerable<WorkloadsByUserViewModel> IWorkloadRepository.GetWorkloadsByUser(string uniqueName)
        {
            throw new NotImplementedException();
        }
    }
}
