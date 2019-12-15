using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;
using Monografico.Repositorio;

namespace Monografico.Controllers
{
    public class MesaController : Controller
    {
        RepositoryWrapper repo;

        public MesaController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }
        // GET: Mesa
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Mesa/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: Mesa/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Zonas = await repo.Zona.GetSelectList();
            return PartialView("~/Views/Admin/PartialViews/Mesa/_Create.cshtml", new Mesa());
        }

        // POST: Mesa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]Mesa mesa)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    await repo.Mesa.Add(mesa);
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NotFound();
        }

        // GET: Mesa/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Mesa/_Edit.cshtml", await repo.Mesa.Find(id));
        }

        // POST: Mesa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody]Mesa mesa)
        {
            try
            {
                // TODO: Add update logic here
                await repo.Mesa.Update(mesa);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Mesa/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                await repo.Mesa.Remove(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: Mesa/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public async Task<JsonResult> List()
        {
            return Json(await repo.Mesa.GetList(x => true));
        }
    }
}