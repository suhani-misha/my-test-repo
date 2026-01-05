using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Shared.Dtos.Department;

namespace SocialSecurity.WebApi.Controllers
{
    public class DepartmentDetailsController : BaseController
    {
        private readonly IDepartmentDetailsService _departmentDetailsService;

        public DepartmentDetailsController(IDepartmentDetailsService departmentDetailsService)
        {
            _departmentDetailsService = departmentDetailsService;
        }

        [HttpGet("department/stats")]
        public async Task<IActionResult> GetStats()
        {
            var response = await _departmentDetailsService.GetDepartmentStatsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPost("department/add-department-detail")]
        public async Task<IActionResult> AddDepartmentDetails([FromBody] DepartmentDetailsDto departmentDetailsDto)
        {
            var response = await _departmentDetailsService.AddDepartmentDetails(departmentDetailsDto);
            return StatusCode(response.Code, response);
        }


        [HttpPut("department/update-department-detail")]
        public async Task<IActionResult> UpdateDepartmentDetails([FromBody] DepartmentDetailsDto departmentDetailsDto)
        {
            var response = await _departmentDetailsService.UpdateDepartmentDetails(departmentDetailsDto);
            return StatusCode(response.Code, response);
        }

        [HttpGet("department/get-departments")]
        public async Task<IActionResult> GetDepartmentsAsync()
        {
            var response = await _departmentDetailsService.GetDepartmentsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpGet("{departmentDetailId}")]
        public async Task<IActionResult> GetDepartmentDetails(int departmentDetailId)
        {
            var response = await _departmentDetailsService.GetDepartmentDetailsAsync(departmentDetailId);
            return StatusCode(response.Code, response);
        }
        [HttpGet("department/get-risk-assessment-matrix")]
        public async Task<IActionResult> GetRiskAssessmentMatrixAsync()
        {
            var response = await _departmentDetailsService.GetRiskAssessmentMatrixAsync();
            return StatusCode(response.Code, response);
        }

        [HttpGet("function/functions-stats")]
        public async Task<IActionResult> GetFunctionsStats()
        {
            var response = await _departmentDetailsService.GetFunctionsStatsAsync();
            return StatusCode(response.Code, response);
        }


        [HttpPost("function/add-function")]
        public async Task<IActionResult> AddFunction([FromBody] DepartmentFunctionDto functionDto)
        {
            var response = await _departmentDetailsService.AddFunctionAsync(functionDto);
            return StatusCode(response.Code, response);
        }

        [HttpGet("functions/update-function")]
        public async Task<IActionResult> UpdateFunctionAsync([FromBody] DepartmentFunctionDto DepartmentFunctionDto)
        {
            var response = await _departmentDetailsService.UpdateFunctionAsync(DepartmentFunctionDto);
            return StatusCode(response.Code, response);
        }

        [HttpGet("functions/Get-department-functions")]
        public IActionResult GetDepartmentFunctionsAsync()
        {
            var response = _departmentDetailsService.GetDepartmentFunctions();
            return StatusCode(response.Code, response);
        }
    }
}
