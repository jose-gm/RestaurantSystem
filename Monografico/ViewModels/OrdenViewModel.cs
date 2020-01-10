using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class OrdenViewModel
    {
        public int IdCuenta { get; set; }
        public int IdOrden { get; set; }
        public int IdDetalle { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public bool Enviado { get; set; }
        public int Mesa { get; set; }
    }
}
