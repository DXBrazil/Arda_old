using Arda.Common.ViewModels;
using Arda.Kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Interfaces
{
    public interface IMetricRepository
    {
        // Add a new metric to the database.
        bool AddNewMetric(MetricMainViewModel metric);

        // Update some metric data based on id.
        bool EditMetricByID(MetricMainViewModel metric);

        // Return a list of metrics.
        List<MetricMainViewModel> GetAllMetrics();

        // Return a specific metric by ID.
        Metric GetMetricByID(Guid id);

        // Delete a metric based on ID
        bool DeleteMetricByID(Guid id);
    }
}
