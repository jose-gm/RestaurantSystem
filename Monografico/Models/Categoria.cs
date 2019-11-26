using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Categoria", Schema = "dbo")]
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string ImagenURL { get; set; }
    }
}
