using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Zona", Schema = "dbo")]
    public class Zona
    {
        public int IdZona { get; set; }
        public string Descripcion { get; set; }
    }
}
