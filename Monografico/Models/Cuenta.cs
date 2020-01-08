using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Cuenta")]
    public class Cuenta
    {
        [Key]
        public int IdCuenta { get; set; }
        public int IdUsuario { get; set; }
        public int IdMesa { get; set; } 
        public bool Activa { get; set; } 
        public virtual Mesa Mesa { get; set; } 
        public virtual List<Orden> Ordenes { get; set; } 
    }
}
