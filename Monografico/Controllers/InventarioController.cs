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
        RepositoryWrapper repo;
        public InventarioController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        // GET: Inventario
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Inventario/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: Inventario/Create
        public async Task<IActionResult> Create()
        {
            return PartialView("~/Views/Admin/PartialViews/Inventario/_Create.cshtml", new Inventario());
        }

        // POST: Inventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]Inventario inventario)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    await repo.Inventario.Add(inventario);
                }
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: Inventario/Edit/5
        public async Task<IActionResult> Edit(int id)
        {  
            return PartialView("~/Views/Admin/PartialViews/Inventario/_Edit.cshtml", await repo.Inventario.Find(id));
        }

        // POST: Inventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody]Inventario inventario)
        {
            try
            {
                // TODO: Add update logic here
                await repo.Inventario.Update(inventario);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Inventario/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                await repo.Inventario.Remove(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        // POST: Inventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, bool notData)
        {
            try
            {
                // TODO: Add delete logic here
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GET:
        public async Task<JsonResult> List()
        {
            var a= Json(await repo.Inventario.GetListViewModel(x => true));
            return a;
        }
    }
}

