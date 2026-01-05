using SocialSecurity.Domain.Models.Common;

namespace SocialSecurity.Shared.Dtos.Holiday;

public class LeaveRequestDto
{
    public string? RequestId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid UserId { get; set; }
    public string Reason { get; set; }
    public int Duaration { get; set; }
    public string Status { get; set; } = LeaveStatusEnum.Pending.ToString();
}
