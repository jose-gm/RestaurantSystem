using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    public class Productos
    {
        [Key]
        public int Id { get; set; }
        public int IdProductosCategorias { get; set; }
        [StringLength(60)]
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public string ImagenURL { get; set; }

        public Productos ()
        {
            Id = 0;
            IdProductosCategorias = 0;
            Descripcion = string.Empty;
            Precio = 0;
            ImagenURL = string.Empty;
        }

        public Productos (int Id, int IdProductosCategorias, string Descripcion, Decimal Precio, string ImagenUrl)
        {
            this.Id = Id;
            this.IdProductosCategorias = IdProductosCategorias;
            this.Descripcion = Descripcion;
            this.Precio = Precio;
            this.ImagenURL = ImagenURL;
        }
    }
}
