using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly Contexto _context;
        private readonly IApplicationUserRoleRepository _userRoleRepo;
        public RepositoryUsuario(Contexto context, UserManager<Usuario> userManager)
        {
            _userManager = userManager;
            _context = context;
            _userRoleRepo = new ApplicationUserRoleRepository(_context);
        }
        public UserManager<Usuario> UserManager
        {
            get
            {
                return _userManager;
            }

        }

        public async Task<IdentityResult> Create(Usuario user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> Create(Usuario user, string password, int roles)
        {
            var result = await Create(user, password);
            if (result.Succeeded)
            {
                List<ApplicationUserRole> userRole = new List<ApplicationUserRole>();
                
                    userRole.Add(new ApplicationUserRole { RoleId = roles, UserId = user.Id });
                
                await _context.AddRangeAsync(userRole);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Usuario> GetByIDAsync(int? id)
        {
            var modelo = await _userManager.FindByIdAsync(id.ToString());
            return modelo;
        }

        public async Task<UsuarioViewModel> GetUserInfoWithRoles(int? id)
        {
            var usuarioInfo = await(from user in _context.Usuario.Where(x => x.Id == id)
                                   .AsNoTracking()
                                    select new UsuarioViewModel
                                    {
                                        IdUsuarioViewModel = user.Id,
                                        NombreUsuario = user.UserName,
                                        Nombre = user.Nombre,
                                        Telefono = user.PhoneNumber,
                                        Email = user.Email,
                                        RoleName = string.Join(", ", (from userRole in user.UsuarioRoles
                                                                      join role in _context.Roles
                                                                      on userRole.RoleId
                                                                      equals role.Id
                                                                      select role.Name).ToList()),
                                        Roles = (from userRole in user.UsuarioRoles //[AspNetUserRoles]
                                                 join role in _context.Roles //[AspNetRoles]//
                                                 on userRole.RoleId
                                                 equals role.Id
                                                 select role.Id).FirstOrDefault()
                                    }).FirstOrDefaultAsync();

            return usuarioInfo;
        }

        public async Task<List<UsuarioViewModel>> GetUsuarioViewModelListAsync(string filtro)
        {
            var lista = await(from m in _context.Usuario
                                .Where(filtro)
                                .AsQueryable()
                                .AsNoTracking()
                                .Include(x => x.UsuarioRoles)
                              select new UsuarioViewModel
                              {
                                  IdUsuarioViewModel = m.Id,
                                  Nombre = m.Nombre,
                                  NombreUsuario = m.UserName,
                                  Telefono = m.PhoneNumber,
                                  Email = m.Email,
                                  RoleName = string.Join(", ", (from uRol in m.UsuarioRoles
                                                                join role in _context.Rol on uRol.RoleId equals role.Id
                                                                select role.Name).ToList())
                              }).ToListAsync();
            return lista;


        }

        public async Task<IdentityResult> Update(Usuario uUser)
        {
            _context.Entry(uUser).Property(c => c.SecurityStamp).IsModified = false;
            var result = await _userManager.UpdateAsync(uUser);
            return result;
        }

        public async Task<IdentityResult> Update(UsuarioViewModel user, int roles)
        {
            var uUser = await GetByIDAsync(user.IdUsuarioViewModel);
            _context.Entry(uUser).Property(c => c.SecurityStamp).IsModified = false;
            uUser.NormalizedEmail = user.Email.ToUpper();
            uUser.NormalizedUserName = user.NombreUsuario.ToUpper();
            uUser.PhoneNumber = user.Telefono;
            uUser.Nombre = user.Nombre;
            uUser.Email = user.Email;
            uUser.UserName = user.NombreUsuario;
            uUser.Cedula = user.Cedula;
            uUser.Sexo = user.Sexo;
            uUser.Apellido = user.Apellido;
            uUser.Direccion = user.Direccion;


            var result = await _userManager.UpdateAsync(uUser);
            if (result.Succeeded)
            {
                //busca todos los roles, los elimina y luego agrega nuevos
                await UpdateUserRoles(user.IdUsuarioViewModel, roles);

            }
            return result;
        }

        private async Task UpdateUserRoles(int Id, int nuevosRoles)
        {
            // Conseguir roles actuales para luego eliminarlos y agregar los nuevos usando nuestro
            // propio repositorio
            var rolesActuales = await _userRoleRepo.GetUserRolListById(Id);
            await _userRoleRepo.EliminarUserFromRoles(Id, rolesActuales);

            List<ApplicationUserRole> userRole = new List<ApplicationUserRole>();
            userRole.Add(new ApplicationUserRole { RoleId = nuevosRoles, UserId = Id });
            await _context.AddRangeAsync(userRole);
            await _context.SaveChangesAsync();
        }
    }
}
