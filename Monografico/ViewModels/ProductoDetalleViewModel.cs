using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class ProductoDetalleViewModel
    {
        public int IdDetalle { get; set; }
        public int IdProducto { get; set; }
        public int IdIngrediente { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal Costo { get; set; }
        //public List<Ingrediente> Ingredientes { get; set; }
    }
}
