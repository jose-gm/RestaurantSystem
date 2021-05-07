using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class CierreCajaViewModel
    {
        public decimal MontoInicial { get; set; }
        public decimal Efectivo { get; set; }
        public decimal Tarjeta { get; set; }
        public decimal Cheque { get; set; }
        public decimal MontoCaja { get; set; }
        public decimal TotalContado { get; set; }
        public decimal Diferencia { get; set; }
        public string Cuadre { get; set; }
    }
}
