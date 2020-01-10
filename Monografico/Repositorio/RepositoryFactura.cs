using Microsoft.AspNetCore.Identity;
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
    public class RepositoryFactura : RepositoryBase<Factura>
    {

        public RepositoryFactura(Contexto contexto): base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Create(FacturaViewModel model)
        {
            var paso = false;
            try
            {
                var factura = new Factura()
                {
                    IdCuenta = model.IdCuenta,
                    Fecha = DateTime.Now.Date,
                    Descuento = (model.Descuento/100)*model.Monto,
                    Monto = model.Monto - ((model.Descuento / 100)*model.Monto),
                    Estado = "No pago",
                    Detalle = new List<FacturaDetalle>()
                };

                foreach (var item in model.Ordenes)
                    factura.Detalle.Add(new FacturaDetalle() { 
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio
                    });

                _contexto.Factura.Add(factura);
                await _contexto.SaveChangesAsync();

                paso = true;
            }
            catch (Exception)
            {

                throw;
            }

            return paso;
        }

        public async Task<FacturaViewModel> Find(int idCuenta, bool no)
        {
            FacturaViewModel model = null;
            try
            {
                var factura = await _contexto.Factura.Include(x => x.Detalle).Include(s => s.Cuenta).ThenInclude(c => c.Mesa).AsNoTracking().SingleOrDefaultAsync(w => w.IdCuenta == idCuenta);

                model = new FacturaViewModel()
                {
                    IdFactura = factura.IdFactura,
                    IdCuenta = factura.IdCuenta ?? default(int),
                    IdMesa = factura.Cuenta.IdMesa,
                    Monto = factura.Monto,
                    Mesa = factura.Cuenta.Mesa.Numero,
                    Descuento = factura.Descuento,
                    Ordenes = new List<OrdenViewModel>()
                };

                foreach(var item in factura.Detalle)
                {
                    var producto = await _contexto.Producto.FindAsync(item.IdProducto);

                    model.Ordenes.Add(new OrdenViewModel() { 
                        IdProducto = item.IdProducto,
                        IdCuenta = factura.IdCuenta ?? default(int),
                        Precio = item.Precio,
                        Descripcion = producto.Descripcion,
                        Cantidad = item.Cantidad,
                        Total = item.Cantidad * item.Precio
                    });
                }

            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }
        
        public async Task<FacturaViewModel> FindAsViewModel(int id)
        {
            FacturaViewModel model = null;
            try
            {
                var factura = await _contexto.Factura.Include(x => x.Detalle).Include(s => s.Cuenta).ThenInclude(c => c.Mesa).AsNoTracking().SingleOrDefaultAsync(w => w.IdFactura == id);
                var usuario = await _contexto.Usuario.FindAsync(factura.Cuenta.IdUsuario);

                model = new FacturaViewModel()
                {
                    IdFactura = factura.IdFactura,
                    IdCuenta = factura.IdCuenta ?? default(int),
                    IdMesa = factura.Cuenta.IdMesa,
                    Fecha = factura.Fecha,
                    Estado = factura.Estado,
                    Monto = factura.Monto,
                    Mesa = factura.Cuenta.Mesa.Numero,
                    Descuento = factura.Descuento,
                    Usuario = (usuario == null) ? "" : usuario.Nombre + " " + usuario.Apellido,
                    Ordenes = new List<OrdenViewModel>()
                };

                foreach(var item in factura.Detalle)
                {
                    var producto = await _contexto.Producto.FindAsync(item.IdProducto);

                    model.Ordenes.Add(new OrdenViewModel() { 
                        IdProducto = item.IdProducto,
                        IdCuenta = factura.IdCuenta ?? default(int),
                        Precio = item.Precio,
                        Descripcion = producto.Descripcion,
                        Cantidad = item.Cantidad,
                        Total = item.Cantidad * item.Precio
                    });
                }

            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        
        public async Task<FacturaViewModel> FindOnlyFactura(int idCuenta)
        {
            FacturaViewModel model = null;
            try
            {
                var factura = await _contexto.Factura.AsNoTracking().SingleOrDefaultAsync(w => w.IdCuenta == idCuenta);

                model = new FacturaViewModel()
                {
                    IdFactura = factura.IdFactura,
                    IdCuenta = factura.IdCuenta ?? default(int),
                    Monto = factura.Monto,
                    Descuento = factura.Descuento,
                    Fecha = factura.Fecha,
                    Estado = factura.Estado,
                    Ordenes = new List<OrdenViewModel>()
                };
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public async Task<bool> ChangeStatus(int id)
        {
            var paso = false;
            try
            {
                var factura = await _contexto.Factura.AsNoTracking().SingleOrDefaultAsync(w => w.IdFactura == id);
                factura.Estado = "Pago";

                _contexto.Factura.Update(factura);

                await _contexto.SaveChangesAsync();

                paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        public async Task<List<FacturaViewModel>> GetAllAsViewModel()
        {
            List<FacturaViewModel> list = new List<FacturaViewModel>();
            try
            {
                var facturas = await _contexto.Factura.Include(x => x.Cuenta).ThenInclude(x => x.Mesa).AsNoTracking().ToListAsync();
                
                foreach (var items in facturas)
                {
                    var usuario = await _contexto.Usuario.FindAsync(items.Cuenta.IdUsuario);

                    list.Add(new FacturaViewModel() {
                        IdFactura = items.IdFactura,
                        IdCuenta = items.IdCuenta ?? default(int),
                        IdMesa = items.Cuenta.IdMesa,
                        Fecha = items.Fecha,
                        Estado = items.Estado,
                        Monto = items.Monto,
                        Mesa = items.Cuenta.Mesa.Numero,
                        Descuento = items.Descuento,
                        Usuario = (usuario == null) ? "" : usuario.Nombre + " " + usuario.Apellido
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
        
        public async Task<List<Factura>> GetAll()
        {
            List<Factura> list = null;
            try
            {
                list = await _contexto.Factura.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }

        public async Task<decimal[]> ListOfMontoPerMonth()
        {
            decimal[] montos = new decimal[12];
            try
            {
                var a = await _contexto.Factura.Where(x => x.Fecha.Year == DateTime.Now.Year)
                    .GroupBy(x => x.Fecha.Month)
                    .Select(x => new { Monto = x.Sum(i => i.Monto), Mes = x.Key })
                    .OrderBy(x => x.Mes)
                    .ToListAsync();

                a.ForEach(x => montos[x.Mes-1] = x.Monto);
            }
            catch (Exception)
            {

                throw;
            }
            return montos;
        }


    } 

    class Entry
    {
        public int Month { get; set; }
    }
}
