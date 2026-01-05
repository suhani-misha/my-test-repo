using SocialSecurity.Shared.Dtos.Common.SocialSecurity.Shared.Dtos.Common;
using SocialSecurity.Shared.Dtos.UserService;
using System.Net.Http.Json;

namespace SocialSecurity.Infrastructure.Integrations
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;

        public UserServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto?> GetUserAsync(Guid userId)
        {
            var resp = await _httpClient.GetAsync($"user/{userId}");
            if (!resp.IsSuccessStatusCode) return null;

            var result = await resp.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();
            return result?.Data;
        }

        public async Task<DepartmentResponseData?> GetDepartmentAsync(Guid departmentId)
        {
            var resp = await _httpClient.GetAsync($"departments/{departmentId}");
            if (!resp.IsSuccessStatusCode) return null;

            var result = await resp.Content.ReadFromJsonAsync<ApiResponse<DepartmentResponseData>>();
            return result?.Data;
        }
    }
}
