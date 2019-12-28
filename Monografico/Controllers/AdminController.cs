using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;
using Monografico.Repositorio;
using Monografico.ViewModels;

namespace Monografico.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
       
        RepositoryWrapper repo;

        public AdminController(RepositoryWrapper _repo)
        {
            repo = _repo;
           
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Mesero"))
                return RedirectToAction("SeleccionMesa", "Admin");

            var productos = await repo.Producto.GetList(x => true);
            var ingredientes = await repo.Ingrediente.GetList(x => true);
            var facturas = await repo.Factura.GetList(x => x.Fecha.Date == DateTime.Now.Date);
            decimal totaldia = 0;
            facturas.ForEach(x => totaldia += x.Monto);

            ViewModel model = new ViewModel() { 
                CantidadProducto = productos.Count,
                CantidadIngrediente = ingredientes.Count,
                TotalDia = totaldia
            };
            return View(model);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Inventario()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult AjusteInventario()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Empleado()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Zona()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Mesa()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Producto()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Categoria()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Ingrediente()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Factura()
        {
            return View();
        }

        [Authorize(Roles = "Administrador,Mesero")]
        public async Task<IActionResult> SeleccionMesa()
        {
            return View(await repo.Zona.GetAllWithMesa());
        }
    }
}