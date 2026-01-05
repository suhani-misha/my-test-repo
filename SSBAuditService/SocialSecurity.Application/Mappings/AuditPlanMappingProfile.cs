using AutoMapper;
using SocialSecurity.Domain.Models;
using SocialSecurity.Shared.Dtos.AuditPlan;

namespace SocialSecurity.Application.Mappings
{
    public class AuditPlanMappingProfile : Profile
    {
        public AuditPlanMappingProfile()
        {
            // Map DTO -> Entity
            CreateMap<AuditPlanCreateDto, AuditPlan>().ReverseMap();
        }
    }

}
