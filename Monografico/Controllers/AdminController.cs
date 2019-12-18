using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inventario()
        {
            return View();
        }
        [BreadCrumb(Title = "Empleado", Url = "/Admin/", Order = 1)]
        public IActionResult Empleado()
        {
            return View();
        }
        [BreadCrumb(Title = "Rol", Url = "/Admin/", Order = 1)]
        public IActionResult Rol()
        {
            return View();
        }
        public IActionResult Zona()
        {
            return View();
        }

        public IActionResult Mesa()
        {
            return View();
        }

        public IActionResult Producto()
        {
            return View();
        }

        public IActionResult Categoria()
        {
            return View();
        }

        public IActionResult Ingrediente()
        {
            return View();
        }

       

        public async Task<IActionResult> SeleccionMesa()
        {
            return View(await repo.Zona.GetAllWithMesa());
        }
    }
}