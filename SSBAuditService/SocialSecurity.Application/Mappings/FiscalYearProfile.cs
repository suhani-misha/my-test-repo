using AutoMapper;
using SocialSecurity.Domain.Models;
using SocialSecurity.Shared.Dtos.FiscalYear;

namespace SocialSecurity.Application.Mappings
{
    public class FiscalYearProfile : Profile
    {
        public FiscalYearProfile()
        {
            CreateMap<FiscalYearDto, FiscalYear>().ReverseMap();
        }
    }
}
