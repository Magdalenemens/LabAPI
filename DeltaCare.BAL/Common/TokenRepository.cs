using DeltaCare.Entity.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeltaCare.BAL.Common
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _config;

        public TokenRepository(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// Generate JWT Token after successful login.
        /// </summary>
        /// <param name="userFLModel"></param>
        /// <returns></returns>
        public string GenerateToken(UserFLModel userFLModel)
        {
            // Create the security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // Create the signing credentials
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create the claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userFLModel.USER_FL_ID.ToString()),
                new Claim("userName", userFLModel.USER_NAME),
                new Claim("siteNo", userFLModel.DEF_SITE ?? string.Empty), // Handle potential null value
                new Claim("userId", userFLModel.USER_ID ?? string.Empty), // Handle potential null value
                new Claim("roleId", userFLModel.ROLE_ID.ToString() ), // Handle potential null value
                new Claim("roleName", userFLModel.ROLE_NAME.ToString() ), // Handle potential null value
                new Claim("sessionId", Guid.NewGuid().ToString()), // Handle potential null value
                new Claim(ClaimTypes.Role, userFLModel.ROLE_NAME.ToString())
             };

            // Parse the expiry configuration and set a default value if parsing fails
            double expiryInHours;
            if (!double.TryParse(_config["Jwt:ExpiryInHours"], out expiryInHours))
            {
                expiryInHours = 1; // Default to 1 hour if configuration is missing or invalid
            }

            // Create the token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expiryInHours), // Use UTC time
                audience: _config["Jwt:Audience"],
                issuer: _config["Jwt:Issuer"],
                signingCredentials: credentials
            );

            // Return the token string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public ClaimsPrincipal ValidateToken(string token)
        {
            IdentityModelEventSource.ShowPII = true;
            TokenValidationParameters validationParameters = new()
            {
                ValidIssuer = _config["Jwt:Audience"],
                ValidAudience = _config["Jwt:Isuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };

            var principal = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);

            return principal;
        }
    }
}
