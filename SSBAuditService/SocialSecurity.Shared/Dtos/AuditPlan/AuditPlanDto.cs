using SocialSecurity.Domain.Models;
using SocialSecurity.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurity.Shared.Dtos.AuditPlan
{
    public class AuditPlanCreateDto
    {
        public int Id { get; set; }
        public int FiscalYearId { get; set; }
        public string Title { get; set; }
        public string Objective { get; set; }
        public string Scope { get; set; }
        public string Methodology { get; set; }
        public string Status { get; set; }
    }

    //public class DepartmentAuditPlan : BaseModel
    //{
    //    [Required]
    //    public int DepartmentId { get; set; }
    //    public Department Department { get; set; }
    //    [Required]
    //    public int FiscalYearPeriodId { get; set; }
    //    public FiscalYearPeriod FiscalYearPeriod { get; set; }
    //    [Required]
    //    public int AuditPlanId { get; set; }
    //    public AuditPlan AuditPlan { get; set; }
    //    [Required]
    //    public DateTime StartDate { get; set; }
    //    [Required]
    //    public DateTime EndDate { get; set; }
    //    [Required]
    //    public string RiskRating { get; set; }
    //    [Required]
    //    public string Objective { get; set; }
    //    [Required]
    //    public string Scope { get; set; }
    //    [Required]
    //    public string Status { get; set; }
    //    public List<AuditTeamMember> TeamMembers { get; set; } = new List<AuditTeamMember>();

    //}

    //public class AuditTeamMember : BaseModel
    //{
    //    [Required]
    //    public int DepartmentAuditPlanId { get; set; }
    //    public DepartmentAuditPlan DepartmentAuditPlan { get; set; }
    //    [Required]
    //    public Guid UserId { get; set; }
    //    [Required]
    //    public bool IsTeamLead { get; set; }
    //}
}
