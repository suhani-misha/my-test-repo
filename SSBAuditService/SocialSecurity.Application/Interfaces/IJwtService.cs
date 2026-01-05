using System.Security.Claims;
using SocialSecurity.Domain.Models;

namespace SocialSecurity.Application.Interfaces
{
    /// <summary>
    /// Interface for JWT token operations
    /// </summary>
    public interface IJwtService
    {

        /// <summary>
        /// Validates a JWT token and returns the claims principal if valid
        /// </summary>
        /// <param name="token">The token to validate</param>
        /// <returns>The claims principal if the token is valid, null otherwise</returns>
        ClaimsPrincipal? ValidateToken(string token);

        /// <summary>
        /// Gets the claims principal from an expired token
        /// </summary>
        /// <param name="token">The expired token</param>
        /// <returns>The claims principal if the token is valid but expired, null otherwise</returns>
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
} 