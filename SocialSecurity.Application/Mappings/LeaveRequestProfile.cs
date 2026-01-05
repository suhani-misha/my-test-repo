using AutoMapper;
using SocialSecurity.Domain.Models;
using SocialSecurity.Shared.Dtos.Holiday;

namespace SocialSecurity.Application.Mappings
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequestDto, LeaveRequest>().ReverseMap();
        }
    }

}
