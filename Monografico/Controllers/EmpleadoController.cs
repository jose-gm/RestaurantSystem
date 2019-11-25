using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monografico.Repositorio;
using Monografico.ViewModels;

namespace Monografico.Controllers
{
    public class EmpleadoController : Controller
    {

        RepositorioBaseTest<EmpleadosViewModel> repo;

        public EmpleadoController()
        {
            repo = new RepositorioBaseTest<EmpleadosViewModel>();
        }

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Empleado/_Create.cshtml", new EmpleadosViewModel());
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody]EmpleadosViewModel empleado)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    repo.Guardar(empleado);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Empleado/_Edit.cshtml", repo.Buscar(id));
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromBody]EmpleadosViewModel empleado)
        {
            try
            {
                // TODO: Add update logic here
                repo.Editar(empleado);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Usuario/Delete/5
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: Usuario/Delete/5
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