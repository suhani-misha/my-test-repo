using AutoMapper;
using SocialSecurity.Domain.Models;
using SocialSecurity.Domain.Models.Common;
using SocialSecurity.Shared.Dtos.Department;
using SocialSecurity.Shared.Dtos.Holiday;

namespace SocialSecurity.Application.Mappings
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map DTO -> Entity
            CreateMap<HolidayDto, Holiday>();

            // Map Entity -> DTO
            CreateMap<Holiday, HolidayDto>();
        }
    }

    public class DepartmentDetailMappingProfile : Profile
    {
        public DepartmentDetailMappingProfile()
        {
            // Map DTO -> Entity
            CreateMap<DepartmentDetailsDto, DepartmentDetail>();
            // Map Entity -> DTO
            CreateMap<DepartmentDetail, DepartmentDetailsDto>();
        }
    }

    public class DepartmentFunctionMappingProfile : Profile
    {
        public DepartmentFunctionMappingProfile()
        {
             CreateMap<DepartmentFunctionDto, DepartmentFunction>();
                CreateMap<DepartmentFunction, DepartmentFunctionDto>();
        }
    }

}
