using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class ApplicationUserRoleRepository : IApplicationUserRoleRepository
    {
        private readonly Contexto _context;
        public ApplicationUserRoleRepository(Contexto _repoContext)
        {
            _context = _repoContext;
        }

        public async Task EliminarUserFromRole(int userId, int rolId)
        {
            ApplicationUserRole ur = new ApplicationUserRole() { UserId = userId, RoleId = rolId };
            _context.Set<ApplicationUserRole>().Remove(ur);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarUserFromRoles(int userId, List<int> rolIds)
        {
            ApplicationUserRole[] entities = new ApplicationUserRole[rolIds.Count()];
            for (int i = 0; i < rolIds.Count(); i++)
                entities[i] = new ApplicationUserRole() { UserId = userId, RoleId = rolIds[i] };

            _context.Set<ApplicationUserRole>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<List<int>> GetUserRolListById(int userId)
        {
            var lista = await (from uRoles in _context.ApplicationUserRole.AsNoTracking().AsQueryable()
                              .Where(x => x.UserId == userId)
                               select uRoles.RoleId)
                              .ToListAsync();
            return lista;
        }
    }
}
