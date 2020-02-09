using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
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
                    Fecha = DateTime.Now,
                    Itbis = model.Itbis,
                    PorcientoLey = model.PorcientoLey,
                    Descuento = (model.Descuento / 100) * model.Monto,
                    Monto = model.Monto - ((model.Descuento / 100) * model.Monto),
                    MetodoPago = model.MetodoPago,
                    Tarjeta = (model.MetodoPago.Equals("Tarjeta")) ? model.Tarjeta : null,
                    TipoTarjeta = (model.MetodoPago.Equals("Tarjeta")) ? model.TipoTarjeta : null,
                    Cheque = (model.MetodoPago.Equals("Cheque")) ? model.Cheque : null,
                    Estado = "Pago",
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
                    Itbis = factura.Itbis,
                    PorcientoLey = factura.PorcientoLey,
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
                    Itbis = factura.Itbis,
                    PorcientoLey = factura.PorcientoLey,
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
                        Descripcion = (producto == null) ? "Eliminado" : producto.Descripcion,
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
                    Itbis = factura.Itbis,
                    PorcientoLey = factura.PorcientoLey,
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

        public async Task<bool> Update(PagoViewModel model)
        {
            var paso = false;
            try
            {
                var factura = await _contexto.Factura.AsNoTracking().SingleOrDefaultAsync(w => w.IdFactura == model.IdFactura);
                factura.Estado = "Pago";
                factura.MetodoPago = model.MetodoPago;
                factura.Tarjeta = model.Tarjeta;
                factura.TipoTarjeta = model.TipoTarjeta;

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
                        Itbis = items.Itbis,
                        PorcientoLey = items.PorcientoLey,
                        Monto = items.Monto,
                        Mesa = items.Cuenta.Mesa.Numero,
                        MetodoPago = items.MetodoPago,
                        Tarjeta = items.Tarjeta,
                        Cheque = items.Cheque,
                        TipoTarjeta = items.TipoTarjeta,
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
        
        public async Task<List<FacturaViewModel>> GetAllAsViewModel(Expression<Func<Factura, bool>> expression)
        {
            List<FacturaViewModel> list = new List<FacturaViewModel>();
            try
            {
                var facturas = await _contexto.Factura.Include(x => x.Cuenta).ThenInclude(x => x.Mesa).Where(expression).AsNoTracking().ToListAsync();
                
                foreach (var items in facturas)
                {
                    var usuario = await _contexto.Usuario.FindAsync(items.Cuenta.IdUsuario);

                    list.Add(new FacturaViewModel() {
                        IdFactura = items.IdFactura,
                        IdCuenta = items.IdCuenta ?? default(int),
                        IdMesa = items.Cuenta.IdMesa,
                        Fecha = items.Fecha,
                        Monto = items.Monto,
                        Itbis = items.Itbis,
                        PorcientoLey = items.PorcientoLey,
                        Mesa = items.Cuenta.Mesa.Numero,
                        Descuento = items.Descuento,
                        Usuario = (usuario == null) ? "" : usuario.Nombre + " " + usuario.Apellido,
                        MetodoPago = items.MetodoPago
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
       
        public async Task<List<ProductoFacturaViewModel>> GetAllProductos(int idProducto, Expression<Func<Factura, bool>> expression)
        {
            List<ProductoFacturaViewModel> list = new List<ProductoFacturaViewModel>();
            try
            {
                var facturas = await _contexto.Factura.Include(x => x.Detalle).Where(expression).AsNoTracking().ToListAsync();
                
                foreach (var items in facturas)
                {
                    foreach(var detalle in items.Detalle)
                    {
                        Producto producto;
                        if (idProducto == 0)
                            producto = await _contexto.Producto.FindAsync(detalle.IdProducto);
                        else
                        {
                            if (detalle.IdProducto == idProducto)
                                producto = await _contexto.Producto.FindAsync(detalle.IdProducto);
                            else
                                continue;
                        }

                        list.Add(new ProductoFacturaViewModel() { 
                            IdProducto = producto.IdProducto,
                            IdFactura = items.IdFactura,
                            Descripcion = producto.Descripcion,
                            Fecha = items.Fecha,
                            Cantidad = detalle.Cantidad,
                            Costo = producto.Costo,
                            Precio = producto.Precio,
                            Total = detalle.Cantidad * producto.Precio
                        });
                    }
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

        public async Task<List<Producto>> GetProductoMasVendidos()
        {
            List<Producto> list = null;
            try
            {
                var detalle = await (from parent in _contexto.Factura.Where(x => x.Fecha.Year == DateTime.Today.Year)
                        from child in parent.Detalle
                        select child).ToListAsync();

                var s = detalle.GroupBy(x => x.IdProducto).OrderByDescending(x => x.Sum(y => y.Cantidad)).Select(x => x.Key).Take(10).ToList();
                list = new List<Producto>();
                foreach (var item in s)
                {
                    list.Add(await _contexto.Producto.FindAsync(item));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
        
        public async Task<ReportesViewModel> GetProductoMasVendidosAsViewModel()
        {
            ReportesViewModel model = null;
            try
            {
                var detalle = await (from parent in _contexto.Factura.Where(x => x.Fecha.Year == DateTime.Today.Year)
                        from child in parent.Detalle
                        select child).ToListAsync();

                model = new ReportesViewModel();
                model.Productos = new List<string>();
                var s = detalle.GroupBy(x => x.IdProducto).OrderByDescending(x => x.Sum(y => y.Cantidad)).Select(x => new { Id = x.Key, Cantidad = x.Sum(y => y.Cantidad) }).Take(10).ToList();
                model.ProductosId = s.Select(x => x.Cantidad).ToList();
                foreach (var item in s)
                {
                    var producto = await _contexto.Producto.FindAsync(item.Id);
                    model.Productos.Add((producto == null) ? "Eliminado" : producto.Descripcion);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public async Task<decimal> GetMontoVentasPorFecha(DateTime fecha)
        {
            decimal monto;
            try
            {
                monto = _contexto.Factura.Where(x => ((x.Fecha >= fecha) && (x.Fecha <= DateTime.Today.AddDays(1))) && x.MetodoPago.Equals("Efectivo")).AsNoTracking().Sum(x => x.Monto);
            }
            catch (Exception)
            {

                throw;
            }
            return monto;
        }
        
        public async Task<decimal> GetMontoTarjetaPorFecha(DateTime fecha)
        {
            decimal monto;
            try
            {
                monto = _contexto.Factura.Where(x => ((x.Fecha >= fecha) && (x.Fecha <= DateTime.Today.AddDays(1))) && x.MetodoPago.Equals("Tarjeta")).AsNoTracking().Sum(x => x.Monto);
            }
            catch (Exception)
            {

                throw;
            }
            return monto;
        }
        
        public async Task<decimal> GetMontoChequePorFecha(DateTime fecha)
        {
            decimal monto;
            try
            {
                monto = _contexto.Factura.Where(x => ((x.Fecha >= fecha) && (x.Fecha <= DateTime.Today.AddDays(1))) && x.MetodoPago.Equals("Cheque")).AsNoTracking().Sum(x => x.Monto);
            }
            catch (Exception)
            {

                throw;
            }
            return monto;
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

        public async Task<decimal[]> ListOfMontoPerWeek()
        {
            decimal[] montos = new decimal[7];
            try
            {
                var list =  await _contexto.Factura.Where(x => GetWeekNumber(x.Fecha) == GetWeekNumber(DateTime.Today)).AsNoTracking().ToListAsync();
                var a = list.GroupBy(x => x.Fecha.DayOfWeek)
                            .Select(x => new { Monto = x.Sum(y => y.Monto), Dia = (int)x.Key })
                            .OrderBy(x => (int)x.Dia)
                            .ToList();


                a.ForEach(x => montos[x.Dia] += x.Monto);
                for (int i = 0; i < montos.Length - 1; i++)
                {
                    var r = montos[i];
                    montos[i] = montos[i + 1];
                    montos[i + 1] = r;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return montos;
        }

        public int GetWeekNumber(DateTime date)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekNum;
        }

        public async Task<int> HoraMasVendida()
        {
            var hora = 0;
            try
            {
                var list = await _contexto.Factura.Where(x => x.Fecha.Year == DateTime.Today.Year).AsNoTracking().ToListAsync();
                var a = list.GroupBy(x => x.Fecha.Hour)
                            .Select(x => new { Hora = x.Key, Cantidad = x.Count()})
                            .OrderByDescending(x => x.Cantidad)
                            .ToList();

                hora = (a.Count > 0) ? a.FirstOrDefault().Hora : 0;
            }
            catch (Exception)
            {

                throw;
            }
            return hora;
        }
        
        public async Task<string> ZonaMayorVenta()
        {
            var zona = "";
            try
            {
                var list = await _contexto.Factura.Where(x => x.Fecha.Year == DateTime.Today.Year)
                                                    .Include(x => x.Cuenta)
                                                    .ThenInclude(x => x.Mesa)
                                                    .AsNoTracking()
                                                    .ToListAsync();
                var a = list.GroupBy(x => x.Cuenta.Mesa.IdZona)
                            .Select(x => new { Monto = x.Sum(y => y.Monto), Zona = x.Key })
                            .OrderByDescending(x => x.Monto)
                            .ToList();

                if(a.Count > 0)
                    zona = ( await _contexto.Zona.FindAsync(a.FirstOrDefault().Zona)).Descripcion;
            }
            catch (Exception)
            {

                throw;
            }
            return zona;
        }
        
        public async Task<string> MeseroMayorVenta()
        {
            var mesero = "";
            try
            {
                var list = await _contexto.Factura.Where(x => x.Fecha.Year == DateTime.Today.Year)
                                                    .Include(x => x.Cuenta)
                                                    .AsNoTracking()
                                                    .ToListAsync();
                var a = list.GroupBy(x => x.Cuenta.IdUsuario)
                            .Select(x => new { Monto = x.Sum(y => y.Monto), IdUsuario = x.Key })
                            .OrderByDescending(x => x.Monto)
                            .ToList();

                if(a.Count > 0)
                {
                    var usuario = await _contexto.Usuario.FindAsync(a.FirstOrDefault().IdUsuario);
                    mesero = usuario.Nombre + " " + usuario.Apellido;
                }               
            }
            catch (Exception)
            {

                throw;
            }
            return mesero;
        }
        
        public async Task<string> MesaMayorVenta()
        {
            var mesa = "";
            try
            {
                var list = await _contexto.Factura.Where(x => x.Fecha.Year == DateTime.Today.Year)
                                                    .Include(x => x.Cuenta)
                                                    .ThenInclude(x => x.Mesa)
                                                    .AsNoTracking()
                                                    .ToListAsync();
                var a = list.GroupBy(x => x.Cuenta.Mesa.IdMesa)
                            .Select(x => new { Monto = x.Sum(y => y.Monto), IdMesa = x.Key })
                            .OrderByDescending(x => x.Monto)
                            .ToList();

                if(a.Count > 0)
                    mesa = (await _contexto.Mesa.FindAsync(a.FirstOrDefault().IdMesa)).Numero.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            return mesa;
        }
    } 
}
