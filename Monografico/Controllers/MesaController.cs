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
        RepositorioBaseTest<Mesa> repo;

        public MesaController()
        {
            repo = new RepositorioBaseTest<Mesa>();
        }
        // GET: Mesa
        public ActionResult Index()
        {
            return View();
        }

        // GET: Mesa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mesa/Create
        public ActionResult Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Mesa/_Create.cshtml", new Mesa());
        }

        // POST: Mesa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody]Mesa mesita)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    repo.Guardar(mesita);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Mesa/Edit/5
        public ActionResult Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Mesa/_Edit.cshtml", repo.Buscar(id));
        }

        // POST: Mesa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromBody]Mesa mesa)
        {
            try
            {
                // TODO: Add update logic here
                repo.Editar(mesa);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Mesa/Delete/5
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

        // POST: Mesa/Delete/5
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