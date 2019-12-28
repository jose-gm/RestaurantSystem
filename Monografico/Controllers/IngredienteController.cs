using System;
using System.Collections.Generic;
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
    public class IngredienteController : Controller
    {
        RepositoryWrapper repo;

        public IngredienteController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        // GET: Ingrediente
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Ingrediente/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        private Ingrediente ToIngrediente(IngredienteViewModel model)
        {
            Inventario inventario = new Inventario();

            if (model.LlevaInventario)
            {
                inventario = new Inventario()
                {
                    FechaEntrada = model.FechaEntrada.Date,
                    Cantidad = model.Cantidad,
                    Unidad = model.Unidad,
                    IdIngrediente = model.IdIngrediente
                };
            }

            return new Ingrediente()
            {
                IdIngrediente = model.IdIngrediente,
                Descripcion = model.Descripcion,
                Costo = model.Costo,
                LlevaInventario = (model.LlevaInventario) ? model.LlevaInventario : model.TieneInventario,
                Inventario = (model.LlevaInventario) ? inventario : null
            };
        }

        private IngredienteViewModel ToIngredienteViewModel(Ingrediente model)
        {
            return new IngredienteViewModel()
            {
                IdIngrediente = model.IdIngrediente,
                Descripcion = model.Descripcion,
                Costo = model.Costo,
                LlevaInventario = model.LlevaInventario
            };
        }

        // GET: Ingrediente/Create
        public async Task<IActionResult> Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Ingrediente/_Create.cshtml", new IngredienteViewModel());
        }

        // POST: Ingrediente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]IngredienteViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    await repo.Ingrediente.Add(ToIngrediente(model));
                    return Ok();
                }

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NotFound();
        }

        // GET: Ingrediente/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return PartialView("~/Views/Admin/PartialViews/Ingrediente/_Edit.cshtml", await repo.Ingrediente.BuscarIngredienteViewModel(id));
        }

        // POST: Ingrediente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody]IngredienteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    await repo.Ingrediente.Update(ToIngrediente(model));
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
            return NotFound();
        }

        // GET: Ingrediente/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                await repo.Ingrediente.EliminarConInventario(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: Ingrediente/Delete/5
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
            return Json(await repo.Ingrediente.GetList(x => true));
        }
        
        //GET:
        public async Task<JsonResult> GetAll(string search)
        {
            if(!string.IsNullOrEmpty(search))
                return Json(await repo.Ingrediente.GetList(x => x.Descripcion.Contains(search)));
            else
                return Json(await repo.Ingrediente.GetList(x => true));
        }
    }
}