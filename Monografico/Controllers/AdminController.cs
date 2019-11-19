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
        public static List<Inventarios> inventarios;

        public AdminController()
        {
            if(inventarios == null)
            {
                inventarios = new List<Inventarios>()
                {
                    new Inventarios
                    {
                        Id = 1,
                        Descripcion = "Salsa rica",
                        Cantidad = 3,
                        FechaEntrada = DateTime.Now,
                        EsContabilizable = true,
                        Precio = 20,
                        Minimo = 5,
                        Unidad = "ml"
                    },new Inventarios
                    {
                        Id = 1,
                        Descripcion = "Queso jeo",
                        Cantidad = 3,
                        FechaEntrada = DateTime.Now,
                        EsContabilizable = true,
                        Precio = 10,
                        Minimo = 2,
                        Unidad = "lbs"
                    },new Inventarios
                    {
                        Id = 1,
                        Descripcion = "Pan rica",
                        Cantidad = 7,
                        FechaEntrada = DateTime.Now,
                        EsContabilizable = true,
                        Precio = 30,
                        Minimo = 2,
                        Unidad = "slice"
                    }
                };
            }
                   
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inventario()
        {
            return View(inventarios);
        }
    }
}