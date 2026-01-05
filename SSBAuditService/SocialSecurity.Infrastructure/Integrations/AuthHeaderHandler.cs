using Microsoft.AspNetCore.Http;

namespace SocialSecurity.Infrastructure.Integrations
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ServiceTokenProvider _serviceTokenProvider;

        public AuthHeaderHandler(IHttpContextAccessor httpContextAccessor, ServiceTokenProvider serviceTokenProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceTokenProvider = serviceTokenProvider;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Prefer propagating incoming user token
            var incomingToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(incomingToken))
            {
                // If header exists, forward it (avoid duplicate)
                if (!request.Headers.Contains("Authorization"))
                    request.Headers.Add("Authorization", incomingToken);
            }
            else
            {
                // No incoming token, use service token (Bearer <token>)
                var serviceJwt = _serviceTokenProvider.GenerateServiceToken();
                if (!request.Headers.Contains("Authorization"))
                    request.Headers.Add("Authorization", $"Bearer {serviceJwt}");
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
