﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class IngredienteViewModel
    {
        public int IdIngrediente { get; set; }
        [Required(ErrorMessage = "Introduzca la descripcion del ingrediente")]
        [Display(Name = "Descripción")]
        [StringLength(100)]
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        [Display(Name = "¿Lleva Inventario?")]
        public bool LlevaInventario { get; set; }
        public bool TieneInventario { get; set; }
        [Display(Name = "Fecha de entrada")]
        [DataType(DataType.Date)]
        public DateTime FechaEntrada { get; set; }
        [Required(ErrorMessage = "Introduzca la uniddad de medida del ingrediente")]
        public string Unidad { get; set; }
        public int Cantidad { get; set; }
    }
}
