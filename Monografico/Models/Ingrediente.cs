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
    [Table("Ingrediente")]
    public class Ingrediente
    {
        [Key]
        public int IdIngrediente { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public bool LlevaInventario { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Inventario Inventario { get; set; }
    }
}
