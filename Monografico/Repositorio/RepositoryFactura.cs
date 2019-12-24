﻿using Microsoft.EntityFrameworkCore;
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
                    Monto = model.Monto,
                    Descuento = model.Descuento,
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
                    Mesa = factura.Cuenta.Mesa.Descripcion,
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

                var cuenta = await _contexto.Cuenta.AsNoTracking().SingleOrDefaultAsync(q => q.IdCuenta == factura.IdCuenta);
                cuenta.Activa = false;

                _contexto.Factura.Update(factura);
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
    }
}