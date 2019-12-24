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
    public class AjusteInventarioController : Controller
    {
        RepositoryWrapper repo;
        public AjusteInventarioController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        // GET: Inventario
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Inventario/Create
        public async Task<IActionResult> Create(int id)
        {
            var Inventario = await repo.Inventario.Find(id);
            AjusteInventario ajusteInventario = new AjusteInventario()
            {
                CantidadAnterior = Inventario.Cantidad,
                IdInventario = Inventario.IdInventario,
                Fecha = DateTime.Now,
                Descripcion = Inventario.Producto != null ? Inventario.Producto.Descripcion : Inventario.Ingrediente.Descripcion

            };
            return PartialView("~/Views/Admin/PartialViews/AjusteInventario/_Create.cshtml", ajusteInventario);
        }

        // POST: Inventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]AjusteInventario inventario)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    await repo.AjusteInventario.Add(inventario);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Inventario/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/AjusteInventario/_Edit.cshtml", await repo.AjusteInventario.Find(id));
        }

        // POST: Inventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody]AjusteInventario inventario)
        {
            try
            {
                // TODO: Add update logic here
                await repo.AjusteInventario.Update(inventario);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Inventario/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                await repo.AjusteInventario.Remove(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // POST: Inventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, bool notData)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GET:
        public async Task<JsonResult> List()
        {
            return Json(await repo.AjusteInventario.GetList(x => true));
        }
    }
}