using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryInventario : RepositoryBase<Inventario>
    {
        public RepositoryInventario(Contexto contexto) : base(contexto)
        {
        }

        public async override Task<List<Inventario>> GetList(Expression<Func<Inventario, bool>> expression)
        {
            var a = await _contexto.Inventario.Include(x => x.Producto).Include(y => y.Ingrediente).ToListAsync();

            return a;
        }

        public async Task<List<InventarioViewModel>> GetListViewModel(Expression<Func<Inventario, bool>> expression)
        {
            List<InventarioViewModel> model = new List<InventarioViewModel>();


            var a = await _contexto.Inventario.Include(x => x.Producto).Include(y => y.Ingrediente).ToListAsync();

            foreach (var item in a)
            {
                model.Add(new InventarioViewModel()
                {
                    IdInventario = item.IdInventario,
                    Descripcion = (item.Producto == null) ? item.Ingrediente.Descripcion : item.Producto.Descripcion,
                    Cantidad = item.Cantidad,
                    FechaEntrada = item.FechaEntrada,
                    Unidad = item.Unidad,
                    IdProducto = item.IdProducto ?? default(int),
                    IdIngrediente = item.IdIngrediente ?? default(int)
                });
            }

            return model;
        }


        public async override Task<Inventario> Find(int id)
        {
            return await _contexto.Inventario.Include(x => x.Producto).Include(x => x.Ingrediente).AsNoTracking().SingleOrDefaultAsync(x => x.IdInventario == id);
        }

        public async Task<bool> RestarInventario(int id, int cantidad)
        {
            var paso = false;
            try
            {
                var inventario = await _contexto.Inventario.FindAsync(id);
                inventario.Cantidad -= cantidad;
                if (inventario.Cantidad <= 0)
                    inventario.Cantidad = 0;

                _contexto.Inventario.Update(inventario);
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
