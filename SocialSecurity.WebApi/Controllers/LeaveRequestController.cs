using Microsoft.AspNetCore.Mvc;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Shared.Dtos.Holiday;
using SocialSecurity.WebApi.Controllers;

namespace SocialSecurity.Api.Controllers
{
    public class LeaveRequestController : BaseController
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        /// <summary>
        /// Get leave request statistics
        /// </summary>
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var response = await _leaveRequestService.GetStatsAsync();
            return StatusCode(response.Code, response);
        }

        /// <summary>
        /// Create a new leave request
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestDto dto)
        {
            var response = await _leaveRequestService.CreateLeaveRequestAsync(dto);
            return StatusCode(response.Code, response);
        }

        /// <summary>
        /// Get all leave requests
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetLeaveRequests()
        {
            var response = await _leaveRequestService.GetLeaveRequestsAsync();
            return StatusCode(response.Code, response);
        }

        /// <summary>
        /// Get a specific leave request by ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLeaveRequestById(int id)
        {
            var response = await _leaveRequestService.GetLeaveRequestByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        /// <summary>
        /// Update an existing leave request
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateLeaveRequest(int id, [FromBody] LeaveRequestDto dto)
        {
            var response = await _leaveRequestService.UpdateLeaveRequestAsync(id, dto);
            return StatusCode(response.Code, response);
        }

        /// <summary>
        /// Delete a leave request by ID
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            var response = await _leaveRequestService.DeleteLeaveRequestAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpPost("decide-request/{id:int}")]
        public async Task<IActionResult> DecideLeaveRequest(int id, [FromQuery] string decision)
        {
            // Assuming decision is either "approve" or "reject"
            var response = await _leaveRequestService.DecideLeaveRequestAsync(id, decision);
            return StatusCode(response.Code, response);
        }
    }
}
