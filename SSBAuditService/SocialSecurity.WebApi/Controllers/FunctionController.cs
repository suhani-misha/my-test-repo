using Microsoft.AspNetCore.Mvc;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Shared.Dtos.Function;
using SocialSecurity.WebApi.Controllers;

public class FunctionController : BaseController
{
    private readonly IFunctionService _functionService;

    public FunctionController(IFunctionService functionService)
    {
        _functionService = functionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _functionService.GetAllFunctionsAsync();
        return StatusCode(response.Code, response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _functionService.GetFunctionByIdAsync(id);
        return StatusCode(response.Code, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FunctionCreateDto dto)
    {
        var response = await _functionService.CreateFunctionAsync(dto);
        return StatusCode(response.Code, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] FunctionCreateDto dto)
    {
        var response = await _functionService.UpdateFunctionAsync(id, dto);
        return StatusCode(response.Code, response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _functionService.DeleteFunctionAsync(id);
        return StatusCode(response.Code, response);
    }

    [HttpGet("risk-matrix")]
    public async Task<IActionResult> GetRiskMatrix()
    {
        var response = await _functionService.GetFunctionRiskMatrixAsync();
        return StatusCode(response.Code, response);
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var response = await _functionService.GetFunctionStatsAsync();
        return StatusCode(response.Code, response);
    }
}
