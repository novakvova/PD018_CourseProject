using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Interfaces;

namespace WebShop.Persistance.Identity {
    public class JwtService : IJwtService {
        private readonly IConfiguration _configuration;
        private readonly IDateTime _dateTime;

        public JwtService(IConfiguration configuration, IDateTime dateTime) {
            _configuration = configuration;
            _dateTime = dateTime;
        }

        public string GenerateToken(int userId, string email, string[] roles) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim("roles", role));
            }
            claims.Add(new Claim("roles", "dummy"));
            claims.Add(new Claim("user_id", userId.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, email));
            claims.Add(new Claim("email", email));

            var token = new JwtSecurityToken(
                _configuration.GetValue<string>("Jwt:Issuer"),
                _configuration.GetValue<string>("Jwt:Audience"),
                claims,
                expires: _dateTime.Now.AddHours(100),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
