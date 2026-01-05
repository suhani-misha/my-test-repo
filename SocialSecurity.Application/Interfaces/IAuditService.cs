using SocialSecurity.Shared.Dtos.AuditPlan;
using SocialSecurity.Shared.Dtos.Common;

namespace SocialSecurity.Application.Interfaces
{
    public interface IAuditService
    {
        Task<Response> CreateAuditPlanAsync(AuditPlanCreateDto auditPlanCreateDto);
    }
}