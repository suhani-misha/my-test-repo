using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Domain.Models;
using SocialSecurity.Shared.Dtos.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialSecurity.Application.Services
{
    /// <summary>
    /// Implementation of JWT token service for handling token generation, validation, and management
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Initializes a new instance of the JwtService class
        /// </summary>
        /// <param name="configuration">The configuration containing JWT settings</param>
        /// <exception cref="InvalidOperationException">Thrown when required JWT configuration is missing</exception>
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _jwtSettings = GetJwtSettings();
        }

        private JwtSettings GetJwtSettings()
        {
            var key = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is not configured");
            var issuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer is not configured");
            var audience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience is not configured");
            var expireDays = int.Parse(_configuration["Jwt:ExpireDays"] ?? "7");

            return new JwtSettings
            {
                Key = key,
                Issuer = issuer,
                Audience = audience,
                ExpireDays = expireDays
            };
        }

        /// <summary>
        /// Validates a JWT token and returns the claims principal if valid
        /// </summary>
        /// <param name="token">The token to validate</param>
        /// <returns>The claims principal if the token is valid, null otherwise</returns>
        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the claims principal from an expired token
        /// </summary>
        /// <param name="token">The expired token</param>
        /// <returns>The claims principal if the token is valid but expired, null otherwise</returns>
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = false // Don't validate lifetime for expired tokens
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
} 