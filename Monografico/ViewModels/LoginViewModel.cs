using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    }
}
