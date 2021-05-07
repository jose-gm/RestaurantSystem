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
    public class RepositoryItbis : RepositoryBase<Itbis>, ISelectList
    {
        public RepositoryItbis(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<SelectList> GetSelectList()
        {
            var list = await GetList(x => true);
            return new SelectList(list, "IdItbis", "Valor");
        }

        public Task<SelectList> GetSelectList(object selected)
        {
            throw new NotImplementedException();
        }
    }
}
