using BarProductBackend.Services.Interfaces;
using BarProductBackend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarProductBackend.Services
{
    public class TokenServicio : ITokenServicio
    {
        private readonly IConfiguration _config;

        public TokenServicio(IConfiguration config)
        {
            _config = config;
        }

        // Devuelve el token generado y su fecha de expiración como tupla
        public (string Token, DateTime Expira) Generar(ApplicationUser usuario, IList<string> roles)
        {
            // Datos de identidad que viajan dentro del token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id),
                new Claim(ClaimTypes.Name, usuario.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Incluye los roles del usuario para autorización
            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            // Clave secreta y algoritmo para firmar el token y evitar alteraciones
            var clave = _config["Jwt:Key"] ?? throw new InvalidOperationException("Falta configurar Jwt:Key.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expira = DateTime.UtcNow.AddHours(1); // Tiempo de vida del token

            // Construye el JWT con sus metadatos, claims, expiración y firma
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expira,
                signingCredentials: credenciales
            );

            // Serializa el token a string (header.payload.signature) y lo retorna junto con la expiración
            return (new JwtSecurityTokenHandler().WriteToken(token), expira);
        }
    }
}