using EyeTrainer.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace EyeTrainer.Api.Handlers
{
    public interface ITokenGenerator
    {
        string GenerateToken(int userId, string role);
    }

    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly SecurityTokenHandler _securityTokenHandler;

        public TokenGenerator(IConfiguration configuration, SecurityTokenHandler securityTokenHandler)
        {
            _configuration = configuration;
            _securityTokenHandler = securityTokenHandler;
        }
        public string GenerateToken(int userId, string role)
        {
            var secretKey = _configuration.GetSection("Authentication:TokenSecret").Value;
            var secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);

            const int defaultExpiryMinutes = 60;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("UserId", userId.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(defaultExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                Audience = "EyeTrainer",
                Issuer = "EyeTrainer"
            };

            var token = _securityTokenHandler.CreateToken(tokenDescriptor);

            var tokenString = _securityTokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
