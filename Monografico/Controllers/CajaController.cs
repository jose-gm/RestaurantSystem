using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;
using Monografico.Repositorio;
using Monografico.ViewModels;

namespace Monografico.Controllers
{
    public class CajaController : Controller
    {
        private readonly RepositoryWrapper repo;

        public CajaController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApertura(AperturaCaja apertura)
        {
            try
            {
                if (await repo.Caja.AbrirCaja(apertura))
                    return Ok(apertura.IdAperturaCaja);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCierre(CierreCaja cierre)
        {
            try
            {
                var usuario = await repo.Usuario.GetUsuario(HttpContext.User);
                if (await repo.Caja.CerrarCaja(usuario, cierre))
                    return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NotFound();
        }

        public async Task<IActionResult> CierreView()
        {
            CierreCajaViewModel model = new CierreCajaViewModel();
            Usuario usuario = await repo.Usuario.GetUsuario(HttpContext.User);
            AperturaCaja apertura = await repo.Caja.GetAperturaCaja(usuario);
            model.MontoInicial = apertura.MontoInicial;
            model.Efectivo = await repo.Factura.GetMontoVentasPorFecha(apertura.Fecha);
            model.Tarjeta = await repo.Factura.GetMontoTarjetaPorFecha(apertura.Fecha);
            model.Cheque = await repo.Factura.GetMontoChequePorFecha(apertura.Fecha);
            return PartialView("~/Views/Admin/PartialViews/Orden/_CierreCaja.cshtml",model);
        }

        public async Task<JsonResult> List(DateTime desde, DateTime hasta)
        {
            return Json(await repo.Caja.GetAllCajaViewModelAsync( x => (x.Fecha >= desde && x.Fecha <= hasta.AddDays(1))));
        }
        
        public async Task<bool> IsCajaAbierta()
        {
            return await repo.Caja.IsCajaAbierta();
        }
    }
}