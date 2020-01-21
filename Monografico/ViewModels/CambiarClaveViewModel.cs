using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class CambiarClaveViewModel
    {
        [Required(ErrorMessage = "Introduzca la contraseña actual")]
        [MinLength(6, ErrorMessage = "La contraseña debe contener minimo 6 caracteres")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
        [Required(ErrorMessage = "Introduzca la nueva contraseña")]
        [MinLength(6, ErrorMessage = "La contraseña debe contener minimo 6 caracteres")]
        [Display(Name = "Contraseña nueva")]
        [DataType(DataType.Password)]
        public string Clavenueva { get; set; }
        [Required(ErrorMessage = "Confirma la contraseña")]
        [MinLength(6, ErrorMessage = "La contraseña debe contener minimo 6 caracteres")]
        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        [Compare(otherProperty: "Clavenueva", ErrorMessage = "Las contraseñas no son iguales")]
        public string confirmarClave { get; set; }
    }
}
