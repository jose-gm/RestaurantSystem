using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryZona : RepositoryBase<Zona>, ISelectList
    {
        private readonly Contexto _contexto;

        public RepositoryZona(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Zona>> GetAllWithMesa()
        {
            List<Zona> lista = null;
            try
            {
                lista = await _contexto.Zona.Include(x => x.Mesas).ThenInclude(y => y.Cuentas).AsNoTracking().ToListAsync();
                lista.ForEach(a => a.Mesas.ForEach(b => b.Cuentas.RemoveAll(c => c.Activa == false)));
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }
 
        public async Task<SelectList> GetSelectList()
        {
            var list = await GetList(x => true);
            return new SelectList(list, "IdZona", "Descripcion");
        }

        public Task<SelectList> GetSelectList(object selected)
        {
            throw new NotImplementedException();
        }
    }
}
