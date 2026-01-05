namespace SocialSecurity.Shared.Dtos.Common
{

    namespace SocialSecurity.Shared.Dtos.Common
    {
        public record ApiResponse<T>(
            int StatusCode,
            string Message,
            T? Data,
            bool IsSuccess
        );
    }

}
