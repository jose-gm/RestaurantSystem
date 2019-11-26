using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Producto", Schema = "dbo")]
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public int IdProductosCategorias { get; set; }
        [StringLength(60)]
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public string ImagenURL { get; set; }
    }
}
