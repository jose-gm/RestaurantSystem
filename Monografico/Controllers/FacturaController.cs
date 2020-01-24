using System;
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
        public async Task<IActionResult> Cuenta([FromBody] FacturaViewModel model)
        {
            return PartialView("~/Views/Admin/PartialViews/Orden/_Cuenta.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FacturaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Ordenes.Count > 0)
                    {
                        if ((await repo.Factura.Create(model)))
                        {
                            await repo.Cuenta.ChangeStatus(model.IdCuenta);
                            var cuenta = await repo.Cuenta.FindWithOrdenes(model.IdCuenta);
                            foreach (var orden in cuenta.Ordenes)
                            {
                                foreach (var item in orden.Detalle)
                                {
                                    var producto = await repo.Producto.FindWithChildren(item.IdProducto);
                                    if (producto.Inventario != null)
                                        await repo.Inventario.RestarInventario(producto.Inventario.IdInventario, item.Cantidad);

                                    if (producto.Detalle.Count > 0)
                                    {
                                        foreach (var listItem in producto.Detalle)
                                        {
                                            var ingrediente = await repo.Ingrediente.FindWithInventarioAsync(listItem.IdIngrediente);
                                            if (ingrediente.Inventario != null)
                                            {
                                                await repo.Inventario.RestarInventario(ingrediente.Inventario.IdInventario, listItem.Cantidad * item.Cantidad);
                                            }
                                        }
                                    }

                                }
                            }
                            return Ok();
                        }
                            
                        /*var factura = await repo.Factura.FindOnlyFactura(model.IdCuenta);
                        factura.Mesa = model.Mesa;
                        factura.Ordenes = model.Ordenes;
                        factura.Usuario = model.Usuario;*/
                        //return PartialView("~/Views/Admin/PartialViews/Orden/_Cuenta.cshtml", factura);
                    }
                    else
                        throw new Exception("No hay ordenes realizadas");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message});
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarPago([FromBody]FacturaViewModel model)
        {      
            return PartialView("~/Views/Admin/PartialViews/Orden/_ConfirmarPago.cshtml", model);  
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> FacturaAsPDF(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Factura/_Factura.cshtml", await repo.Factura.FindAsViewModel(id));
        }

        [Authorize(Roles = "Administrador")]
        public async Task<JsonResult> List()
        {
            return Json(await repo.Factura.GetAllAsViewModel());
        }
        
        [Authorize(Roles = "Administrador")]
        public async Task<JsonResult> ListRange(DateTime desde, DateTime hasta)
        {
            return Json(await repo.Factura.GetAllAsViewModel(x => (x.Fecha >= desde) && (x.Fecha <= hasta) ));
        }
        
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListRangePDF(DateTime desde, DateTime hasta)
        {
            return new ViewAsPdf("~/Views/Admin/ReportsPDF/FacturasPDF.cshtml", await repo.Factura.GetAllAsViewModel(x => (x.Fecha >= desde) && (x.Fecha <= hasta) ));
        }
        
        [Authorize(Roles = "Administrador")]
        public async Task<JsonResult> ListProductoRange(int idProducto, DateTime desde, DateTime hasta)
        {
            return Json(await repo.Factura.GetAllProductos(idProducto ,x => (x.Fecha >= desde) && (x.Fecha <= hasta) ));
        }
        
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListProductoRangePDF(int idProducto, DateTime desde, DateTime hasta)
        {
            return new ViewAsPdf("~/Views/Admin/ReportsPDF/ProductosPDF.cshtml", await repo.Factura.GetAllProductos(idProducto, x => (x.Fecha >= desde) && (x.Fecha <= hasta)));
        }
    }
}