using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;

namespace Monografico.Repositorio
{
    public class RoleRepository 
    {
        private readonly Contexto _contexto;
        private readonly RoleManager<Rol> _roleManager;

        public RoleRepository(Contexto context, RoleManager<Rol> roleManager)
        {
            _contexto = context;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> CreateAsync(Rol rol)
        {
            var result = await _roleManager.CreateAsync(rol);
            return result;
        }

        public async Task<IdentityResult> CreateWithRoleViewModelAsync(RolesViewModel rol)
        {
            Rol rol1 = new Rol()
            {
                Name = rol.Nombre
            };
            var result = await _roleManager.CreateAsync(rol1);
            return result;
        }

        public async Task<IdentityResult> DeleteByIdAsync(int id)
        {
            var rol = await GetByIDAsync(id);
            var result = await _roleManager.DeleteAsync(rol);
            return result;
        }

        public async Task<Rol> GetByIDAsync(int? id)
        {
            var modelo = await _roleManager.FindByIdAsync(id.ToString());
            return modelo;
        }

        public async Task<IEnumerable<Rol>> GetRoleListAsync(string filtro = "")
        {
            var roles = await _roleManager.Roles
                .AsNoTracking()
                .AsQueryable()
                .Where(filtro)
                .ToListAsync();

            return roles;
        }

        public async Task<IEnumerable<RolesViewModel>> GetRoleViewModelListAsync(string filtro = "")
        {
            var lista = await(from m in _contexto.Rol
                               .Where(filtro)
                               .AsQueryable()
                               .AsNoTracking()
                              select new RolesViewModel
                              {
                                  Id = m.Id,
                                  Nombre = m.Name
                              }).ToListAsync();
            return lista;
        }

        public async Task<IdentityResult> UpdateAsync(Rol rol)
        {
            var uRole = await GetByIDAsync(rol.Id); //Si no se hace esto para actualizar, dice error de concurrencia, hay que buscar una mejor solucion.
            uRole.Name = rol.Name;
            var result = await _roleManager.UpdateAsync(uRole);
            return result;
        }

        public async Task<IdentityResult> UpdateWithRoleViewModelAsync(RolesViewModel rol)
        {
            var uRole = await GetByIDAsync(rol.Id);
            uRole.Name = rol.Nombre;
            var result = await _roleManager.UpdateAsync(uRole);
            return result;
        }
    }
}
