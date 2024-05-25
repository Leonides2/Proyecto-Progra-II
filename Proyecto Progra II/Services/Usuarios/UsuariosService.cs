﻿using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;

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

 

        async public Task<List<Usuario>> GetUsuarios()
        {
            var ListUsuarios = new List<Usuario>();
            ListUsuarios = await _context.Usuarios.ToListAsync();


            return ListUsuarios;
        }

        async public Task<Usuario> GetUsuarios(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);


            return usuario;
        }

         async public Task<Usuario> PostUsuario(Usuario usuario)
        {
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
    }
}
