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
            var cuenta = await repo.Cuenta.FindCuentaViewModel(idMesa, true);
            if(cuenta == null || !cuenta.Activa)
            {
                await repo.Cuenta.Add(new Cuenta() { IdCuenta = 0, IdMesa = idMesa, Activa = true });
                cuenta = await repo.Cuenta.FindCuentaViewModel(idMesa, true);
            }

            return View("~/Views/Admin/Orden.cshtml",cuenta);
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

        public async Task<JsonResult> ListOFOrden(int id)
        {
            return Json(await repo.Cuenta.GetListOrdenViewModels(id, x => true));
        }
        
        public async Task<IActionResult> TicketOrden(int id)
        {
            ViewBag.OrdenCancelada = false;
            return PartialView("~/Views/Admin/PartialViews/Orden/_TicketOrden.cshtml", await repo.Cuenta.GetListOrdenViewModels(id, x => x.Enviado == false));
        }
    }
}