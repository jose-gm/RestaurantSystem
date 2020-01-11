using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                var Inventario = await _contexto.Inventario.Include(x => x.Producto).Include(x => x.Ingrediente).AsNoTracking().SingleOrDefaultAsync(x => x.IdInventario == entity.IdInventario);
                switch (entity.Estado)
                {
                    case "Ajustar":
                        Inventario.Cantidad = entity.CantidadActual;
                        break;
                    case "Sumar":
                        Inventario.Cantidad += entity.CantidadActual;
                        break;
                    case "Restar":
                        Inventario.Cantidad -= entity.CantidadActual;
                        break;
                    default:
                        break;
                }
                _contexto.Inventario.Update(Inventario);
                entity.Descripcion = Inventario.Producto != null ? Inventario.Producto.Descripcion : Inventario.Ingrediente.Descripcion;
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

        public async override Task<bool> Remove(int id)
        {
            bool paso = false;
            try
            {
                var ajuste = await _contexto.Ajusteinventario.FindAsync(id);
                var inventario = await _contexto.Inventario.FindAsync(ajuste.IdInventario);
                inventario.Cantidad = ajuste.CantidadAnterior;
                _contexto.Inventario.Update(inventario);
                await base.Remove(id);
                await _contexto.SaveChangesAsync();
                paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;


        }

        public async override Task<bool> Update(AjusteInventario entity)
        {
            bool paso = true;
            try
            {
                var Inventario = _contexto.Inventario.Find(entity.IdInventario);
                Inventario.Cantidad = entity.CantidadActual;
                _contexto.Ajusteinventario.Update(entity);
                await _contexto.SaveChangesAsync();
                paso = true;

            }
            catch (Exception)
            {

                throw;
            }

            return paso;
        }

    }
}
