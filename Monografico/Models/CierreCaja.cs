using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("CierreCaja")]
    public class CierreCaja
    {
        [Key]
        public int IdCierreCaja { get; set; }
        public int IdCaja { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Efectivo { get; set; }
        public decimal Tarjeta { get; set; }
        public decimal Cheque { get; set; }
        public decimal MontoCaja { get; set; }
        public decimal TotalContado { get; set; }
        public decimal Diferencia { get; set; }
        public string Cuadre { get; set; }
    }
}
