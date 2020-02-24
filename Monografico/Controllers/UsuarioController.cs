using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monografico.Repositorio;
using Monografico.ViewModels;

namespace Monografico.Controllers
{
    [Authorize(Roles = "Administrador, Mesero")]
    public class UsuarioController : Controller
    {

        RepositoryWrapper repo;

        public UsuarioController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        // GET: Usuario
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Usuario/Details/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await repo.Rol.GetSelectList();
            return PartialView("~/Views/Admin/PartialViews/Empleado/_Create.cshtml", new UsuarioViewModel());
        }

        // POST: Usuario/Create
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = await repo.Usuario.Create(model);
                    if(result.Succeeded)
                        return Ok();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest(false);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Roles = await repo.Rol.GetSelectList();
            return PartialView("~/Views/Admin/PartialViews/Empleado/_Edit.cshtml", await repo.Usuario.FindAsViewModel(id));
        }

        // POST: Usuario/Edit/5
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioViewModel usuario)
        {
            try
            {
                if ((!(await repo.Usuario.Exists(usuario.NombreUsuario))) ||
                    ((await repo.Usuario.Exists(usuario.NombreUsuario)) && usuario.NombreUsuario.Equals(usuario.UsuarioActual)))
                {
                    // TODO: Add update logic here
                    if((await repo.Usuario.Update(usuario)))
                        return Ok();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest(false);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPerfil(EditarPerfilViewModel usuario)
        {
            List<string> errors = new List<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    var result = await repo.Usuario.UpdateUsuario(usuario);
                    if(result.Succeeded)
                        return Ok();

                    errors = new List<string>();
                    foreach (var error in result.Errors)
                        errors.Add(error.Description);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest(new { response = false, errors = errors });
        }
        
        public async Task<IActionResult> EliminarImagen()
        {
            try
            {              
                // TODO: Add update logic here
                if((await repo.Usuario.RemoveImage(HttpContext.User)))
                    return Ok();
   
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest(new { response = false});
        }
        
        [HttpPost]
        public async Task<IActionResult> ActualizarClave(CambiarClaveViewModel model)
        {
            List<string> errors = new List<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    var result = await repo.Usuario.ActualizarClave(model, HttpContext.User);
                    if(result.Succeeded)
                        return Ok();

                    errors = new List<string>();
                    foreach (var error in result.Errors)
                        errors.Add(error.Description);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest(new { response = false, errors = errors });
        }

        // GET: Usuario/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                if(await repo.Usuario.Remove(id))
                    return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NotFound();
        }

        //GET:
        [Authorize(Roles = "Administrador")]
        public async Task<JsonResult> List()
        {
            return Json(await repo.Usuario.GetListAsViewModel());
        }

        public async Task<JsonResult> IsUserNameInUse(string nombreUsuario, string nombreUsuarioActual, bool isEdicion)
        {
            if (!isEdicion)
            {
                if ((await repo.Usuario.Exists(nombreUsuario)))
                    return Json(false);
            }
            else
            {
                if (!nombreUsuario.Equals(nombreUsuarioActual))
                {
                    if ((await repo.Usuario.Exists(nombreUsuario)))
                        return Json(false);
                }
            }

            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyPassword(string password)
        {
            try
            {
                if (await repo.Usuario.IsAnAdminPassword(password))
                    return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NotFound();
        }
    }
}