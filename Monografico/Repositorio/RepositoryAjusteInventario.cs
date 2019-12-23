using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryAjusteInventario : RepositoryBase<AjusteInventario>
    {
        private readonly Contexto _contexto;

        public RepositoryAjusteInventario(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        public async override Task<bool> Add(AjusteInventario entity)
        {
            bool paso = false;
            try
            {
                _contexto.Inventario.Find(entity.IdInventario).Cantidad = entity.CantidadActual;
                _contexto.Ajusteinventario.Add(entity);
                await _contexto.SaveChangesAsync();
                paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        public override Task<AjusteInventario> Find(int id)
        {
            return base.Find(id);
        }

        public override Task<bool> Remove(int id)
        {
            return base.Remove(id);
        }

        public override Task<bool> Update(AjusteInventario entity)
        {
            return base.Update(entity);
        }
    }
}
