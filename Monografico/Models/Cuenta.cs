using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Cuenta", Schema = "dbo")]
    public class Cuenta
    {
        public int IdCuenta { get; set; }
        public int IdMesa { get; set; } 
        public Orden orden { get; set; } 
    }
}
