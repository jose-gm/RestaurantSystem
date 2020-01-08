using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class CategoriaViewModel
    {
        public int IdCategoria { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Introduzca una desripcion para la categoria")]
        public string Descripcion { get; set; }
        public IFormFile Imagen { get; set; }
        public string ImagenEncoded { get; set; }
    }
}
