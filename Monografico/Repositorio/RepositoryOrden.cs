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
    public class RepositoryOrden : RepositoryBase<Orden>
    {

        public RepositoryOrden(Contexto contexto) : base(contexto)
        {
        }

        public async Task<bool> RemoveOrden(int idOrden, int idDetalle)
        {
            var paso = false;
            try
            {
                var orden = _contexto.Orden.Include(x => x.Detalle).AsNoTracking().SingleOrDefault(z => z.IdOrden == idOrden);
                var detalle = orden.Detalle.Find(c => c.IdOrdenDetalle == idDetalle);
                orden.Detalle.Clear();
                orden.Detalle.Add(detalle);

                _contexto.Entry(orden.Detalle[0]).State = EntityState.Deleted;
                _contexto.Orden.Update(orden);
                await _contexto.SaveChangesAsync();

                paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        public async Task<bool> UpdateOrden(int idOrden, int idDetalle, int cantidad)
        {
            var paso = false;

            try
            {
                var orden = _contexto.Orden.Include(x => x.Detalle).AsNoTracking().SingleOrDefault(z => z.IdOrden == idOrden);
                var detalle = orden.Detalle.Find(c => c.IdOrdenDetalle == idDetalle);

                var diferencia = cantidad - detalle.Cantidad;
                detalle.Cantidad += diferencia;

                _contexto.Orden.Update(orden);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return paso;
        }
    }
}
