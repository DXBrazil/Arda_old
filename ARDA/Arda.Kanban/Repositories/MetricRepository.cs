using Arda.Kanban.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arda.Common.ViewModels;
using Arda.Kanban.Models;

namespace Arda.Kanban.Repositories
{
    public class MetricRepository : IMetricRepository
    {
        KanbanContext _context;

        public MetricRepository(KanbanContext context)
        {
            _context = context;
        }

        // Adds a new metric to the system.
        public bool AddNewMetric(MetricMainViewModel metric)
        {
            try
            {
                var metricToBeSaved = new Metric()
                {
                    MetricID = metric.MetricID,
                    MetricCategory = metric.MetricCategory,
                    MetricName = metric.MetricName,
                    Description = metric.Description,
                    FiscalYear = new FiscalYear()
                    {
                        FiscalYearID = metric.FiscalYearID,
                        FullNumericFiscalYear = metric.FullNumericFiscalYear,
                        TextualFiscalYear = metric.TextualFiscalYear
                    }
                };

                _context.Metrics.Add(metricToBeSaved);
                var response = _context.SaveChanges();

                if (response > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Return all metrics
        public List<MetricMainViewModel> GetAllMetrics()
        {
            try
            {
                var response = (from m in _context.Metrics
                                join f in _context.FiscalYears on m.FiscalYear.FiscalYearID equals f.FiscalYearID
                                orderby f.FullNumericFiscalYear, m.MetricCategory
                                select new MetricMainViewModel
                                {
                                    MetricID = m.MetricID,
                                    MetricCategory = m.MetricCategory,
                                    MetricName = m.MetricName,
                                    Description = m.Description,
                                    FiscalYearID = m.FiscalYear.FiscalYearID,
                                    FullNumericFiscalYear = m.FiscalYear.FullNumericFiscalYear,
                                    TextualFiscalYear = m.FiscalYear.TextualFiscalYear
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

        // Return metric based on ID
        public MetricMainViewModel GetMetricByID(Guid id)
        {
            try
            {
                var response = (from m in _context.Metrics
                                join fy in _context.FiscalYears on m.FiscalYear.FiscalYearID equals fy.FiscalYearID
                                where m.MetricID == id
                                select m).First();

                var metric = new MetricMainViewModel()
                {
                    MetricID = response.MetricID,
                    MetricCategory = response.MetricCategory,
                    MetricName = response.MetricName,
                    Description = response.Description,
                    FiscalYearID = response.FiscalYear.FiscalYearID,
                    FullNumericFiscalYear = response.FiscalYear.FullNumericFiscalYear,
                    TextualFiscalYear = response.FiscalYear.TextualFiscalYear
                };

                if (metric != null)
                {
                    return metric;
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

        // Update metric data based on ID
        public bool EditMetricByID(MetricMainViewModel metric)
        {
            try
            {
                var metricToBeUpdated = _context.Metrics.First(m => m.MetricID == metric.MetricID);

                if (metricToBeUpdated != null)
                {
                    // Update informations of object
                    metricToBeUpdated.MetricName = metric.MetricName;
                    metricToBeUpdated.MetricCategory = metric.MetricCategory;
                    metricToBeUpdated.Description = metric.Description;
                    metricToBeUpdated.FiscalYear.FiscalYearID = metric.FiscalYearID;

                    var response = _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Delete metric based on ID
        public bool DeleteMetricByID(Guid id)
        {
            try
            {
                var metricToBeDeleted = _context.Metrics.First(m => m.MetricID == id);

                if (metricToBeDeleted != null)
                {
                    var response = _context.Remove(metricToBeDeleted);
                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
