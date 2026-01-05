using SocialSecurity.Shared.Dtos.Common;
using SocialSecurity.Shared.Dtos.Holiday;

namespace SocialSecurity.Application.Interfaces
{
    public interface IHolidayService
    {
        Task<Response> GetStatsAsync();
        Task<Response> CreateHolidayAsync(HolidayDto holidayDto);
        Task<Response> GetHolidaysAsync();
        Task<Response> GetHolidayByIdAsync(long id);
        Task<Response> UpdateHolidayAsync(HolidayDto holidayDto);
        Task<Response> DeleteHolidayAsync(long id);
    }
} 