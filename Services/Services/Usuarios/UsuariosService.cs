using Microsoft.EntityFrameworkCore;
using Models.Models.Custom;
using Proyecto_Progra_II.Models;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Proyecto_Progra_II.Services.Usuarios
{
    public class UsuariosService : IUsuariosService
    {

        private readonly ApiContext _context;


        public UsuariosService(ApiContext context)
        {
            _context = context;
        }
    

        async public Task<Usuario> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return null;
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }


        async public Task<List<UsuarioListObject>> GetUsuarios()
        {
            var ListUsuarios =  _context.Usuarios.ToList();

            var List = ListUsuarios.Select(item => new UsuarioListObject
            {
                Id = item.Id,
                Name = item.Name,
                Email = item.Email,
                Telefono = item.Telefono,
            }).ToList();
      
            return List;
        }

        async public Task<Usuario> GetUsuarios(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);


            return usuario;
        }

         async public Task<Usuario> PostUsuario(Usuario usuario)
        {
  
            if (_context.Usuarios.Any( item => item.Email.Equals(usuario.Email)))
            {
                return null;
            }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        async public Task<Usuario> PutUsuario(int id, Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return usuario;
        }

        async public Task<Usuario> ReadUsuarioToken(string Jtoken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(Jtoken);
            var token = jsonToken as JwtSecurityToken;
            var userEmail = token.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)!.Value;

            var user =  await _context.Usuarios.FirstOrDefaultAsync(item => item.Email == userEmail);

            //var user = GetUsuarios(userId);

            return user;
        }
    }
}
