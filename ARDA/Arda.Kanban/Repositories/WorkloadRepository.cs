using Arda.Common.ViewModels;
using Arda.Kanban.Interfaces;
using Arda.Common.Kanban.Models;
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

        public List<WorkloadsByUserMainViewModel> GetWorkloadsByUser(string uniqueName)
        {
            try
            {
                var response = (from wb in _context.WorkloadBacklogs
                                join wbu in _context.WorkloadBacklogUsers on wb.WBUsers.SingleOrDefault().WBUserID equals wbu.WBUserID
                                join uk in _context.UsersKanban on wbu.KanbanUser.UniqueName equals uk.UniqueName
                                where uk.UniqueName.Equals(uniqueName)
                                orderby wb.WBTitle
                                select new WorkloadsByUserMainViewModel
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
