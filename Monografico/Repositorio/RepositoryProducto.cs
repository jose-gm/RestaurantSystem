using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryProducto : RepositoryBase<Producto>
    {
        private readonly Contexto _contexto;

        public RepositoryProducto(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<ProductoViewModel> BuscarProductoViewModel(int id)
        {
            var producto = await _contexto.Producto.FindAsync(id);
            return new ProductoViewModel() { 
                IdProducto = producto.IdProducto,
                IdCategoria = producto.IdCategoria,
                Descripcion = producto.Descripcion,
                ImagenEncoded = producto.Imagen,
                LlevaIngredientes = producto.LlevaIngredientes,
                TieneInventario = producto.LlevaInventario,
                Precio = producto.Precio
            };
        }

        public async Task<bool> EliminarConInventario(int id)
        {
            bool paso = false;
            try
            {
                var entity = _contexto.Producto.Include(x => x.Inventario).SingleOrDefault(c => c.IdProducto == id);
                _contexto.Producto.Remove(entity);

                await _contexto.SaveChangesAsync();
                paso = true;

                _contexto.Dispose();
            }
            catch (Exception)
            { throw; }
            return paso;
        }
    }
}
