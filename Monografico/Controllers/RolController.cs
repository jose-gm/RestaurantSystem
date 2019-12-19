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
        RepositoryWrapper repo;

        public RolController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        // GET: Rol
        /*public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Rol/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: Rol/Create
        public async Task<IActionResult> Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Rol/_Create.cshtml", new RolesViewModel());
        }

        // POST: Rol/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]RolesViewModel rol)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    //repo.Rol.Guardar(rol);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Rol/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Rol/_Edit.cshtml", await repo.Rol.Find(id));
        }

        // POST: Rol/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody]RolesViewModel rol)
        {
            try
            {
                // TODO: Add update logic here
                //repo.Rol.Editar(rol);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Rol/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                await repo.Rol.Remove(id);
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

        //GET:
        public async Task<JsonResult> List()
        {
            return Json(await repo.Rol.GetList(x => true));
        }*/
    }
}