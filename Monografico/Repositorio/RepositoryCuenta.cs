using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryCuenta : RepositoryBase<Cuenta>
    {
        private readonly Contexto _contexto;

        public RepositoryCuenta(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<Cuenta> Find(int idMesa, bool isActivo)
        {
            Cuenta cuenta = null;
            try
            {
                cuenta = _contexto.Cuenta.Include(x => x.Ordenes).AsNoTracking().SingleOrDefault(c => c.IdMesa == idMesa && c.Activa == isActivo);
            }
            catch (Exception)
            {

                throw;
            }
            return cuenta;
        }

        public override async Task<Cuenta> Find(int id)
        {
            Cuenta cuenta = null;
            try
            {
                cuenta = _contexto.Cuenta.Include(x => x.Ordenes).AsNoTracking().SingleOrDefault(c => c.IdCuenta == id);
            }
            catch (Exception)
            {

                throw;
            }
            return cuenta;
        }

        public async Task<bool> AddOrden(OrdenViewModel model)
        {
            bool paso = false;
            try
            {
                Orden orden = null;
                var cuenta = _contexto.Cuenta.Include(x => x.Ordenes).ThenInclude(y => y.Detalle).AsNoTracking().SingleOrDefault(z => z.IdCuenta == model.IdCuenta);
                if(cuenta.Ordenes.Count > 0 && cuenta.Ordenes.Find(z => z.Enviado == false) != null)
                {
                    orden = cuenta.Ordenes.Find(z => z.Enviado == false);
                }
                else
                {
                    orden = new Orden()
                    {
                        IdCuenta = model.IdCuenta,
                        Enviado = false,
                        Fecha = DateTime.Now.Date,
                        Detalle = new List<OrdenDetalle>(),
                    };
                }

                var detalle = orden.Detalle.Find(c => c.IdProducto == model.IdProducto);

                if (detalle == null)
                {
                    orden.Detalle.Add(new OrdenDetalle()
                    {
                        IdProducto = model.IdProducto,
                        Cantidad = model.Cantidad,
                    });

                    cuenta.Ordenes.Add(orden);
                }
                else                
                    detalle.Cantidad++;
                    
                

                _contexto.Cuenta.Update(cuenta);
                await _contexto.SaveChangesAsync();

                paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        public async Task<bool> EnviarOrdenes(int id)
        {
            var paso = false;
            try
            {
                Orden orden = null;
                var cuenta = _contexto.Cuenta.Include(x => x.Ordenes).ThenInclude(y => y.Detalle).AsNoTracking().SingleOrDefault(z => z.IdCuenta == id);
                orden = cuenta.Ordenes.Find(z => z.Enviado == false);
                orden.Enviado = true;

                _contexto.Cuenta.Update(cuenta);
                await _contexto.SaveChangesAsync();

                paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        public async Task<CuentaViewModel> FindCuentaViewModel(int idMesa, bool isActivo)
        {
            CuentaViewModel model = null;
            try
            {
                var cuenta =  _contexto.Cuenta.Include(x => x.Ordenes).AsNoTracking().SingleOrDefault(c => c.IdMesa == idMesa && c.Activa == isActivo);
                if(cuenta != null)
                {
                    model = new CuentaViewModel();
                    var categorias = await _contexto.Categoria.Include(w => w.Productos).AsNoTracking().ToListAsync();
                    var mesa = await _contexto.Mesa.FindAsync(idMesa);

                    model.IdCuenta = cuenta.IdCuenta;
                    model.IdMesa = cuenta.IdMesa;
                    model.Mesa = mesa.Descripcion;
                    //model.Ordenes = cuenta.ordenes;
                    model.Categorias = categorias;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public async Task<bool> RemoveAllOrdenes(int id)
        {
            var paso = false;
            try
            {
                var cuenta = _contexto.Cuenta.Include(x => x.Ordenes).ThenInclude(y => y.Detalle).AsNoTracking().SingleOrDefault(z => z.IdCuenta == id);

                foreach (var item in cuenta.Ordenes)
                    _contexto.Entry(item).State = EntityState.Deleted;

                _contexto.Cuenta.Update(cuenta);
                await _contexto.SaveChangesAsync();

                paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        /// <summary>
        /// Devuelve una lista con todas las ordenes de la cuenta pasada por parametro
        /// </summary>
        /// <param name="id">Id de la cuenta</param>
        /// <returns></returns>
        public async Task<List<OrdenViewModel>> GetListOrdenViewModels(int id, Func<Orden, bool> expression)
        {
            List<OrdenViewModel> lista = null;
            try
            {
                lista = new List<OrdenViewModel>();
                var cuenta = _contexto.Cuenta.Include(s => s.Mesa).Include(x => x.Ordenes).ThenInclude(y => y.Detalle).AsNoTracking().SingleOrDefault(c => c.IdCuenta == id);
                var ordenes = cuenta.Ordenes.Where(expression);
                foreach(var item in cuenta.Ordenes)
                {
                    foreach(var inner in item.Detalle)
                    {
                        var producto = await _contexto.Producto.FindAsync(inner.IdProducto);

                        lista.Add(new OrdenViewModel() { 
                            IdCuenta = cuenta.IdCuenta,
                            IdDetalle = inner.IdOrdenDetalle,
                            IdOrden = inner.IdOrden,
                            IdProducto = inner.IdProducto,
                            Cantidad = inner.Cantidad,
                            Descripcion = producto.Descripcion,
                            Precio = producto.Precio,
                            Total = inner.Cantidad * producto.Precio,
                            Enviado = item.Enviado,
                            Mesa = cuenta.Mesa.Descripcion
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

    }
}
