using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        
        public async Task<bool> Update(UsuarioViewModel model)
        {
            var paso = false;
            try
            {
                var usuario = await _userManager.FindByIdAsync(model.IdUsuario.ToString());
                usuario.Nombre = model.Nombre;
                usuario.Apellido = model.Apellido;
                usuario.Sexo = model.Sexo;
                usuario.Cedula = model.Cedula;
                usuario.Direccion = model.Direccion;
                usuario.Email = model.Email;
                usuario.UserName = model.NombreUsuario;

                var roles = await _userManager.GetRolesAsync(usuario);
                var rol = roles.FirstOrDefault();
                if (!rol.Equals(model.Rol))
                {
                    var roleResult = await _userManager.RemoveFromRoleAsync(usuario, rol);

                    if (roleResult.Succeeded)
                        await _userManager.AddToRoleAsync(usuario,model.Rol);
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                await _userManager.ResetPasswordAsync(usuario, token, model.Clave);

                await _userManager.UpdateAsync(usuario);
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
        
        public async Task<UsuarioViewModel> FindAsViewModel(int id)
        {
            UsuarioViewModel model = null;
            try
            {
                var usuario = await _userManager.Users.Include(x => x.UsuarioRoles).ThenInclude(x => x.Role).AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
                model = new UsuarioViewModel()
                {
                    IdUsuario = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Sexo = usuario.Sexo,
                    Cedula = usuario.Cedula,
                    Direccion = usuario.Direccion,
                    NombreUsuario = usuario.UserName,
                    Email = usuario.Email,
                    Rol = usuario.UsuarioRoles.FirstOrDefault().Role.Name

                };
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }
        
        public async Task<bool> Remove(int id)
        {
            var paso = false;
            try
            {
                var usuario = await _userManager.Users.Include(x => x.UsuarioRoles).ThenInclude(x => x.Role).SingleOrDefaultAsync(x => x.Id == id);
                string role = usuario.UsuarioRoles.FirstOrDefault().Role.Name;
                var deleted = await _userManager.RemoveFromRoleAsync(usuario, role);

                if (deleted.Succeeded)
                {
                    var result = await _userManager.DeleteAsync(usuario);

                    if (result.Succeeded)
                        paso = true;
                }            
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
                list = await _userManager.Users.Include(x => x.UsuarioRoles).ThenInclude(x => x.Role).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
        
        public async Task<List<UsuarioViewModel>> GetListAsViewModel()
        {
            List<UsuarioViewModel> list = new List<UsuarioViewModel>();
            try
            {
                var usurios = await _userManager.Users.Include(x => x.UsuarioRoles).ThenInclude(x => x.Role).AsNoTracking().ToListAsync();

                foreach (var item in usurios)
                {
                    list.Add(new UsuarioViewModel() { 
                        IdUsuario = item.Id,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        Sexo = item.Sexo,
                        Cedula = item.Cedula,
                        Direccion = item.Direccion,
                        NombreUsuario = item.UserName,
                        Email = item.Email,
                        Rol = item.UsuarioRoles.FirstOrDefault().Role.Name
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
    }
}
