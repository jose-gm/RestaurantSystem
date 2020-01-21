using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("AperturaCaja")]
    public class AperturaCaja
    {
        [Key]
        public int IdAperturaCaja { get; set; }
        public int IdCaja { get; set; }
        public int IdUsurio { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoInicial { get; set; }
        public virtual Caja Caja { get; set; }
    }
}
