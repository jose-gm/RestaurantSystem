using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;

namespace Monografico.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly SignInManager<Usuario> _signInManager;

        public HomeController(SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}