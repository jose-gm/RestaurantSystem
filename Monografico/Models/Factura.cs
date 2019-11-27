using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Factura", Schema = "dbo")]
    public class Factura
    {
        public int IdFactura { get; set; }
        public int IdCuenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Descuento { get; set; }
    }
}
