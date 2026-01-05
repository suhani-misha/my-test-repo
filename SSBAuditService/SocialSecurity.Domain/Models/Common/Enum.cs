namespace SocialSecurity.Domain.Models.Common
{
    public enum StatusEnums
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3,
        Draft = 4,
        Published = 5,
        Improved = 6
    }

    public enum HolidayTypeEnums
    {
        Public = 1,
    }

    public enum ResponseSatusEnums
    {
        Success = 1,
        Error = 2
    }

    public enum LeaveReasonEnums
    {
        Annual = 1,
        Training = 2,
        Sick = 3,
        Maternity = 4,
        Paternity = 5,
        Compassionate = 6,
        Unpaid = 7
    }

    public enum LeaveStatusEnum
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        Cancelled = 4,
        Completed = 5
    }

    public enum RiskRatingStatus
    {
        Low,
        Medium,
        High,
        Critical
    }

    public enum LikelihoodStatus
    {
        Low,
        Medium,
        High
    }

    public enum ImpactStatus
    {
        Low,
        Medium,
        High
    }

    public enum ControlEffectivenessStatus
    {
        Effective,
        PartiallyEffective,
        Ineffective
    }

    public enum AuditStatus { Draft, Approved, InProgress, Completed, Submitted }
}
