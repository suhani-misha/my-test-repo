using SocialSecurity.Shared.Dtos.Common;
using SocialSecurity.Shared.Dtos.Holiday;

namespace SocialSecurity.Application.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<Response> GetStatsAsync();
        Task<Response> CreateLeaveRequestAsync(LeaveRequestDto leaveRequestDto);
        Task<Response> GetLeaveRequestsAsync();
        Task<Response> GetLeaveRequestByIdAsync(long id);
        Task<Response> UpdateLeaveRequestAsync(int id, LeaveRequestDto leaveRequestDto);
        Task<Response> DeleteLeaveRequestAsync(long id);
        Task<Response> DecideLeaveRequestAsync(int id, string decision);
    }

}