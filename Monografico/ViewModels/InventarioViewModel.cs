using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class InventarioViewModel
    {
        public int IdInventario { get; set; }
        public int IdProducto { get; set; }
        public int IdIngrediente { get; set; }
        [Display(Name = "Fecha entrada")]
        [DataType(DataType.Date)]
        public DateTime? FechaEntrada { get; set; }
        public int Cantidad { get; set; }
        [StringLength(10)]
        public string Unidad { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
