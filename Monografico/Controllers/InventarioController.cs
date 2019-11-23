﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monografico.Data;
using Monografico.Models;
using Monografico.Repositorio;

namespace Monografico.Controllers
{
    public class InventarioController : Controller
    {
        RepositorioInventario repo;
        public InventarioController(Contexto contexto)
        {
            repo = new RepositorioInventario(contexto);
        }

        // GET: Inventario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Inventario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inventario/Create
        public ActionResult Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Inventario/_Crear.cshtml", new Inventarios());
        }

        // POST: Inventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody]Inventarios inventario)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    repo.Guardar(inventario);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Inventario/Edit/5
        public ActionResult Edit(int id)
        {  
            return PartialView("~/Views/Admin/PartialViews/Inventario/_Editar.cshtml",repo.Buscar(id));
        }

        // POST: Inventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromBody]Inventarios inventario)
        {
            try
            {
                // TODO: Add update logic here
                repo.Editar(inventario);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Inventario/Delete/5
        public ActionResult Delete(int id)
        {
            repo.Eliminar(id);
            return Ok();
        }

        // POST: Inventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, bool notData)
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

