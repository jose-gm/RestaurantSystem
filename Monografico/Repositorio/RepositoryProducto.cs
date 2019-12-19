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

        public async Task<ProductoViewModel> FindProductoViewModel(int id)
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
        
        public async Task<ProductoDetalleViewModel> FindProductoDetalleViewModel(int id)
        {
            var producto =  _contexto.Producto.Include(w => w.Detalle).SingleOrDefault(q => q.IdProducto == id);
            var model = new ProductoDetalleViewModel();
    /*        if(producto.Detalle != null)
            {
                foreach (var item in producto.Detalle)
                {
                    model.Ingredientes.Add(_contexto.Ingrediente.Include(x => x.Inventario).SingleOrDefault(c => c.IdIngrediente == item.IdIngrediente));
                }
            }*/
            model.IdProducto = producto.IdProducto;

            return model;
        }

        public async Task<bool> RemoveWithInventario(int id)
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
        
        public async Task<bool> RemoveIngrediente(int id, int idDetalle)
        {
            bool paso = false;
            try
            {
                var producto = _contexto.Producto.Include(x => x.Detalle).AsNoTracking().SingleOrDefault(c => c.IdProducto == id);
                var detalle = producto.Detalle.Find(w => w.IdDetalle == idDetalle);
                producto.Detalle.Clear();
                producto.Detalle.Add(detalle);
                _contexto.Producto.Attach(producto);
                _contexto.Entry(producto.Detalle[0]).State = EntityState.Deleted;
                _contexto.Producto.Update(producto);

                await _contexto.SaveChangesAsync();
                paso = true;
            }
            catch (Exception)
            { throw; }
            return paso;
        }

        public async Task<bool> UpdateCantidadIngrediente(int id, int idDetalle, int cantidad)
        {
            bool paso = false;
            try
            {
                var producto = _contexto.Producto.Include(x => x.Detalle).AsNoTracking().SingleOrDefault(c => c.IdProducto == id);
                var detalle = producto.Detalle.Find(w => w.IdDetalle == idDetalle);
                detalle.Cantidad = cantidad;
                producto.Detalle.Clear();
                producto.Detalle.Add(detalle);
                _contexto.Producto.Attach(producto);
                _contexto.Entry(producto.Detalle[0]).State = EntityState.Modified;
                _contexto.Producto.Update(producto);

                await _contexto.SaveChangesAsync();
                paso = true;
            }
            catch (Exception)
            { throw; }
            return paso;
        }

        public async Task<List<ProductoDetalleViewModel>> GetProductoIngredientes(int id)
        {
            List<ProductoDetalleViewModel> lista = new List<ProductoDetalleViewModel>();
            try
            {
                var producto = _contexto.Producto.Include(w => w.Detalle).SingleOrDefault(q => q.IdProducto == id);
               
                foreach (var item in producto.Detalle)
                {
                    var ingrediente = _contexto.Ingrediente.Include(x => x.Inventario).SingleOrDefault(c => c.IdIngrediente == item.IdIngrediente);
                    lista.Add(new ProductoDetalleViewModel()
                    {
                        IdProducto = producto.IdProducto,
                        IdDetalle = item.IdDetalle,
                        IdIngrediente = item.IdIngrediente,
                        Cantidad = item.Cantidad,
                        Costo = ingrediente.Costo,
                        Descripcion = ingrediente.Descripcion,
                        Unidad = (ingrediente.Inventario != null) ? ingrediente.Inventario.Unidad : string.Empty
                    });
                }
                    
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        public async Task<bool> IsIngredienteAdded(int id, int idIngrediente)
        {
            bool paso = false;
            try
            {
                var producto = _contexto.Producto.Include(w => w.Detalle).AsNoTracking().SingleOrDefault(q => q.IdProducto == id);
                if (producto.Detalle.Find(x => x.IdIngrediente == idIngrediente) != null)
                    paso = true;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public async Task<List<ProductoViewModel>> GetAllWithCategoria()
        {
            List<ProductoViewModel> list = null;
            try
            {
                var productos = await _contexto.Producto.Include(x => x.Categoria).AsNoTracking().ToListAsync();
                list = new List<ProductoViewModel>();

                foreach (var item in productos)
                {
                    list.Add(new ProductoViewModel() { 
                        IdProducto = item.IdProducto,
                        IdCategoria = item.IdCategoria,
                        Descripcion = item.Descripcion,
                        Precio = item.Precio,
                        LlevaIngredientes = item.LlevaIngredientes,
                        LlevaInventario = item.LlevaInventario,
                        ImagenEncoded = item.Imagen,
                        Categoria = item.Categoria.Descripcion
                    });
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
    }
}
