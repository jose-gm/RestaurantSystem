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
    [Authorize(Roles = "Administrador")]
    public class ItbisController : Controller
    {
        RepositoryWrapper repo;

        public ItbisController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }
        // GET: Zona
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Zona/Create
        public async Task<IActionResult> Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Itbis/_Create.cshtml", new Itbis());
        }

        // POST: Zona/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]Itbis itbis)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if(await repo.Itbis.Add(itbis))
                        return Ok();
                }
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        // GET: Zona/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Itbis/_Edit.cshtml", await repo.Itbis.Find(id));
        }

        // POST: Zona/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody]Itbis itbis)
        {
            try
            {
                // TODO: Add update logic here
                await repo.Itbis.Update(itbis);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Zona/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                if(await repo.Itbis.Remove(id))
                    return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest("Esta zona contiene mesas, no puede ser eliminada");
        }

        public async Task<JsonResult> List()
        {
            return Json(await repo.Itbis.GetList(x => true));
        }
    }
}