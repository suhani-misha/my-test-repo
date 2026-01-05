using SocialSecurity.Shared.Dtos.Common;
using SocialSecurity.Shared.Dtos.Function;

namespace SocialSecurity.Application.Interfaces
{
    public interface IFunctionService
    {
        Task<Response> CreateFunctionAsync(FunctionCreateDto dto);
        Task<Response> UpdateFunctionAsync(int id, FunctionCreateDto dto);
        Task<Response> GetFunctionByIdAsync(int id);
        Task<Response> GetAllFunctionsAsync();
        Task<Response> DeleteFunctionAsync(int id);
        Task<Response> GetFunctionRiskMatrixAsync();
        Task<Response> GetFunctionStatsAsync();
    }

}