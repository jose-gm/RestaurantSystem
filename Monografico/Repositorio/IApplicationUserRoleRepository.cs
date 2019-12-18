using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    interface IApplicationUserRoleRepository
    {
        Task<List<int>> GetUserRolListById(int userId);
        Task EliminarUserFromRoles(int userId, List<int> rolIds);
        Task EliminarUserFromRole(int userId, int rolId);
    }
}
