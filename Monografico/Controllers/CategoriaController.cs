using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;
using Monografico.Repositorio;
using Monografico.ViewModels;

namespace Monografico.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class CategoriaController : Controller
    {
        RepositoryWrapper repo;

        public CategoriaController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }
        // GET: Productos
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        private Categoria ToModel(CategoriaViewModel model)
        {
            byte[] image = new byte[0];
            if (model.Imagen != null)
            {
                using (var bytes = new MemoryStream())
                {
                    model.Imagen.CopyTo(bytes);
                    image = bytes.ToArray();
                }

            }

            return new Categoria()
            {
                IdCategoria = model.IdCategoria,
                Descripcion = model.Descripcion,
                Imagen = (model.Imagen != null) ? Convert.ToBase64String(image) : model.ImagenEncoded
            };
        }

        private CategoriaViewModel ToViewModel(Categoria model)
        {
            return new CategoriaViewModel()
            {
                IdCategoria = model.IdCategoria,
                Descripcion = model.Descripcion,
                ImagenEncoded = model.Imagen
            };
        }

        // GET: Productos/Create
        public async Task<IActionResult> Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Categoria/_Create.cshtml", new CategoriaViewModel());
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    await repo.Categoria.Add(ToModel(model));
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Categoria/_Edit.cshtml", await repo.Categoria.BuscarCategoriaViewModel(id));
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoriaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    await repo.Categoria.Update(ToModel(model));
                    return Ok();

                }               
            }
            catch
            {
                return BadRequest();
            }
            return NotFound();
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var categoria = await repo.Categoria.FindWithProductos(id);
                if (categoria.Productos.Count == 0)
                {
                    // TODO: Add delete logic here
                    await repo.Categoria.Remove(id);
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NotFound("Esta categoria contiene productos, no puede ser eliminada");
        }

        public async Task<JsonResult> List()
        {
            return Json(await repo.Categoria.GetList(x => true));
        }
    }
}