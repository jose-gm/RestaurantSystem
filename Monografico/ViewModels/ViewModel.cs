using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class ViewModel
    {
        public int CantidadProducto { get; set; }
        public int CantidadIngrediente { get; set; }
        public decimal TotalDia { get; set; }
        public decimal[] MontoMensuales { get; set; }
        public decimal[] MontoSemana { get; set; }
    }
}
