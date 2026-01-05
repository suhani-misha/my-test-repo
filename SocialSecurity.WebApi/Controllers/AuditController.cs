using Microsoft.AspNetCore.Mvc;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Shared.Dtos.AuditPlan;

namespace SocialSecurity.WebApi.Controllers
{
    public class AuditController : BaseController
    {
        private readonly IAuditService _auditService;

        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpPost("create-audit-plan")]
        public async Task<IActionResult> CreateAuditPlan([FromBody] AuditPlanCreateDto auditPlanCreateDto)
        {
            var response = await _auditService.CreateAuditPlanAsync(auditPlanCreateDto);
            return StatusCode(response.Code, response);
        }
    }
}
