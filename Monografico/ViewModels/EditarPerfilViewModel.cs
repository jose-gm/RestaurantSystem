﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class EditarPerfilViewModel
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Introduzca el nombre de usuario del empleado")]
        [Remote("IsUserNameInUse", "Usuario", AdditionalFields = "nombreUsuarioActual, isEdicion", ErrorMessage = "Este nombre de usuario no esta disponible")]
        [Display(Name = "Nombre de usuario")]
        public string NombreUsuario { get; set; }
        public string UsuarioActual { get; set; }
        [Required(ErrorMessage = "Introduzca el nombre del empleado")]
        [StringLength(120)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Introduzca el apellido del empleado")]
        [StringLength(120)]
        public string Apellido { get; set; }
        public bool Sexo { get; set; }
        [Required(ErrorMessage = "Introduzca la cedula del empleado")]
        [RegularExpression(@"^[0-9]{3}[-][0-9]{7}[-]{1}[0-9]{1}$", ErrorMessage = "cédula invalido")]
        [StringLength(13)]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Introduzca un numero telefonico")]
        [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Numero invalido")]
        public string Telefono { get; set; }
        public IFormFile Imagen { get; set; }
        public string ImagenEncoded { get; set; }
        [MinLength(6, ErrorMessage = "La contraseña debe contener minimo 6 caracteres")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
        [MinLength(6, ErrorMessage = "La contraseña debe contener minimo 6 caracteres")]
        [Display(Name = "Contraseña nueva")]
        [DataType(DataType.Password)]
        public string Clavenueva { get; set; }
        [MinLength(6, ErrorMessage = "La contraseña debe contener minimo 6 caracteres")]
        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        [Compare(otherProperty: "Clavenueva", ErrorMessage = "Las contraseñas no son iguales")]
        public string confirmarClave { get; set; }
    }
}
