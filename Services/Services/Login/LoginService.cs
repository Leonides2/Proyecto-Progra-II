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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role) // Incluir el rol en los claims
            };

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
