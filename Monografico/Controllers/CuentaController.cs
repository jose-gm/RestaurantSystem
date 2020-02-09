using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;
using Monografico.Repositorio;
using Monografico.ViewModels;

namespace Monografico.Controllers
{
    [Authorize(Roles = "Administrador,Mesero")]
    public class CuentaController : Controller
    {
        RepositoryWrapper repo;

        public CuentaController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        // GET: Orden
        public async Task<ActionResult> Index(int idMesa)
        {
            var usuario = await repo.Usuario.GetUsuario(HttpContext.User);
            var model = await repo.Cuenta.FindCuentaViewModel(idMesa);

            if(model.Cuentas.Count == 0)
            {
                await repo.Cuenta.Add(new Cuenta() { IdCuenta = 0, IdMesa = idMesa, IdUsuario = usuario.Id, Activa = true });
                model = await repo.Cuenta.FindCuentaViewModel(idMesa);
            }

            model.IdUsuario = usuario.Id;
            ViewBag.Usuario = usuario.Nombre + " " + usuario.Apellido;

            ViewBag.AlterMenu = true;
            model.IsCajaAbierta = await repo.Caja.IsCajaAbierta();
            return View("~/Views/Admin/Orden.cshtml",model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrden([FromBody]OrdenViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    await repo.Cuenta.AddOrden(model);
                    return Ok();
                }
                
            }
            catch
            {
                return View();
            }
            return NotFound();
        }
        
        public async Task<IActionResult> UpdateOrden(int idOrden, int idDetalle, int cantidad)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    await repo.Orden.UpdateOrden(idOrden, idDetalle, cantidad);
                    return Ok();
                }
                
            }
            catch
            {
                return View();
            }
            return NotFound();
        }

        // GET: Orden/Edit/5
        public async Task<IActionResult> EnviarOrdenes(int id)
        {
            try
            {
                await repo.Cuenta.EnviarOrdenes(id);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cuenta cuenta)
        {
            try
            {
                if (await repo.Cuenta.Add(cuenta))
                    return Json(cuenta);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NotFound();
        }

        // GET: Orden/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await repo.Cuenta.Remove(id);
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CancelAll(int idMesa)
        {
            try
            {
                var cuentas = await repo.Cuenta.GetList(x => x.IdMesa == idMesa && x.Activa);
                if(await repo.Cuenta.RemoveAllAsync(cuentas))
                    return PartialView("~/Views/Admin/PartialViews/Orden/_TicketOrdenesCancelada.cshtml", cuentas);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NotFound();
        }
        
        // GET:
        public async Task<ActionResult> DeleteOrden(int idOrden, int idDetalle)
        {
            try
            {

                await repo.Orden.RemoveOrden(idOrden, idDetalle);
                return Ok();
            }
            catch (Exception)
            {

                return Json("Error");
            }
        }
        
        // GET:
        public async Task<IActionResult> CancelarOrden(int id, bool ordenPendiente)
        {
            try
            {
                if (ordenPendiente)
                {
                    ViewBag.OrdenCancelada = true;
                    var ticket = await repo.Cuenta.GetListOrdenViewModels(id, x => x.Enviado == true);
                    await repo.Cuenta.RemoveAllOrdenes(id);
                    return PartialView("~/Views/Admin/PartialViews/Orden/_TicketOrden.cshtml", ticket);
                }
                else
                {
                    await repo.Cuenta.RemoveAllOrdenes(id);
                    return Ok();
                }
            }
            catch (Exception)
            {

                return Json("Error");
            }
        }

        public async Task<IActionResult> LoadCuentas(int idMesa)
        {        
            return PartialView("~/Views/Admin/PartialViews/Orden/_Cuentas.cshtml", await repo.Cuenta.GetList(x => x.IdMesa == idMesa && x.Activa));
        }

        public async Task<JsonResult> ListOFOrden(int id)
        {
            return Json(await repo.Cuenta.GetListOrdenViewModels(id, x => true));
        }
        
        public async Task<IActionResult> TicketOrden(int id)
        {
            var usuario = await repo.Usuario.GetUsuario(HttpContext.User);
            ViewBag.Usuario = usuario.Nombre + " " + usuario.Apellido;
            ViewBag.OrdenCancelada = false;
            return PartialView("~/Views/Admin/PartialViews/Orden/_TicketOrden.cshtml", await repo.Cuenta.GetListOrdenViewModels(id, x => x.Enviado == false));
        }
        
        public async Task<IActionResult> ReHacerTicketOrden([FromQuery]int id, [FromQuery(Name = "idOrden")]int idOrden)
        {
            var usuario = await repo.Usuario.GetUsuario(HttpContext.User);
            ViewBag.Usuario = usuario.Nombre + " " + usuario.Apellido;
            ViewBag.OrdenCancelada = false;
            return PartialView("~/Views/Admin/PartialViews/Orden/_TicketOrden.cshtml", await repo.Cuenta.GetListOrdenViewModels(id, x => x.IdOrden == idOrden));
        }

        public async Task<bool> OrdenesEnviadas(int idMesa)
        {
            return await repo.Cuenta.HayOrdenesPendiente(idMesa);
        }
    }
}