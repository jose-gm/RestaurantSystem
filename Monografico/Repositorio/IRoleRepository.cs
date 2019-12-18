using Microsoft.AspNetCore.Identity;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public interface IRoleRepository
    {
        Task<IdentityResult> CreateAsync(Rol rol);
        Task<IdentityResult> CreateWithRoleViewModelAsync(RolesViewModel rol);
        Task<IdentityResult> UpdateAsync(Rol rol);
        Task<IdentityResult> UpdateWithRoleViewModelAsync(RolesViewModel rol);
        Task<IdentityResult> DeleteByIdAsync(int id);
        Task<Rol> GetByIDAsync(int? id);
        Task<IEnumerable<Rol>> GetRoleListAsync(string filtro = "");
        Task<IEnumerable<RolesViewModel>> GetRoleViewModelListAsync(string filtro = "");
        Task<List<Generic>> GetGenericRoleListAsync();
    }
}
