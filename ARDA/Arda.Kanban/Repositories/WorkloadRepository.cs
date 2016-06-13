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
                    WBTitle = workload.WBTitle,
                    WBUsers = userList
                };

                _context.WorkloadBacklogs.Add(workloadToBeSaved);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteWorkloadByID(Guid id)
        {
            try
            {
                //Get files:
                var files =  _context.Files.Where(f => f.WorkloadBacklog.WBID == id);
                //Load Metrics:
                var metrics = _context.WorkloadBacklogMetrics.Where(wbm => wbm.WorkloadBacklog.WBID == id);
                //Load Technologies:
                var technologies = _context.WorkloadBacklogTechnologies.Where(wbt => wbt.WorkloadBacklog.WBID == id);
                //Load Users:
                var users = _context.WorkloadBacklogUsers.Where(wbu => wbu.WorkloadBacklog.WBID == id);
                //Get Workload:
                var workload = _context.WorkloadBacklogs.First(w => w.WBID == id);
                //Remove Everything:
                _context.Files.RemoveRange(files);
                _context.WorkloadBacklogMetrics.RemoveRange(metrics);
                _context.WorkloadBacklogTechnologies.RemoveRange(technologies);
                _context.WorkloadBacklogUsers.RemoveRange(users);
                _context.WorkloadBacklogs.Remove(workload);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
            try
            {
                if (id != null)
                {
                    //Load files:
                    var filesList = new List<Tuple<Guid, string, string>>();
                    foreach (var f in _context.Files.Where(f => f.WorkloadBacklog.WBID == id))
                    {
                        //GUID, URL and Name:
                        filesList.Add(new Tuple<Guid, string, string>(f.FileID, f.FileLink, f.FileName));
                    }
                    //Load Metrics:
                    var metricList = (from wbm in _context.WorkloadBacklogMetrics
                                      join m in _context.Metrics on wbm.Metric equals m
                                      where wbm.WorkloadBacklog.WBID == id
                                      select new Guid(wbm.Metric.MetricID.ToString())).ToList();
                    //Load Technologies:
                    var technologyList = (from wbt in _context.WorkloadBacklogTechnologies
                                          join t in _context.Technologies on wbt.Technology equals t
                                          where wbt.WorkloadBacklog.WBID == id
                                          select new Guid(wbt.Technology.TechnologyID.ToString())).ToList();
                    //Load Users:
                    var userList = (from wbu in _context.WorkloadBacklogUsers
                                    join u in _context.Users on wbu.User equals u
                                    where wbu.WorkloadBacklog.WBID == id
                                    select wbu.User.UniqueName).ToList();
                    //Load workload:
                    var workload = (from w in _context.WorkloadBacklogs
                                    join a in _context.Activities on w.WBActivity.ActivityID equals a.ActivityID
                                    where w.WBID == id
                                    select new WorkloadViewModel()
                                    {
                                        WBActivity = a.ActivityID,
                                        WBComplexity = (int)w.WBComplexity,
                                        WBCreatedBy = w.WBCreatedBy,
                                        WBCreatedDate = w.WBCreatedDate,
                                        WBDescription = w.WBDescription,
                                        WBEndDate = w.WBEndDate,
                                        WBExpertise = (int)w.WBExpertise,
                                        WBFilesList = filesList,
                                        WBID = w.WBID,
                                        WBIsWorkload = w.WBIsWorkload,
                                        WBMetrics = metricList,
                                        WBStartDate = w.WBStartDate,
                                        WBStatus = (int)w.WBStatus,
                                        WBTechnologies = technologyList,
                                        WBTitle = w.WBTitle,
                                        WBUsers = userList
                                    }).First();
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<WorkloadsByUserViewModel> GetWorkloadsByUser(string uniqueName)
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

    }
}
