using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class MesaViewModel
    {
        public int IdMesa { get; set; }
        public int IdZona { get; set; }
        public int Numero { get; set; }
        public int Asientos { get; set; }
        public Zona Zona { get; set; }
        public string ZonaDescripcion { get; set; }
        public string Estado { get; set; }
    }
}
