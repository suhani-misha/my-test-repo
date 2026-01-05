using AutoMapper;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Application.UnitOfWorks;
using SocialSecurity.Domain.Models;
using SocialSecurity.Domain.Models.Common;
using SocialSecurity.Infrastructure.Integrations;
using SocialSecurity.Shared.Dtos.AuditPlan;
using SocialSecurity.Shared.Dtos.Common;

namespace SocialSecurity.Application.Services
{
    public class AuditService : IAuditService
    {
        // Implementation of AuditPlanService methods would go here
        private readonly IRepository<AuditPlan> _auditPlanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserServiceClient _userServiceClient;
        private readonly IMapper _mapper;

        public AuditService(IRepository<AuditPlan> auditPlanRepository, IUnitOfWork unitOfWork, IUserServiceClient userServiceClient, IMapper mapper)
        {
            _auditPlanRepository = auditPlanRepository;
            _unitOfWork = unitOfWork;
            _userServiceClient = userServiceClient;
            _mapper = mapper;
        }

        public async Task<Response> CreateAuditPlanAsync(AuditPlanCreateDto auditPlanCreateDto)
        {
            var existingAuditPlan = await _auditPlanRepository.GetFirstOrDefaultAsync(
                filter: ap => ap.Status == AuditStatus.Approved.ToString() && ap.FiscalYearId == auditPlanCreateDto.FiscalYearId
            );

            if (existingAuditPlan != null)
            {
                return new Response
                {
                    Status = ResponseSatusEnums.Error.ToString(),
                    Message = "An approved audit plan for the specified fiscal year already exists.",
                    Code = 400
                };
            }

            var auditPlan = _mapper.Map<AuditPlan>(auditPlanCreateDto);

            await _auditPlanRepository.AddAsync(auditPlan);
            await _unitOfWork.SaveAsync();

            var auditPlanDto = _mapper.Map<AuditPlanCreateDto>(auditPlan);

            return new Response
            {
                Status = ResponseSatusEnums.Success.ToString(),
                Message = "Audit Plan created successfully",
                Data = auditPlanDto,
                Code = 200
            };
        }
    }
}
