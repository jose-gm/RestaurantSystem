using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("OrdenDetalle", Schema = "dbo")]
    public class OrdenDetalle
    {
        public int IdDetalle { get; set; }
        public int IdOrden { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
