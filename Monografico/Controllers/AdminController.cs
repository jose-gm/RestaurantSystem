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
    [Authorize(Roles = "Administrador, Mesero, Cajero, Host")]
    public class AdminController : Controller
    {
       
        RepositoryWrapper repo;

        public AdminController(RepositoryWrapper _repo)
        {
            repo = _repo;
            ViewBag.AlterMenu = false;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Mesero") || User.IsInRole("Cajero"))
                return RedirectToAction("SeleccionMesa", "Admin");
            
            if (User.IsInRole("Host"))
                return RedirectToAction("ListaMesas", "Admin");

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
        
        public async Task<IActionResult> CambiarClave()
        {
            return View();
        }

        [Authorize(Roles = "Administrador,Mesero, Cajero")]
        public async Task<IActionResult> SeleccionMesa()
        {
            var usuario = await repo.Usuario.GetUsuario(HttpContext.User);
            ViewBag.IdUsuario = usuario.Id;
            ViewBag.IsCajaAbierta = await repo.Caja.IsCajaAbierta();
            return View(await repo.Zona.GetAllWithMesa());
        }
        
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> ListaMesas()
        {
            ViewBag.Zonas = await repo.Zona.GetSelectList();
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Reportes()
        {
            var model = await repo.Factura.GetProductoMasVendidosAsViewModel();
            model.MontoMensuales = await repo.Factura.ListOfMontoPerMonth();
            model.HoraMasVendida = await repo.Factura.HoraMasVendida();
            model.ZonaMayorVenta = await repo.Factura.ZonaMayorVenta();
            model.MeseroMayorVenta = await repo.Factura.MeseroMayorVenta();
            model.MesaroMayorVenta = await repo.Factura.MesaMayorVenta();
            return View(model);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ReporteProducto()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ReporteInventario()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ReporteFactura()
        {
            ViewBag.Usuarios = await repo.Usuario.GetSelectList();
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ReporteCaja()
        {
            ViewBag.Usuarios = await repo.Usuario.GetSelectList();
            return View();
        }
        
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Itbis()
        {
            return View();
        }
        
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Configuracion()
        {
            return View(await repo.Configuracion.Get());
        }
    }
}