using SocialSecurity.Shared.Dtos.Common;
using SocialSecurity.Shared.Dtos.FiscalYear;

namespace SocialSecurity.Application.Interfaces
{
    public interface IFiscalYearService
    {
        //Task<Response> GetAllFiscalYearsAsync();
        //Task<Response> GetFiscalYearByIdAsync(int id);
        Task<Response> CreateFiscalYearAsync(FiscalYearDto fiscalYearDto);
        Task<Response> GetAllFiscalYearsAsync();
        //Task<Response> UpdateFiscalYearAsync(int id, FiscalYearDto fiscalYearDto);
        //Task<Response> DeleteFiscalYearAsync(int id);
    }
}