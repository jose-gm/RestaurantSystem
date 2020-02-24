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
                var result = await _signInManager.PasswordSignInAsync(model.Usuario, model.Password, true, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Usuario);
                    var rol = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    if (rol.Equals("Mesero") || rol.Equals("Cajero"))
                        model.ReturnUrl = Url.Action("SeleccionMesa", "Admin");
                    if (rol.Equals("Host"))
                        model.ReturnUrl = Url.Action("ListaMesas", "Admin");
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

        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}