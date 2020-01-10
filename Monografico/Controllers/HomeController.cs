using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;
using Monografico.Repositorio;
using Monografico.ViewModels;

namespace Monografico.Controllers
{
    public class HomeController : Controller
    {
        RepositoryWrapper repo;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public HomeController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, RepositoryWrapper _repo)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            repo = _repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = "")
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Usuario, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Usuario);
                    var esMesero = await _userManager.IsInRoleAsync(user, "Mesero");

                    if (esMesero)
                        model.ReturnUrl = Url.Action("SeleccionMesa", "Admin");
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }

            ModelState.AddModelError("", "Usuario/Contraseña Inválidos");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }


        [Authorize]
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

        //[HttpPost]
        //public async Task<IActionResult> EditPerfil(UsuarioViewModel model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(model.Usuario, model.Password, model.RememberMe, false);

        //        if (result.Succeeded)
        //        {
        //            var user = await _userManager.FindByNameAsync(model.Usuario);
        //            var esMesero = await _userManager.IsInRoleAsync(user, "Mesero");

        //            if (esMesero)
        //                model.ReturnUrl = Url.Action("SeleccionMesa", "Admin");
        //            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        //            {
        //                return LocalRedirect(model.ReturnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "Admin");
        //            }
        //        }
        //    }

        //    ModelState.AddModelError("", "Usuario/Contraseña Inválidos");
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioViewModel usuario)
        {
            try
            {
                // TODO: Add update logic here
                if (!(await repo.Usuario.UpdateUsuario(usuario)))
                    return Ok();

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest(false);
        }


        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}