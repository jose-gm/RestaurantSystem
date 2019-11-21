using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;

namespace Monografico.Controllers
{
    public class InventarioController : Controller
    {
        public static List<Inventarios> inventarios;

        public InventarioController()
        {
            if (inventarios == null)
            {
                inventarios = new List<Inventarios>()
                {
                    new Inventarios
                    {
                        Id = 1,
                        Descripcion = "Salsa rica",
                        Cantidad = 3,
                        FechaEntrada = DateTime.Now.Date,
                        EsContabilizable = true,
                        Precio = 20,
                        Minimo = 5,
                        Unidad = "ml"
                    },new Inventarios
                    {
                        Id = 2,
                        Descripcion = "Queso jeo",
                        Cantidad = 3,
                        FechaEntrada = DateTime.Now.Date,
                        EsContabilizable = true,
                        Precio = 10,
                        Minimo = 2,
                        Unidad = "lbs"
                    },new Inventarios
                    {
                        Id = 3,
                        Descripcion = "Pan rica",
                        Cantidad = 7,
                        FechaEntrada = DateTime.Now.Date,
                        EsContabilizable = true,
                        Precio = 30,
                        Minimo = 2,
                        Unidad = "slice"
                    }
                };
            }
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
            return View();
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
                    inventarios.Add(inventario);
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
            return PartialView("~/Views/Admin/PartialViews/Inventario/_Editar.cshtml",inventarios[id-1]);
        }

        // POST: Inventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit([FromBody]Inventarios inventario)
        {
            try
            {
                // TODO: Add update logic here
                var index = inventarios.Find(x => x.Id == inventario.Id).Id;
                inventarios[index - 1] = inventario;
                return Json(inventarios);
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Json(inventarios);
                //return View("~/Views/Admin/Inventario.cshtml");
            }
        }

        // GET: Inventario/Delete/5
        public ActionResult Delete(int id)
        {
            var entity = inventarios.Find(x => x.Id == id);
            inventarios.Remove(entity);
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
            return Json(inventarios);
        }
    }
}

