using Arda.Common.Interfaces.Kanban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arda.Common.ViewModels.Reports;
using Arda.Kanban.Models;

namespace Arda.Kanban.Repositories
{
    public class ReportRepository : IReportRepository
    {
        KanbanContext _context;

        public ReportRepository(KanbanContext context)
        {
            _context = context;
        }

        public IEnumerable<ActivityConsumingViewModel> GetActivityConsumingData(DateTime startDate, DateTime endDate, string user = "All")
        {
            try
            {
                List<ActivityConsumingViewModel> activities;

                if (user == "All")
                {
                    activities = (from ap in _context.Appointments
                                  join w in _context.WorkloadBacklogs on ap.AppointmentWorkload.WBID equals w.WBID
                                  join a in _context.Activities on w.WBActivity.ActivityID equals a.ActivityID
                                  where w.WBStartDate >= startDate && w.WBEndDate <= endDate
                                  select new ActivityConsumingViewModel()
                                  {
                                      Activity = a.ActivityName,
                                      Hours = ap.AppointmentHoursDispensed
                                  }).ToList();
                }
                else
                {
                    activities = (from ap in _context.Appointments
                                  join w in _context.WorkloadBacklogs on ap.AppointmentWorkload.WBID equals w.WBID
                                  join a in _context.Activities on w.WBActivity.ActivityID equals a.ActivityID
                                  where w.WBStartDate >= startDate && w.WBEndDate <= endDate && ap.AppointmentUser.UniqueName == user
                                  select new ActivityConsumingViewModel()
                                  {
                                      Activity = a.ActivityName,
                                      Hours = ap.AppointmentHoursDispensed
                                  }).ToList();
                }
                var totalHours = (Convert.ToDecimal(activities.Sum(a => a.Hours))) / 100;

                var activityConsuming = activities
                     .GroupBy(a => a.Activity)
                     .Select(ac => new ActivityConsumingViewModel
                     {
                         Activity = ac.Key,
                         Hours = ac.Sum(a => a.Hours),
                         Percent = Math.Round(ac.Sum(a => a.Hours) / totalHours, 2)
                     })
                     .OrderByDescending(ac => ac.Hours)
                     .ToList();

                return activityConsuming;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ExpertiseConsumingViewModel> GetExpertiseConsumingData(DateTime startDate, DateTime endDate, string user = "All")
        {
            try
            {
                List<ExpertiseConsumingViewModel> expertises;

                if (user == "All")
                {
                    expertises = (from ap in _context.Appointments
                                  join w in _context.WorkloadBacklogs on ap.AppointmentWorkload.WBID equals w.WBID
                                  where w.WBStartDate >= startDate && w.WBEndDate <= endDate
                                  select new ExpertiseConsumingViewModel()
                                  {
                                      Expertise = w.WBExpertise.ToString(),
                                      Hours = ap.AppointmentHoursDispensed
                                  }).ToList();
                }
                else
                {
                    expertises = (from ap in _context.Appointments
                                  join w in _context.WorkloadBacklogs on ap.AppointmentWorkload.WBID equals w.WBID
                                  where w.WBStartDate >= startDate && w.WBEndDate <= endDate && ap.AppointmentUser.UniqueName == user
                                  select new ExpertiseConsumingViewModel()
                                  {
                                      Expertise = w.WBExpertise.ToString(),
                                      Hours = ap.AppointmentHoursDispensed
                                  }).ToList();
                }
                var totalHours = (Convert.ToDecimal(expertises.Sum(a => a.Hours))) / 100;

                var categoryConsuming = expertises
                     .GroupBy(c => c.Expertise)
                     .Select(cc => new ExpertiseConsumingViewModel
                     {
                         Expertise = cc.Key,
                         Hours = cc.Sum(a => a.Hours),
                         Percent = Math.Round(cc.Sum(a => a.Hours) / totalHours, 2)
                     })
                     .OrderByDescending(ac => ac.Hours)
                     .ToList();

                return categoryConsuming;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
