using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class ReportesViewModel
    {
        public List<string> Productos { get; set; }
        public List<int> ProductosId { get; set; }
        public decimal[] MontoMensuales { get; set; }
        public int HoraMasVendida { get; set; }
        public string ZonaMayorVenta { get; set; }
        public string MeseroMayorVenta { get; set; }
        public string MesaroMayorVenta { get; set; }
    }
}
