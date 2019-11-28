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
    public class RolController : Controller
    {
        RepositorioBaseTest<RolesViewModel> repo;

        public RolController()
        {
            repo = new RepositorioBaseTest<RolesViewModel>();
        }

        // GET: Rol
        public ActionResult Index()
        {
            return View();
        }

        // GET: Rol/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rol/Create
        public ActionResult Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Rol/_Create.cshtml", new RolesViewModel());
        }

        // POST: Rol/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody]RolesViewModel rol)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    repo.Guardar(rol);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Rol/Edit/5
        public ActionResult Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Rol/_Edit.cshtml", repo.Buscar(id));
        }

        // POST: Rol/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromBody]RolesViewModel rol)
        {
            try
            {
                // TODO: Add update logic here
                repo.Editar(rol);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Rol/Delete/5
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

        // POST: Rol/Delete/5
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