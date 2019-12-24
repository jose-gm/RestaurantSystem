using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class FacturaViewModel
    {
        public int IdFactura { get; set; }
        public int IdCuenta { get; set; }
        public int IdMesa { get; set; }
        public decimal Monto { get; set; }
        public decimal Descuento { get; set; }
        public string Mesa { get; set; }
        public List<OrdenViewModel> Ordenes { get; set; }
    }
}
