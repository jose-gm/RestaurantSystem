﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monografico.Repositorio;
using Monografico.ViewModels;
using Rotativa.AspNetCore;

namespace Monografico.Controllers
{
    [Authorize(Roles = "Administrador,Mesero")]
    public class FacturaController : Controller
    {
        private readonly RepositoryWrapper repo;

        public FacturaController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FacturaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await repo.Factura.Create(model);
                    var factura = await repo.Factura.FindOnlyFactura(model.IdCuenta);
                    factura.Mesa = model.Mesa;
                    factura.Ordenes = model.Ordenes;
                    return PartialView("~/Views/Admin/PartialViews/Orden/_Factura.cshtml", factura);
                }
            }
            catch (Exception)
            {

                return Json("Error");
            }
            return BadRequest();
        }

        public async Task<IActionResult> ConfirmarPago(int idCuenta)
        {      
            return PartialView("~/Views/Admin/PartialViews/Orden/_ConfirmarPago.cshtml", await repo.Factura.Find(idCuenta,false));  
        }
        
        public async Task<IActionResult> CambiarEstado(int id, int idCuenta)
        {
            try
            {
                await repo.Factura.ChangeStatus(id);
                await repo.Cuenta.ChangeStatus(idCuenta);

                var cuenta = await repo.Cuenta.FindWithOrdenes(idCuenta);
                foreach (var orden in cuenta.Ordenes)
                {
                    foreach (var item in orden.Detalle)
                    {
                        var producto = await repo.Producto.FindWithChildren(item.IdProducto);
                        if(producto.Inventario != null)                       
                            await repo.Inventario.RestarInventario(producto.Inventario.IdInventario, item.Cantidad);

                        if(producto.Detalle.Count > 0)
                        {
                            foreach (var listItem in producto.Detalle)
                            {
                                var ingrediente = await repo.Ingrediente.FindWithInventarioAsync(listItem.IdIngrediente);
                                if(ingrediente.Inventario != null)
                                {
                                    await repo.Inventario.RestarInventario(ingrediente.Inventario.IdInventario, listItem.Cantidad * item.Cantidad);
                                }
                            }
                        }
                        
                    }
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> FacturaAsPDF(int id)
        {
            return new ViewAsPdf("~/Views/Admin/FacturaPdf.cshtml", await repo.Factura.FindAsViewModel(id));
        }

        [Authorize(Roles = "Administrador")]
        public async Task<JsonResult> List()
        {
            return Json(await repo.Factura.GetAll());
        }
    }
}