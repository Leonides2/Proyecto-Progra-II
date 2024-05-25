using Microsoft.IdentityModel.Tokens;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Models.Custom;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proyecto_Progra_II.Services.Login
{

    public class LoginService : ILoginService
    {
        private readonly ApiContext _apiContext;
        private readonly IConfiguration _configuration;

        public LoginService(ApiContext apiContext, IConfiguration configuration)
        {
            _apiContext = apiContext;
            _configuration = configuration;
        }


        private string generateToken(string id)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));

            var credentialsToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credentialsToken
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenMake = tokenHandler.WriteToken(tokenConfig);

            return tokenMake;
        }

        public async Task<UsuarioResponse> ReturnToken(UsuarioRequest request)
        {
            var User = _apiContext.Usuarios.FirstOrDefault(
                u => u.Email == request.Email && u.Password == request.Password
                );

            if (User == null)
            {
                return await Task.FromResult<UsuarioResponse>(null);
            }


            string tokenMake = generateToken(User.Id.ToString());

            return new UsuarioResponse() { Token = tokenMake, Msg = "Ok", Status = "" };
        }
    }
}
