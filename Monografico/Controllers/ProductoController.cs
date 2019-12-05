﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;
using Monografico.Repositorio;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Monografico.Controllers
{
    public class ProductoController : Controller
    {
        RepositoryWrapper repo;

        public ProductoController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        private Producto ToModel(ProductoViewModel model)
        {
            byte[] image = new byte[0];
            Inventario inventario = new Inventario();
            if(model.Imagen != null)
            {
                using (var bytes = new MemoryStream())
                {
                    model.Imagen.CopyTo(bytes);
                    image = bytes.ToArray();
                }
            }

            if (model.LlevaInventario)
            {
                inventario = new Inventario()
                {
                    FechaEntrada = Convert.ToDateTime(Request.Form["FechaEntrada"]).Date,
                    Cantidad = model.Cantidad,
                    Unidad = model.Unidad,
                    IdProducto = model.IdProducto
                };
            }
           
            return new Producto()
            {
                IdProducto = model.IdProducto,
                IdCategoria = model.IdCategoria,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                Imagen = (model.Imagen != null) ? Convert.ToBase64String(image) : model.ImagenEncoded,
                LlevaIngredientes = model.LlevaIngredientes,
                LlevaInventario = (model.LlevaInventario) ? model.LlevaInventario : model.TieneInventario,
                Inventario = (model.LlevaInventario) ? inventario : null
            };
        }

        private ProductoViewModel ToViewModel(Producto model)
        {
            return new ProductoViewModel()
            {
                IdProducto = model.IdProducto,
                IdCategoria = model.IdCategoria,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                ImagenEncoded = model.Imagen
            };
        }

        // GET: Producto/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = await repo.Categoria.GetSelectList();
            return PartialView("~/Views/Admin/PartialViews/Producto/_Create.cshtml", new ProductoViewModel());
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    await repo.Producto.Add(ToModel(model));
                    return Ok();
                }

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NotFound();
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categorias = await repo.Categoria.GetSelectList();
            return PartialView("~/Views/Admin/PartialViews/Producto/_Edit.cshtml", await repo.Producto.BuscarProductoViewModel(id));
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    await repo.Producto.Update(ToModel(model));
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
            return NotFound();
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                await repo.Producto.EliminarConInventario(id);
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
            return Json(await repo.Producto.GetList(x => true));
        }
    }
}