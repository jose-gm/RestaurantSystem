using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Caja")]
    public class Caja
    {
        [Key]
        public int IdCaja { get; set; }
        public int IdUsario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public virtual CierreCaja Cierre { get; set; }
        public virtual AperturaCaja Apertura { get; set; }
    }
}
