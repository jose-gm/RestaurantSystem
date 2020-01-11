using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;
using Monografico.Repositorio;
using Monografico.ViewModels;

namespace Monografico.Controllers
{
    [Authorize(Roles = "Administrador, Mesero")]
    public class AdminController : Controller
    {
       
        RepositoryWrapper repo;

        public AdminController(RepositoryWrapper _repo)
        {
            repo = _repo;         
        }

        private decimal[] ListOfMontosPerWeek()
        {
            List<Factura> facturas = new List<Factura>() { 
                new Factura(){ Monto = 100, Fecha = new DateTime(2020,1,1) },
                new Factura(){ Monto = 5000, Fecha = new DateTime(2020,1,1) },
                new Factura(){ Monto = 700, Fecha = new DateTime(2020,1,1) },
                
                new Factura(){ Monto = 1300, Fecha = new DateTime(2020,1,2) },
                new Factura(){ Monto = 4000, Fecha = new DateTime(2020,1,2) },
                new Factura(){ Monto = 700, Fecha = new DateTime(2020,1,2) },

                new Factura(){ Monto = 700, Fecha = new DateTime(2020,1,3) },

                new Factura(){ Monto = 700, Fecha = new DateTime(2020,1,4) },

                new Factura(){ Monto = 7030, Fecha = new DateTime(2020,1,5) },
                new Factura(){ Monto = 8900, Fecha = new DateTime(2020,1,5) },

                new Factura(){ Monto = 700, Fecha = new DateTime(2020,1,6) },
                new Factura(){ Monto = 2333, Fecha = new DateTime(2020,1,6) },
                new Factura(){ Monto = 4500, Fecha = new DateTime(2020,1,6) },

                new Factura(){ Monto = 700, Fecha = new DateTime(2020,1,7) },
                new Factura(){ Monto = 700, Fecha = new DateTime(2020,1,7) },

                new Factura(){ Monto = 7400, Fecha = new DateTime(2020,1,8) },
                new Factura(){ Monto = 1200, Fecha = new DateTime(2020,1,8) },

                new Factura(){ Monto = 500, Fecha = new DateTime(2020,1,9) },
                new Factura(){ Monto = 100, Fecha = new DateTime(2020,1,9) },
                new Factura(){ Monto = 9000, Fecha = new DateTime(2020,1,9) },
                new Factura(){ Monto = 3400, Fecha = new DateTime(2020,1,9) },

                new Factura(){ Monto = 9000, Fecha = new DateTime(2020,1,10) },
                new Factura(){ Monto = 1200, Fecha = new DateTime(2020,1,10) },
                new Factura(){ Monto = 5600, Fecha = new DateTime(2020,1,10) },
                new Factura(){ Monto = 7300, Fecha = new DateTime(2020,1,10) },

                new Factura(){ Monto = 7653, Fecha = new DateTime(2020,1,11) },
                new Factura(){ Monto = 8800, Fecha = new DateTime(2020,1,11) },
                new Factura(){ Monto = 3456, Fecha = new DateTime(2020,1,11) },

                new Factura(){ Monto = 7200, Fecha = new DateTime(2020,1,12) },
                new Factura(){ Monto = 3500, Fecha = new DateTime(2020,1,12) },
            };

            var weekMontos = facturas.Where(x => GetWeekNumber(x.Fecha) == GetWeekNumber(DateTime.Today))
                                     .GroupBy(x => x.Fecha.DayOfWeek)
                                     .Select(x => new { Monto = x.Sum(y => y.Monto), Dia = (int)x.Key })
                                     .OrderBy(x => (int)x.Dia)
                                     .ToList();

            var w = new decimal[7];
            weekMontos.ForEach(x => w[x.Dia] += x.Monto);
            for(int i=0; i<w.Length-1; i++)
            {
                var r = w[i];
                w[i] = w[i + 1];
                w[i + 1] = r;
            }

            return w;
        }

        public int GetWeekNumber(DateTime date)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekNum;
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

            var montos = await repo.Factura.ListOfMontoPerWeek();

            ViewModel model = new ViewModel() { 
                CantidadProducto = productos.Count,
                CantidadIngrediente = ingredientes.Count,
                TotalDia = totaldia,
                MontoSemana = montos
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

        public async Task<IActionResult> EditPerfil()
        {

            var usuario = await repo.Usuario.GetUsuario(User);
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel()
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Sexo = usuario.Sexo,
                Telefono = usuario.PhoneNumber,
                Cedula = usuario.Cedula,
                Direccion = usuario.Direccion,
                ImagenEncoded = usuario.Imagen,
                NombreUsuario = usuario.UserName,
                IdUsuario = usuario.Id


            };
            return View(usuarioViewModel);
        }

        [Authorize(Roles = "Administrador,Mesero")]
        public async Task<IActionResult> SeleccionMesa()
        {
            return View(await repo.Zona.GetAllWithMesa());
        }
    }
}