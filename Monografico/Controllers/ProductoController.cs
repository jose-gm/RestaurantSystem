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
    public class ProductoController : Controller
    {
        RepositorioBaseTest<Producto> repo;

        public ProductoController()
        {
            repo = new RepositorioBaseTest<Producto>();
        }

        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Producto/_Create.cshtml", new Producto());
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody] Producto prod)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    repo.Guardar(prod);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Producto/_Edit.cshtml", repo.Buscar(id));
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromBody] Producto prod)
        {
            try
            {
                // TODO: Add update logic here
                repo.Editar(prod);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Producto/Delete/5
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

        // POST: Producto/Delete/5
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

        //GET:
        public JsonResult List()
        {
            return Json(repo.GetList(x => true));
        }
    }
}