using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace MotorSolutionNet.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtService()
        {
            _secretKey = ConfigurationManager.AppSettings["JwtSecretKey"];
            _issuer = ConfigurationManager.AppSettings["JwtIssuer"];
            _audience = ConfigurationManager.AppSettings["JwtAudience"];
        }

        // Función para generar el token
        public string GenerateToken(int userId, string name, string rol)
        {
            var claims = new[]
            {
            new Claim("id", userId.ToString()), // Agregar el ID del usuario
            new Claim("name", name), // Agregar el nombre
            new Claim("rol", rol), // Agregar el rol
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(3), // Expiración del token (3 horas)
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Validar JWT
        public ClaimsPrincipal ValidateToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null; // Token inválido
            }
        }
    }
}