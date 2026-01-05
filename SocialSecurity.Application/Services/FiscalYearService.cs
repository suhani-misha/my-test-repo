using AutoMapper;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Application.UnitOfWorks;
using SocialSecurity.Domain.Models;
using SocialSecurity.Domain.Models.Common;
using SocialSecurity.Shared.Dtos.Common;
using SocialSecurity.Shared.Dtos.FiscalYear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurity.Application.Services
{
    public class FiscalYearService : IFiscalYearService
    {
        private readonly IRepository<FiscalYear> _fiscalYearRepository;
        private readonly IRepository<FiscalYearPeriod> _fiscalYearPeriodRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FiscalYearService(IRepository<FiscalYear> fiscalYearRepository,
                                 IRepository<FiscalYearPeriod> fiscalYearPeriodRepository,
                                 IUnitOfWork unitOfWork,
                                 IMapper mapper)
        {
            _fiscalYearRepository = fiscalYearRepository;
            _fiscalYearPeriodRepository = fiscalYearPeriodRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> CreateFiscalYearAsync(FiscalYearDto fiscalYearDto)
        {
            await _unitOfWork.BeginTrans();
            try
            {
                // Step 1: Check if Fiscal Year already exists
                var existingFiscalYear = await _fiscalYearRepository.GetFirstOrDefaultAsync(
                    fy => fy.Name == fiscalYearDto.Name
                );
                if (existingFiscalYear != null)
                {
                    await _unitOfWork.RollBackTrans();
                    return new Response
                    {
                        Code = 400,
                        Status = "Error",
                        Message = $"Fiscal Year with name {fiscalYearDto.Name} already exists."
                    };
                }

                // Step 2: Create the Fiscal Year entity
                var fiscalYear = _mapper.Map<FiscalYear>(fiscalYearDto);
                fiscalYear.Periods = new List<FiscalYearPeriod>();

                var createdFiscalYear = await _fiscalYearRepository.AddAsync(fiscalYear);
                await _unitOfWork.SaveAsync();

                // Step 3: Create the Fiscal Year periods
                foreach (var periodName in fiscalYearDto.PeriodNames)
                {
                    var fiscalYearPeriod = new FiscalYearPeriod
                    {
                        FiscalYearId = createdFiscalYear.Id,
                        PeriodName = periodName,
                        StartDate = GetPeriodStartDate(periodName, fiscalYear.StartDate),
                        EndDate = GetPeriodEndDate(periodName, fiscalYear.StartDate)
                    };

                    await _fiscalYearPeriodRepository.AddAsync(fiscalYearPeriod);
                }

                // Step 4: Save all changes
                await _unitOfWork.SaveAsync();
                await _unitOfWork.Commit();

                return new Response
                {
                    Code = 201,
                    Status = "Success",
                    Message = "Fiscal Year created successfully.",
                    Data = createdFiscalYear
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackTrans();
                return new Response
                {
                    Code = 500,
                    Status = "Error",
                    Message = $"An error occurred while creating the fiscal year. Details: {ex.Message}"
                };
            }
        }
        public async Task<Response> GetAllFiscalYearsAsync()
        {
            var fiscalYears = await _fiscalYearRepository.GetAllAsync(includeProperties: "Periods");
            var dto = _mapper.Map<List<FiscalYearDto>>(fiscalYears);

            return new Response(
                ResponseSatusEnums.Success.ToString(),
                "Fiscal years retrieved successfully",
                dto,
                200
            );
        }

        private DateTime GetPeriodStartDate(string periodName, DateTime fiscalYearStartDate)
        {
            // Implement logic to calculate start date based on fiscal year and period
            // For example, for "Quarter 1", it would be fiscalYearStartDate itself
            // For "Quarter 2", it would be fiscalYearStartDate plus 3 months, etc.
            // This is just a basic example:

            switch (periodName)
            {
                case "Quarter 1":
                    return fiscalYearStartDate;
                case "Quarter 2":
                    return fiscalYearStartDate.AddMonths(3);
                case "Quarter 3":
                    return fiscalYearStartDate.AddMonths(6);
                case "Quarter 4":
                    return fiscalYearStartDate.AddMonths(9);
                default:
                    return fiscalYearStartDate; // Adjust for other period types like Monthly, etc.
            }
        }

        private DateTime GetPeriodEndDate(string periodName, DateTime fiscalYearStartDate)
        {
            // Implement logic to calculate the end date of the period
            // For example, for "Quarter 1", it would be the end of March, etc.

            switch (periodName)
            {
                case "Quarter 1":
                    return fiscalYearStartDate.AddMonths(3).AddDays(-1); // End of March
                case "Quarter 2":
                    return fiscalYearStartDate.AddMonths(6).AddDays(-1); // End of June
                case "Quarter 3":
                    return fiscalYearStartDate.AddMonths(9).AddDays(-1); // End of September
                case "Quarter 4":
                    return fiscalYearStartDate.AddMonths(12).AddDays(-1); // End of December
                default:
                    return fiscalYearStartDate.AddMonths(1).AddDays(-1); // End of each month for monthly periods
            }
        }
    }

}
