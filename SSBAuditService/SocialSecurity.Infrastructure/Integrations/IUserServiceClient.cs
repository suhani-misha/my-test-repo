using SocialSecurity.Shared.Dtos.UserService;

namespace SocialSecurity.Infrastructure.Integrations
{
    public interface IUserServiceClient
    {
        Task<UserDto?> GetUserAsync(Guid userId);
        Task<DepartmentResponseData?> GetDepartmentAsync(Guid departmentId);
    }
}
