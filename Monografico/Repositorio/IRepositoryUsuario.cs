using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    interface IRepositoryUsuario
    {
            Task<IdentityResult> Create(Usuario user, string password);
            Task<IdentityResult> Create(Usuario user, string password, int roles);

            Task<IdentityResult> Update(UsuarioViewModel user, int roles);
            Task<IdentityResult> Update(Usuario user);

            Task<List<UsuarioViewModel>> GetUsuarioViewModelListAsync(string filtro);
            Task<UsuarioViewModel> GetUserInfoWithRoles(int? id);
            Task<Usuario> GetByIDAsync(int? id);

            UserManager<Usuario> UserManager { get; }

          //  Task<IdentityResult> GuardarPerfilUsuario(PerfilUsuarioViewModel modelo, IFormFile logo, string removeLogo);
        
    }
}
