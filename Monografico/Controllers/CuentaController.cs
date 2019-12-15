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
    public class CuentaController : Controller
    {
        RepositoryWrapper repo;

        public CuentaController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        // GET: Orden
        public async Task<ActionResult> Index(int id)
        {
            var cuenta = await repo.Cuenta.FindCuentaViewModel(id, true);
            if(cuenta == null)
            {
                await repo.Cuenta.Add(new Cuenta() { IdCuenta = 0, IdMesa = id, Activa = true });
                cuenta = await repo.Cuenta.FindCuentaViewModel(id, true);
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

        // GET: Orden/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Orden/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orden/Create
        [HttpPost]
        public ActionResult Create([FromBody]CuentaViewModel model)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Orden/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orden/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Orden/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
        public async Task<ActionResult> CancelarOrden(int id)
        {
            try
            {
                await repo.Cuenta.RemoveAllOrdenes(id);
                return Ok();
            }
            catch (Exception)
            {

                return Json("Error");
            }
        }

        // POST: Orden/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<JsonResult> ListOFOrden(int id)
        {
            return Json(await repo.Cuenta.GetAllOrdenViewModel(id));
        }
    }
}