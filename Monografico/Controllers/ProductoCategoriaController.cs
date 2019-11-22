using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Monografico.Controllers
{
    public class ProductoCategoriaController : Controller
    {
        // GET: ProductosCategorias
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductosCategorias/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductosCategorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductosCategorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductosCategorias/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductosCategorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductosCategorias/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductosCategorias/Delete/5
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
    }
}