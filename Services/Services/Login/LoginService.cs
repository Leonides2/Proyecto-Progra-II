using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Models.Custom;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proyecto_Progra_II.Services.Login
{

    public class LoginService : ILoginService
    {
        private readonly ApiContext _apiContext;

        public LoginService(ApiContext apiContext)
        {
            _apiContext = apiContext;

        }


        private string generateToken(string username, string role, string secret)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secret);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var credentialsToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30), // Token expiration time
                SigningCredentials = credentialsToken,
                Issuer = "yourIssuer",
                Audience = "yourAudience"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenMake = tokenHandler.WriteToken(tokenConfig);

            return tokenMake;
        }

        public async Task<UsuarioResponse> ReturnToken(UsuarioRequest request, string secret)
        {
            var user = _apiContext.Usuarios.FirstOrDefault(
                u => u.Email == request.Email && u.Password == request.Password
            );

            if (user == null)
            {
                return await Task.FromResult<UsuarioResponse>(null);
            }

            var role = _apiContext.Roles.FirstOrDefault(item => item.Id == user.IdRol);

            if (role == null)
            {
                return await Task.FromResult<UsuarioResponse>(null);
            }

            string tokenMake = generateToken(user.Email, role.NombreRol, secret);

            return new UsuarioResponse() { Token = tokenMake, Msg = "Ok", Status = "Success" };
        }
    }
}
