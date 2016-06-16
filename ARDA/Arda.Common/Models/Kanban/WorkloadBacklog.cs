using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Common.Models.Kanban
{
    [Table("WorkloadBacklogs")]
    public class WorkloadBacklog
    {
        //Entity Properties:
        [Key]
        [Required]
        public Guid WBID { get; set; }

        [Required]
        public bool WBIsWorkload { get; set; }

        [Required]
        public Status WBStatus { get; set; }

        [Required]
        public DateTime WBStartDate { get; set; }

        [Required]
        public DateTime WBEndDate { get; set; }

        [Required]
        public string WBTitle { get; set; }
        
        public string WBDescription { get; set; }

        [Required]
        public virtual Expertise WBExpertise { get; set; }

        [Required]
        public Complexity WBComplexity { get; set; }

        [Required]
        public string WBCreatedBy { get; set; }

        [Required]
        public DateTime WBCreatedDate { get; set; }


        //Foreign Keys:
        public virtual Activity WBActivity { get; set; }

        public virtual IEnumerable<File> WBFiles { get; set; }

        public virtual IEnumerable<WorkloadBacklogMetric> WBMetrics { get; set; }
        
        public virtual IEnumerable<WorkloadBacklogUser> WBUsers { get; set; }

        public virtual IEnumerable<WorkloadBacklogTechnology> WBTechnologies { get; set; }

        public virtual ICollection<Appointment> WBAppointments { get; set; }

    }

    public enum Status
    {
        To_Do,
        Doing,
        Done,
        Approved
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