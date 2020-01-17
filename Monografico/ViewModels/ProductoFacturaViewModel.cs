using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class ProductoFacturaViewModel
    {
        public int IdProducto { get; set; }
        public int IdFactura { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public decimal Total { get; set; }
    }
}
