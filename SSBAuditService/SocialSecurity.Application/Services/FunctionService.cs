using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Application.UnitOfWorks;
using SocialSecurity.Domain.Models;
using SocialSecurity.Domain.Models.Common;
using SocialSecurity.Shared.Dtos.Common;
using SocialSecurity.Shared.Dtos.Department;
using SocialSecurity.Shared.Dtos.Function;

namespace SocialSecurity.Shared.Services
{
    public class FunctionService : IFunctionService
    {
        private readonly IRepository<Function> _functionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FunctionService(IRepository<Function> functionRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _functionRepository = functionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> CreateFunctionAsync(FunctionCreateDto dto)
        {
            var function = _mapper.Map<Function>(dto);
            await _functionRepository.AddAsync(function);
            await _unitOfWork.SaveAsync();
            return new Response("Success", "Function created successfully", _mapper.Map<FunctionDto>(function), 200);
        }

        public async Task<Response> UpdateFunctionAsync(int id, FunctionCreateDto dto)
        {
            var existing = await _functionRepository.GetAsync(id);
            if (existing == null) return new Response("Error", "Function not found", 404);

            _mapper.Map(dto, existing);
            _functionRepository.Update(existing);
            await _unitOfWork.SaveAsync();
            return new Response("Success", "Function updated successfully", _mapper.Map<FunctionDto>(existing), 200);
        }

        public async Task<Response> GetFunctionByIdAsync(int id)
        {
            var function = await _functionRepository.GetAsync(id);

            if (function == null) return new Response("Error", "Function not found", 404);
            return new Response("Success", "Function retrieved successfully", _mapper.Map<FunctionDto>(function), 200);
        }

        public async Task<Response> GetAllFunctionsAsync()
        {
            var functions = await _functionRepository.GetQueryable()
                .ToListAsync();

            var mapped = _mapper.Map<List<FunctionDto>>(functions);
            return new Response("Success", "Functions retrieved successfully", mapped, 200);
        }

        public async Task<Response> DeleteFunctionAsync(int id)
        {
            var function = await _functionRepository.GetAsync(id);
            if (function == null) return new Response("Error", "Function not found", 404);

            _functionRepository.Remove(function);
            await _unitOfWork.SaveAsync();
            return new Response("Success", "Function deleted successfully", 200);
        }

        public async Task<Response> GetFunctionRiskMatrixAsync()
        {
            var query = _functionRepository.GetQueryable(asNoTracking: true);
            var riskMatrix = await query
                .GroupBy(f => f.RiskRating)
                .Select(g => new
                {
                    RiskRating = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToListAsync();

            return new Response("Success", "Function risk assessment matrix retrieved successfully.", riskMatrix, 200);
        }

        public async Task<Response> GetFunctionStatsAsync()
        {
            var query = _functionRepository.GetQueryable(asNoTracking: true);

            var total = await query.CountAsync();
            var high = await query.Where(x => x.RiskRating == RiskRatingStatus.High).CountAsync();
            var medium = await query.Where(x => x.RiskRating == RiskRatingStatus.Medium).CountAsync();
            var low = await query.Where(x => x.RiskRating == RiskRatingStatus.Low).CountAsync();

            var stats = new FunctionStatsDto
            {
                TotalFunctions = total,
                HighRiskCount = high,
                MediumRiskCount = medium,
                LowRiskCount = low
            };

            return new Response
            {
                Code = 200,
                Status = "Success",
                Message = "Function statistics retrieved successfully.",
                Data = stats
            };
        }
    }
}