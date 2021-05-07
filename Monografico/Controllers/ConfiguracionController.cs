using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monografico.Models;
using Monografico.Repositorio;

namespace Monografico.Controllers
{
    [Authorize(Roles = "Administrador, Host")]
    public class ConfiguracionController : Controller
    {

        RepositoryWrapper repo;

        public ConfiguracionController(RepositoryWrapper _repo)
        {
            repo = _repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromBody]Configuracion configuracion)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if(await repo.Configuracion.Update(configuracion))
                        return Ok();
                }
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }
    }
}