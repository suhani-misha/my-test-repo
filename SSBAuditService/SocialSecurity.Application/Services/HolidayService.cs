using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Application.UnitOfWorks;
using SocialSecurity.Domain.Models;
using SocialSecurity.Domain.Models.Common;
using SocialSecurity.Shared.Dtos.Common;
using SocialSecurity.Shared.Dtos.Holiday;

namespace SocialSecurity.Application.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IRepository<Holiday> _holidayRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HolidayService(IRepository<Holiday> holidayRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _holidayRepository = holidayRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This method retrieves statistics about holidays, including total public holidays, SSB holidays, and total holidays.
        /// </summary>
        public async Task<Response> GetStatsAsync()
        {
            var query = _holidayRepository.GetQueryable(asNoTracking: true);

            var publicHolidays = await query.Where(h => h.Type == HolidayTypeEnums.Public.ToString() && h.IsActive && !h.IsDeleted).CountAsync();
            var ssbHolidays = await query.Where(h => h.Type != HolidayTypeEnums.Public.ToString() && h.IsActive && !h.IsDeleted).CountAsync();
            var totalHolidays = await query.Where(h => h.IsActive && !h.IsDeleted).CountAsync();

            var stats = new
            {
                PublicHolidays = publicHolidays,
                SSBHolidays = ssbHolidays,
                TotalHolidays = totalHolidays
            };

            return new Response
            {
                Code = 200,
                Status = "Success",
                Message = "Holiday statistics retrieved successfully.",
                Data = stats
            };
        }

        /// <summary>
        /// Creates a new holiday entry in the database.
        /// </summary>
        public async Task<Response> CreateHolidayAsync(HolidayDto holidayDto)
        {
            var existingHoliday = await _holidayRepository.GetFirstOrDefaultAsync(
                h => h.Date.Date == holidayDto.Date.Date && h.Country == holidayDto.Country
            );

            if (existingHoliday != null)
            {
                return new Response
                {
                    Code = 400,
                    Status = "Error",
                    Message = $"A holiday already exists on {holidayDto.Date:yyyy-MM-dd} for {holidayDto.Country}."
                };
            }

            var holiday = _mapper.Map<Holiday>(holidayDto);
            var result = await _holidayRepository.AddAsync(holiday);
            await _unitOfWork.SaveAsync();

            var resultDto = _mapper.Map<HolidayDto>(result);
            return new Response
            {
                Code = 201,
                Status = "Success",
                Message = "Holiday created successfully.",
                Data = resultDto
            };
        }

        /// <summary>
        /// Retrieves all holiday entries from the database.
        /// </summary>
        public async Task<Response> GetHolidaysAsync()
        {
            var holidays = await _holidayRepository.GetAllAsync();
            var holidayDtos = _mapper.Map<IEnumerable<HolidayDto>>(holidays);

            return new Response
            {
                Code = 200,
                Status = "Success",
                Message = "Holidays retrieved successfully.",
                Data = holidayDtos
            };
        }

        /// <summary>
        /// Retrieves a holiday entry by its ID.
        /// </summary>
        public async Task<Response> GetHolidayByIdAsync(long id)
        {
            var holiday = await _holidayRepository.GetAsync((int)id);
            if (holiday == null)
            {
                return new Response
                {
                    Code = 404,
                    Status = "Error",
                    Message = "Holiday not found."
                };
            }

            var holidayDto = _mapper.Map<HolidayDto>(holiday);
            return new Response
            {
                Code = 200,
                Status = "Success",
                Message = "Holiday retrieved successfully.",
                Data = holidayDto
            };
        }

        /// <summary>
        /// Updates an existing holiday entry.
        /// </summary>
        public async Task<Response> UpdateHolidayAsync(HolidayDto holidayDto)
        {
            var existingHoliday = await _holidayRepository.GetAsync((int)holidayDto.Id);
            if (existingHoliday == null)
            {
                return new Response
                {
                    Code = 404,
                    Status = "Error",
                    Message = "Holiday not found."
                };
            }

            _mapper.Map(holidayDto, existingHoliday);
            existingHoliday.ModifiedOn = DateTime.UtcNow;

            _holidayRepository.Update(existingHoliday);
            _unitOfWork.Save();

            var updatedDto = _mapper.Map<HolidayDto>(existingHoliday);
            return new Response
            {
                Code = 200,
                Status = "Success",
                Message = "Holiday updated successfully.",
                Data = updatedDto
            };
        }

        /// <summary>
        /// Deletes a holiday entry by its ID.
        /// </summary>
        public async Task<Response> DeleteHolidayAsync(long id)
        {
            var holiday = await _holidayRepository.GetAsync((int)id);
            if (holiday == null)
            {
                return new Response
                {
                    Code = 404,
                    Status = "Error",
                    Message = "Holiday not found."
                };
            }

            _holidayRepository.Remove(holiday);
            await _unitOfWork.SaveAsync();

            return new Response
            {
                Code = 200,
                Status = "Success",
                Message = "Holiday deleted successfully."
            };
        }
    }
}
