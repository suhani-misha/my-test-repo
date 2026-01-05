using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurity.Infrastructure.Integrations
{
    /// <summary>
    /// Produces a signed JWT for service-to-service calls when no user token is present.
    /// NOTE: For production prefer client-credentials flow from a real auth server.
    /// </summary>
    public class ServiceTokenProvider
    {
        private readonly IConfiguration _configuration;

        public ServiceTokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateServiceToken(string subject = "internal-audit-service", int expireMinutes = 60)
        {
            var issuer = _configuration["Jwt:ValidIssuer"];
            var audience = _configuration["Jwt:ValidAudience"];
            var key = _configuration["Jwt:Secret"];

            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("Jwt:Key is not configured.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, subject),
                new Claim("scope", "service"),
                new Claim("svc", "internal-audit")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
