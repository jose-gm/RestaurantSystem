using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monografico.Repositorio;
using Monografico.ViewModels;

namespace Monografico.Controllers
{
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
        
        public async Task<IActionResult> CambiarEstado(int id)
        {
            try
            {
                await repo.Factura.ChangeStatus(id);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}