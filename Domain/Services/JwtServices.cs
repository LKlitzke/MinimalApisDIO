using Microsoft.IdentityModel.Tokens;
using MinimalApisDIO.Domain.Entities;
using MinimalApisDIO.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalApisDIO.Domain.Services
{
    public class JwtServices(IConfiguration configuration) : IJwtServices
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateJwtToken(Admin admin)
        {
            var key = _configuration.GetSection("Jwt:Key").Value ?? throw new ArgumentNullException("JWT Key is not configured in appsettings.json");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim("Profile", admin.Profile.ToString()),
                new Claim(ClaimTypes.Role, admin.Profile.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
