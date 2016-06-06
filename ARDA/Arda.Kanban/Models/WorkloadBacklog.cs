using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Models
{
    public class WorkloadBacklog
    {
        //Entity Properties:

        public Guid WBID { get; set; }

        public DateTime WBStartDate { get; set; }

        public DateTime WBEndDate { get; set; }

        public string WBTitle { get; set; }

        public string WBDescription { get; set; }

        public virtual Expertise Expertise { get; set; }

        public IEnumerable<string> WBFilesLink { get; set; }

        public Complexity WBComplexity { get; set; }

        public string WBCreatedBy { get; set; }

        public DateTime WBCreatedDate { get; set; }


        //Foreign Keys:
        public virtual Activity WBActivity { get; set; }

        public virtual IEnumerable<WorkloadBacklogMetric> WBMetrics { get; set; }
        
        public virtual IEnumerable<WorkloadBacklogUser> WBUsers { get; set; }

        public virtual IEnumerable<WorkloadBacklogTechnology> WBTechnologies { get; set; }

    }

    public enum Expertise
    {
        Dev,
        Infra,
        DevOps
    }

    public enum Complexity
    {
        VeryLow,
        Low,
        Medium,
        Hard,
        VeryHard
    }
}