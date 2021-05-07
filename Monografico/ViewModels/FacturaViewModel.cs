using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class FacturaViewModel
    {
        public int IdFactura { get; set; }
        public int IdCuenta { get; set; }
        public int? IdMesa { get; set; }
        public decimal Monto { get; set; }
        public decimal Descuento { get; set; }
        public decimal Itbis { get; set; }
        public decimal PorcientoLey { get; set; }
        public int Mesa { get; set; }
        [Display(Name = "Empleado")]
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        [Display(Name = "Metodo de pago")]
        public string MetodoPago { get; set; }
        public decimal Pago { get; set; }
        public string Tarjeta { get; set; }
        public string TipoTarjeta { get; set; }
        public string Cheque { get; set; }
        public List<OrdenViewModel> Ordenes { get; set; }
    }
}
