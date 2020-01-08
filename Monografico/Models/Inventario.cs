using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    /*[Bind("IdInventario," +
        "IdProducto," +
        "IdIngrediente," +
        "FechaEntrada," +
        "Cantidad," +
        "Unidad")]*/
    [Table("Inventario")]
    public class Inventario
    {
        [Key]
        public int IdInventario { get; set; }
        public int? IdProducto { get; set; }
        public int? IdIngrediente { get; set; }
        [Display(Name = "Fecha de entrada")]
        [DataType(DataType.Date)]
        public DateTime? FechaEntrada { get; set; }
        public int Cantidad { get; set; }
        [StringLength(10)]
        public string Unidad { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Producto Producto { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Ingrediente Ingrediente { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<AjusteInventario> AjusteInventarios { get; set; }
    }
}
