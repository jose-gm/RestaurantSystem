using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class CajaViewModel
    {
        public DateTime FechaApertura { get; set; }
        public string FechaCierre { get; set; }
        public string Usuario { get; set; }
        public decimal MontoInicial { get; set; }
        public decimal Efectivo { get; set; }
        public decimal Tarjeta { get; set; }
        public decimal Cheque { get; set; }
        public decimal Diferencia { get; set; }
        public string Estado { get; set; }
        public string Cuadre { get; set; }
    }
}
