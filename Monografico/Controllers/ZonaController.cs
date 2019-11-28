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
    public class ZonaController : Controller
    {
        RepositorioBaseTest<Zona> repo;
        public ZonaController()
        {
            repo = new RepositorioBaseTest<Zona>();
        }
        // GET: Zona
        public ActionResult Index()
        {
            return View();
        }

        // GET: Zona/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Zona/Create
        public ActionResult Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Zona/_Create.cshtml", new Zona());
        }

        // POST: Zona/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody]Zona zona)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    repo.Guardar(zona);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Zona/Edit/5
        public ActionResult Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Zona/_Edit.cshtml", repo.Buscar(id));
        }

        // POST: Zona/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromBody]Zona zona)
        {
            try
            {
                // TODO: Add update logic here
                repo.Editar(zona);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Zona/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                repo.Eliminar(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: Zona/Delete/5
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

        public JsonResult List()
        {
            return Json(repo.GetList(x => true));
        }
    }
}