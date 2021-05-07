using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Orden")]
    public class Orden
    {
        [Key]
        public int IdOrden { get; set; }
        public int IdCuenta { get; set; }
        public bool Enviado { get; set; }
        public DateTime Fecha { get; set; }
        public virtual Cuenta Cuenta { get; set; }
        public virtual List<OrdenDetalle> Detalle { get; set; }
    }
}
