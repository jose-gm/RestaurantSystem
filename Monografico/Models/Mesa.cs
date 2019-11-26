using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Mesa", Schema = "dbo")]
    public class Mesa
    {
        public int IdMesa { get; set; }
        public int IdZona { get; set; }
        public string Descripcion { get; set; }
        public int Asientos { get; set; }
    }
}
