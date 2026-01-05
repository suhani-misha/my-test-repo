using Microsoft.AspNetCore.Mvc;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Shared.Dtos.Holiday;
using SocialSecurity.WebApi.Controllers;

namespace SocialSecurity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : BaseController
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        /// <summary>
        /// Get holiday statistics
        /// </summary>
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var response = await _holidayService.GetStatsAsync();
            return StatusCode(response.Code, response);
        }

        /// <summary>
        /// Create a new holiday
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateHoliday([FromBody] HolidayDto dto)
        {
            var response = await _holidayService.CreateHolidayAsync(dto);
            return StatusCode(response.Code, response);
        }

        /// <summary>
        /// Get all holidays
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetHolidays()
        {
            var response = await _holidayService.GetHolidaysAsync();
            return StatusCode(response.Code, response);
        }

        /// <summary>
        /// Get holiday by Id
        /// </summary>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetHolidayById(long id)
        {
            var response = await _holidayService.GetHolidayByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        /// <summary>
        /// Update holiday
        /// </summary>
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateHoliday(long id, [FromBody] HolidayDto dto)
        {
            dto.Id = id; // Ensure the ID from route is used
            var response = await _holidayService.UpdateHolidayAsync(dto);
            return StatusCode(response.Code, response);
        }

        /// <summary>
        /// Delete holiday by Id
        /// </summary>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteHoliday(long id)
        {
            var response = await _holidayService.DeleteHolidayAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
