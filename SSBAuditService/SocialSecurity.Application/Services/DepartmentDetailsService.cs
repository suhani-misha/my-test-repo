using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Application.UnitOfWorks;
using SocialSecurity.Domain.Models;
using SocialSecurity.Domain.Models.Common;
using SocialSecurity.Infrastructure.Integrations;
using SocialSecurity.Shared.Dtos.Common;
using SocialSecurity.Shared.Dtos.Department;

namespace SocialSecurity.Application.Services
{
    public class DepartmentDetailsService : IDepartmentDetailsService
    {
        private readonly IRepository<DepartmentDetail> _departmentDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserServiceClient _userServiceClient;
        private readonly IMapper _mapper;

        public DepartmentDetailsService(IRepository<DepartmentDetail> departmentDetailRepository, IUnitOfWork unitOfWork, IUserServiceClient userServiceClient, IMapper mapper)
        {
            _departmentDetailRepository = departmentDetailRepository;
            _unitOfWork = unitOfWork;
            _userServiceClient = userServiceClient;
            _mapper = mapper;
        }

        public async Task<Response> GetDepartmentStatsAsync()
        {
            var query = _departmentDetailRepository.GetQueryable();
            var stats = new
            {
                TotalDepartments = await query.Where(dd => !dd.IsDeleted).CountAsync(),
                HighRisk = await query.Where(dd => !dd.IsDeleted && dd.RiskRating == RiskRatingStatus.High.ToString()).CountAsync(),
                MediumRisk = await query.Where(dd => !dd.IsDeleted && dd.RiskRating == RiskRatingStatus.Medium.ToString()).CountAsync(),
                LowRisk = await query.Where(dd => !dd.IsDeleted && dd.RiskRating == RiskRatingStatus.Low.ToString()).CountAsync()
            };
            return new Response(ResponseSatusEnums.Success.ToString(), "Department statistics retrieved successfully", stats, 200);
        }
        public async Task<Response> AddDepartmentDetails(DepartmentDetailsDto departmentDetailsDto)
        {
            var department = await _userServiceClient.GetDepartmentAsync(departmentDetailsDto.DepartmentId);

            if (department == null)
            {
                return new Response(ResponseSatusEnums.Error.ToString(), "Department not found in User Service", 404);
            }

            var departmentDetail = _mapper.Map<DepartmentDetail>(departmentDetailsDto);

            await _departmentDetailRepository.AddAsync(departmentDetail);
            await _unitOfWork.SaveAsync();

            return new Response(ResponseSatusEnums.Success.ToString(), "Department details added successfully", 200);
        }

        public async Task<Response> UpdateDepartmentDetails(DepartmentDetailsDto departmentDetailsDto)
        {
            var existingDetail = await _departmentDetailRepository.GetFirstOrDefaultAsync(dd => dd.DepartmentId == departmentDetailsDto.DepartmentId && !dd.IsDeleted);
            if (existingDetail == null)
            {
                return new Response(ResponseSatusEnums.Error.ToString(), "Department details not found", 404);
            }
            _mapper.Map(departmentDetailsDto, existingDetail);
            _departmentDetailRepository.Update(existingDetail);
            await _unitOfWork.SaveAsync();
            return new Response(ResponseSatusEnums.Success.ToString(), "Department details updated successfully", 200);
        }

        public async Task<Response> GetDepartmentsAsync()
        {
            var departmentDetails = _departmentDetailRepository.GetAllNoTracking(dd => !dd.IsDeleted);
            var departmentDetailsDtos = _mapper.Map<List<DepartmentDetailsDto>>(departmentDetails);
            return new Response(ResponseSatusEnums.Success.ToString(), "Department details retrieved successfully", departmentDetailsDtos, 200);
        }

        public async Task<Response> GetDepartmentDetailsAsync(int departmentDetailId)
        {
            var departmentDetail = await _departmentDetailRepository.GetFirstOrDefaultAsync(dd => dd.Id == departmentDetailId && !dd.IsDeleted, includeProperties: "DepartmentFunctions");
            if (departmentDetail == null)
            {
                return new Response(ResponseSatusEnums.Error.ToString(), "Department details not found", 404);
            }
            var departmentDetailDto = _mapper.Map<DepartmentDetailsDto>(departmentDetail);
            return new Response(ResponseSatusEnums.Success.ToString(), "Department details retrieved successfully", departmentDetailDto, 200);
        }

        public async Task<Response> GetRiskAssessmentMatrixAsync()
        {
            var departmentDetails = _departmentDetailRepository.GetAllNoTracking(dd => !dd.IsDeleted);
            var riskMatrix = departmentDetails
                .GroupBy(dd => dd.RiskRating)
                .Select(g => new
                {
                    RiskRating = g.Key,
                    Count = g.Count()
                }).ToList();
            return new Response(ResponseSatusEnums.Success.ToString(), "Risk assessment matrix retrieved successfully", riskMatrix, 200);
        }

        public async Task<Response> GetFunctionsStatsAsync()
        {
            var departmentDetails = _departmentDetailRepository.GetAllNoTracking(dd => !dd.IsDeleted, includeProperties: "DepartmentFunctions");
            var functionStats = departmentDetails
                .SelectMany(dd => dd.DepartmentFunctions)
                .GroupBy(df => df.RiskRating)
                .Select(g => new
                {
                    RiskRating = g.Key,
                    Count = g.Count()
                }).ToList();
            return new Response(ResponseSatusEnums.Success.ToString(), "Department functions statistics retrieved successfully", functionStats, 200);
        }
        public async Task<Response> AddFunctionAsync(DepartmentFunctionDto functionDto)
        {
            var existingDetail = await _departmentDetailRepository.GetFirstOrDefaultAsync(dd => dd.DepartmentId == functionDto.DepartmentId && !dd.IsDeleted);
            if (existingDetail == null)
            {
                return new Response(ResponseSatusEnums.Error.ToString(), "Department details not found", 404);
            }
            var function = _mapper.Map<DepartmentFunction>(functionDto);
            existingDetail.DepartmentFunctions.Add(function);
            _departmentDetailRepository.Update(existingDetail);
            await _unitOfWork.SaveAsync();
            return new Response(ResponseSatusEnums.Success.ToString(), "Department function added successfully", 200);
        }

        public async Task<Response> UpdateFunctionAsync(DepartmentFunctionDto functionDto)
        {
            var existingDetail = await _departmentDetailRepository.GetFirstOrDefaultAsync(dd => dd.DepartmentId == functionDto.DepartmentId && !dd.IsDeleted, includeProperties: "DepartmentFunctions");
            if (existingDetail == null)
            {
                return new Response(ResponseSatusEnums.Error.ToString(), "Department details not found", 404);
            }
            var existingFunction = existingDetail.DepartmentFunctions.FirstOrDefault(df => df.Id == functionDto.Id && !df.IsDeleted);
            if (existingFunction == null)
            {
                return new Response(ResponseSatusEnums.Error.ToString(), "Department function not found", 404);
            }
            _mapper.Map(functionDto, existingFunction);
            _departmentDetailRepository.Update(existingDetail);
            await _unitOfWork.SaveAsync();
            return new Response(ResponseSatusEnums.Success.ToString(), "Department function updated successfully", 200);
        }

        public Response GetDepartmentFunctions()
        {
            var departmentFunctions = _departmentDetailRepository.GetAllNoTracking(dd => !dd.IsDeleted, includeProperties: "DepartmentFunctions")
                .SelectMany(dd => dd.DepartmentFunctions)
                .Where(df => !df.IsDeleted)
                .OrderByDescending(df => df.DepartmentDetailsId)
                .ToList();
            var functionDtos = _mapper.Map<List<DepartmentFunctionDto>>(departmentFunctions);
            return new Response(ResponseSatusEnums.Success.ToString(), "Department functions retrieved successfully", functionDtos, 200);
        }
    }
}
