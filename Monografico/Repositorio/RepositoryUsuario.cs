using Microsoft.AspNetCore.Identity;
using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryUsuario
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Rol> _roleManager;

        public RepositoryUsuario(UserManager<Usuario> userManager, RoleManager<Rol> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> Create(UsuarioViewModel model)
        {
            var paso = false;
            try
            {
                var usuario = new Usuario()
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Sexo = model.Sexo,
                    Cedula = model.Cedula,
                    Direccion = model.Direccion,
                    Email = model.Email,
                    UserName = model.NombreUsuario
                };
                var result = await _userManager.CreateAsync(usuario, model.Clave);

                if (result.Succeeded)
                {
                    var existe = await _roleManager.RoleExistsAsync(model.Rol);
                    if (existe)
                    {
                        var resultRol = await _userManager.AddToRoleAsync(usuario, model.Rol);
                        if (resultRol.Succeeded)
                            paso = true;
                    }
                    
                }
                    
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }
        
        public async Task<Usuario> Find(int id)
        {
            Usuario usuario = null;
            try
            {
                usuario = await _userManager.FindByIdAsync(id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            return usuario;
        }
        
        public async Task<bool> Remove(int id)
        {
            var paso = false;
            try
            {
                var usuario = await _userManager.FindByIdAsync(id.ToString());
               
                var result = await _userManager.DeleteAsync(usuario);

                if (result.Succeeded)
                    paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        public async Task<List<Usuario>> GetList()
        {
            List<Usuario> list = null;
            try
            {
                list = _userManager.Users.ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
    }
}
