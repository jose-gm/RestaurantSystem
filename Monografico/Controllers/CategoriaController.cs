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
    public class CategoriaController : Controller
    {
        RepositorioBaseTest<Categoria> repo;
        public CategoriaController()
        {
            repo = new RepositorioBaseTest<Categoria>();
        }
        // GET: Productos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Categoria/_Create.cshtml", new Categoria());
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody]Categoria categoria)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    repo.Guardar(categoria);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Categoria/_Edit.cshtml", repo.Buscar(id));
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromBody]Categoria categoria)
        {
            try
            {
                // TODO: Add update logic here
                repo.Editar(categoria);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Productos/Delete/5
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

        // POST: Productos/Delete/5
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