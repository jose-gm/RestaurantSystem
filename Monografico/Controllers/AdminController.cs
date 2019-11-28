using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;

namespace Monografico.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inventario()
        {
            return View();
        }
        
        public IActionResult Empleado()
        {
            return View();
        }
        
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
    }
}