using Arda.Common.ViewModels.Main;
using Arda.Common.Interfaces.Kanban;
using Arda.Kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
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
