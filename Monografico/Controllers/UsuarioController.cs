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
    public class UsuarioController : Controller
    {

        RepositoryWrapper repo;

        public UsuarioController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public async Task<IActionResult> Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Empleado/_Create.cshtml", new UsuarioViewModel());
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]UsuarioViewModel usuario)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    //repo.Usuario.Guardar(usuario);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Empleado/_Edit.cshtml", await repo.Usuario.Find(id));
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody]UsuarioViewModel usuario)
        {
            try
            {
                // TODO: Add update logic here
                //repo.Usuario.Editar(usuario);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                await repo.Usuario.Remove(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: Usuario/Delete/5
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
            return Json(await repo.Usuario.GetList(x => true));
        }
    }
}