using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Mesa")]
    public class Mesa
    {
        [Key]
        public int IdMesa { get; set; }
        public int IdZona { get; set; }
        [Required(ErrorMessage = "Debe indicar el numero de la mesa")]
        public int Numero { get; set; }
        public int Asientos { get; set; }
        public virtual List<Cuenta> Cuentas { get; set; }
    }
}
