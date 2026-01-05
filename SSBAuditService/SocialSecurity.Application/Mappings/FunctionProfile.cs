using AutoMapper;
using SocialSecurity.Domain.Models;
using SocialSecurity.Domain.Models.Common;
using SocialSecurity.Shared.Dtos.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurity.Application.Mappings
{
    public class FunctionProfile : Profile
    {
        public FunctionProfile()
        {
            CreateMap<Function, FunctionDto>()
                .ForMember(dest => dest.RiskRating, opt => opt.MapFrom(src => src.RiskRating.ToString()))
                .ForMember(dest => dest.Likelihood, opt => opt.MapFrom(src => src.Likelihood.ToString()))
                .ForMember(dest => dest.Impact, opt => opt.MapFrom(src => src.Impact.ToString()))
                .ForMember(dest => dest.Controls, opt => opt.MapFrom(src => src.Controls.ToString()));

            CreateMap<FunctionCreateDto, Function>()
                .ForMember(dest => dest.RiskRating, opt => opt.MapFrom(src => Enum.Parse<RiskRatingStatus>(src.RiskRating, true)))
                .ForMember(dest => dest.Likelihood, opt => opt.MapFrom(src => Enum.Parse<LikelihoodStatus>(src.Likelihood, true)))
                .ForMember(dest => dest.Impact, opt => opt.MapFrom(src => Enum.Parse<ImpactStatus>(src.Impact, true)))
                .ForMember(dest => dest.Controls, opt => opt.MapFrom(src => Enum.Parse<ControlEffectivenessStatus>(src.Controls, true)));
        }
    }

}
