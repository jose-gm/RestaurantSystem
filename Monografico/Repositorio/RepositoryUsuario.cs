using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Monografico.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Monografico.Repositorio
{
    public class RepositoryUsuario : ISelectList
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Rol> _roleManager;

        public RepositoryUsuario(UserManager<Usuario> userManager, RoleManager<Rol> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Create(UsuarioViewModel model)
        {
            IdentityResult result = null;
            try
            {
                var usuario = new Usuario()
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Sexo = model.Sexo,
                    Cedula = model.Cedula,
                    PhoneNumber = model.Telefono,
                    Direccion = model.Direccion,
                    Imagen = (model.Imagen != null) ? model.Imagen.ToBase64String() : null,
                    UserName = model.NombreUsuario
                };
                result = await _userManager.CreateAsync(usuario, model.Clave);

                if (result.Succeeded)
                {
                    if (await _roleManager.RoleExistsAsync(model.Rol))
                    {
                        await _userManager.AddToRoleAsync(usuario, model.Rol);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return result;
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
                usuario.PhoneNumber = model.Telefono;
                usuario.Imagen = (model.Imagen != null) ? model.Imagen.ToBase64String() : model.ImagenEncoded;
                usuario.Direccion = model.Direccion;
                usuario.UserName = model.NombreUsuario;

                var roles = await _userManager.GetRolesAsync(usuario);
                var rol = roles.FirstOrDefault();
                if (!rol.Equals(model.Rol))
                {
                    var roleResult = await _userManager.RemoveFromRoleAsync(usuario, rol);

                    if (roleResult.Succeeded)
                        await _userManager.AddToRoleAsync(usuario, model.Rol);
                }

                if (!string.IsNullOrEmpty(model.Clave))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    await _userManager.ResetPasswordAsync(usuario, token, model.Clave);
                }

                await _userManager.UpdateAsync(usuario);
                paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        public async Task<IdentityResult> UpdateUsuario(EditarPerfilViewModel model)
        {
            IdentityResult result = null;
            try
            {
                var usuario = await _userManager.FindByIdAsync(model.IdUsuario.ToString());
                usuario.Nombre = model.Nombre;
                usuario.Apellido = model.Apellido;
                usuario.Sexo = model.Sexo;
                usuario.Cedula = model.Cedula;
                usuario.PhoneNumber = model.Telefono;
                usuario.Imagen = (model.Imagen != null) ? model.Imagen.ToBase64String() : model.ImagenEncoded;
                usuario.Direccion = model.Direccion;
                usuario.UserName = model.NombreUsuario;

                result = await _userManager.UpdateAsync(usuario);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        
        public async Task<IdentityResult> ActualizarClave(CambiarClaveViewModel model, ClaimsPrincipal user)
        {
            IdentityResult result = null;
            try
            {
                var usuario = await GetUsuario(user);

                result = await _userManager.ChangePasswordAsync(usuario, model.Clave, model.Clavenueva);
              
            }
            catch (Exception)
            {

                throw;
            }
            return result;
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
        public async Task<Usuario> GetUsuario(ClaimsPrincipal claims)
        {
            Usuario usuario = null;
            try
            {
                usuario = await _userManager.GetUserAsync(claims);
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
                    Telefono = usuario.PhoneNumber,
                    Cedula = usuario.Cedula,
                    Direccion = usuario.Direccion,
                    ImagenEncoded = usuario.Imagen,
                    NombreUsuario = usuario.UserName,
                    UsuarioActual = usuario.UserName,
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
                usuario.Desactivado = true;
                var result = await _userManager.UpdateAsync(usuario);

                if (result.Succeeded)
                    paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }
        
        public async Task<bool> RemoveImage(ClaimsPrincipal claims)
        {
            var paso = false;
            try
            {
                var usuario = await _userManager.GetUserAsync(claims); ;
                usuario.Imagen = null;
                var result = await _userManager.UpdateAsync(usuario);

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
                var usurios = await _userManager.Users.Include(x => x.UsuarioRoles).ThenInclude(x => x.Role).Where(x => !x.Desactivado).AsNoTracking().ToListAsync();

                foreach (var item in usurios)
                {
                    list.Add(new UsuarioViewModel()
                    {
                        IdUsuario = item.Id,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        Sexo = item.Sexo,
                        Cedula = item.Cedula,
                        Direccion = item.Direccion,
                        ImagenEncoded = item.Imagen,
                        NombreUsuario = item.UserName,
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

        public async Task<bool> Exists(string nombreUsuario)
        {
            var paso = false;
            try
            {
                var usuario = await _userManager.FindByNameAsync(nombreUsuario);

                if (usuario != null)
                    paso = true;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public async Task<bool> IsAnAdminPassword(string password)
        {
            var paso = false;
            try
            {
                var usuarios = await _userManager.GetUsersInRoleAsync("Administrador");
                foreach (var usuario in usuarios)
                {
                    if (await _userManager.CheckPasswordAsync(usuario, password))
                    {
                        paso = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        public async Task<SelectList> GetSelectList()
        {
            var list = new List<Usuario>() { new Usuario() };
            list.AddRange(await _userManager.Users.Where(x => !x.Desactivado).ToListAsync());
            return new SelectList(list, "Id", "Nombre");
        }

        public Task<SelectList> GetSelectList(object selected)
        {
            throw new NotImplementedException();
        }
    }
}
