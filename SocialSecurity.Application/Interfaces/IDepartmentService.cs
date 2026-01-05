using SocialSecurity.Shared.Dtos.Common;
using SocialSecurity.Shared.Dtos.Department;

namespace SocialSecurity.Application.Interfaces
{
    public interface IDepartmentDetailsService
    {
        Task<Response> GetDepartmentStatsAsync();
        Task<Response> AddDepartmentDetails(DepartmentDetailsDto departmentDetailsDto);
        Task<Response> UpdateDepartmentDetails(DepartmentDetailsDto departmentDetailsDto);
        Task<Response> GetDepartmentsAsync();
        Task<Response> GetDepartmentDetailsAsync(int departmentDetailId);
        Task<Response> GetRiskAssessmentMatrixAsync();
        Task<Response> GetFunctionsStatsAsync();
        Task<Response> AddFunctionAsync(DepartmentFunctionDto functionDto);
        Task<Response> UpdateFunctionAsync(DepartmentFunctionDto functionDto);
        Response GetDepartmentFunctions();

    }
}