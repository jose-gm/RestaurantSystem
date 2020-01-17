using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public int? IdCategoria { get; set; }
        [StringLength(60)]
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public Decimal Costo { get; set; }
        public string Imagen { get; set; }
        public bool LlevaIngredientes { get; set; }
        public bool LlevaInventario { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Inventario Inventario { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Categoria Categoria { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductoDetalle> Detalle { get; set; }
    }
}
