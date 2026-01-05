namespace SocialSecurity.Shared.Dtos.Settings
{
    /// <summary>
    /// Settings class for JWT configuration
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Gets or sets the secret key used for signing the JWT token
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the issuer of the JWT token
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the audience of the JWT token
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of days until the token expires
        /// </summary>
        public int ExpireDays { get; set; }
    }
} 