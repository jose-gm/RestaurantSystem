using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryRol : ISelectList
    {
        private readonly RoleManager<Rol> _roleManager;

        public RepositoryRol(RoleManager<Rol> roleManager)
        {
            _roleManager = roleManager;
        }

        public Task<SelectList> GetSelectList()
        {
            var list = _roleManager.Roles.ToList();

            return ToTask();
        }

        private async Task<SelectList> ToTask()
        {
            var list = _roleManager.Roles.ToList();
            return await Task.Delay(10).ContinueWith(x => new SelectList(list, "Name", "Name"));
        }

        public Task<SelectList> GetSelectList(object selected)
        {
            throw new NotImplementedException();
        }
    }
}
