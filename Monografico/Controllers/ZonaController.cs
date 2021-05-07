using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;
using Monografico.Repositorio;

namespace Monografico.Controllers
{
    [Authorize(Roles = "Administrador, Host")]
    public class ZonaController : Controller
    {
        RepositoryWrapper repo;

        public ZonaController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }
        // GET: Zona
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Zona/Details/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: Zona/Create
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Zona/_Create.cshtml", new Zona());
        }

        // POST: Zona/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody]Zona zona)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    await repo.Zona.Add(zona);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Zona/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Zona/_Edit.cshtml", await repo.Zona.Find(id));
        }

        // POST: Zona/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit([FromBody]Zona zona)
        {
            try
            {
                // TODO: Add update logic here
                await repo.Zona.Update(zona);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Zona/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var zona = await repo.Zona.FindWithMesasAsync(id);
                if(zona.Mesas.Count == 0)
                {
                    // TODO: Add delete logic here
                    await repo.Zona.Remove(id);
                    return Ok();
                }
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest("Esta zona contiene mesas, no puede ser eliminada");
        }

        // POST: Zona/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
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

        [Authorize(Roles = "Administrador")]
        public async Task<JsonResult> List()
        {
            return Json(await repo.Zona.GetList(x => true));
        }

        public async Task<JsonResult> ListOfMesa(int zona)
        {
            return Json(await repo.Zona.GetAllMesas(x => (zona == 0) ? true : x.IdZona == zona));
        }
    }
}