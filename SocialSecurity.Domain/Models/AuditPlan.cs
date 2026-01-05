using SocialSecurity.Domain.Models;
using SocialSecurity.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace SocialSecurity.Domain.Models
{
    public class AuditPlan : BaseModel
    {
        public int FiscalYearId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string Objective { get; set; }
        [Required]
        public string Scope { get; set; }
        [Required]
        public string Methodology { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime? ApprovedDate { get; set; }
        // Navigation properties
        public FiscalYear FiscalYear { get; set; }
        public ICollection<DepartmentAuditPlan> DepartmentAuditPlans { get; set; } = new List<DepartmentAuditPlan>();
    }

    public class DepartmentAuditPlan : BaseModel
    {
        [Required]
        public Guid DepartmentId { get; set; }
        [Required]
        public int FiscalYearPeriodId { get; set; }
        [Required]
        public int AuditPlanId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string RiskRating { get; set; }
        [Required]
        public string Objective { get; set; }
        [Required]
        public string Scope { get; set; }
        [Required]
        public string Status { get; set; }
        // Navigation properties
        public FiscalYearPeriod FiscalYearPeriod { get; set; }
        public AuditPlan AuditPlan { get; set; }
        public ICollection<AuditTeamMember> AuditTeamMembers { get; set; } = new List<AuditTeamMember>();
    }

}

public class AuditTeamMember : BaseModel
{
    [Required]
    public int DepartmentAuditPlanId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public bool IsTeamLead { get; set; }
    // Navigation property
    public DepartmentAuditPlan DepartmentAuditPlan { get; set; }
}

