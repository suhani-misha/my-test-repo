using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Shared.Dtos.FiscalYear;

namespace SocialSecurity.WebApi.Controllers
{
    public class FiscalYearController : BaseController
    {
        private readonly IFiscalYearService _fiscalYearService;

        public FiscalYearController(IFiscalYearService fiscalYearService)
        {
            _fiscalYearService = fiscalYearService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFiscalYear([FromBody] FiscalYearDto fiscalYearDto)
        {
            var response = await _fiscalYearService.CreateFiscalYearAsync(fiscalYearDto);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFiscalYears()
        {
            var response = await _fiscalYearService.GetAllFiscalYearsAsync();
            return StatusCode(response.Code, response);
        }
    }
}
