using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    public class ProductosCategorias
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string ImagenURL { get; set; }

    }
}
