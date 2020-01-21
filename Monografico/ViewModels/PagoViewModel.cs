using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class PagoViewModel
    {
        public int IdFactura { get; set; }
        public int IdCuenta { get; set; }
        public string MetodoPago { get; set; }
        public int Pago { get; set; }
        public string Tarjeta { get; set; }
        public string TipoTarjeta { get; set; }
    }
}
