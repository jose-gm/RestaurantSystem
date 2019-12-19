using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    [Bind("IdProducto," +
        "IdCategoria," +
        "Descripcion," +
        "Precio," +
        "Imagen," +
        "ImagenEncoded," +
        "LlevaIngredientes," +
        "LlevaInventario," +
        "TieneInventario," +
        "FechaEntada," +
        "Unidad," +
        "Cantidad")]
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }
        [Display(Name = "Categoria")]
        public int? IdCategoria { get; set; }
        [Required]
        [StringLength(60)]
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public string Categoria { get; set; }
        public IFormFile Imagen { get; set; }
        public string ImagenEncoded { get; set; }

        [Display(Name = "Lleva Ingredientes?")]
        public bool LlevaIngredientes { get; set; }
        [Display(Name = "Lleva Inventario?")]
        public bool LlevaInventario { get; set; }
        public bool TieneInventario { get; set; }
        public DateTime? FechaEntada { get; set; }
        public string Unidad { get; set; }
        public int Cantidad { get; set; }
    }
}
