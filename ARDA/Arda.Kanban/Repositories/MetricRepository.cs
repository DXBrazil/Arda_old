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
                return false;
            }
        }

        public bool DeleteMetricByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool EditMetricByID(MetricMainViewModel metric)
        {
            throw new NotImplementedException();
        }

        public List<MetricMainViewModel> GetAllMetrics()
        {
            throw new NotImplementedException();
        }

        public Metric GetMetricByID(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
