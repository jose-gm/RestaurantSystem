﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    [Bind("Nombre,Apellido,Sexo,Cedula,Direccion,NombreUsuario,Email,Roles,Clave,Telefono")]
    public class UsuarioViewModel
    {
        public int IdUsuarioViewModel { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(120)]
        public string Apellido { get; set; }
        public bool Sexo { get; set; }
        [Required]
        [StringLength(13)]
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
     
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
        [Display(Name = "Roles")]
        public string RoleName { get; set; }
        public int Roles { get; set; }
        public UsuarioViewModel()
        {
            IdUsuarioViewModel = 0;
            Roles = 0;
        }
    }
}
