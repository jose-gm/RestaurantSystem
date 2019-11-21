using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [BindProperties(SupportsGet = true)]
    public class Inventarios
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string? Descripcion { get; set; }
        public int? Cantidad { get; set; }
        public Decimal? Precio { get; set; }
        [Display(Name = "Es Contabilizable?")]
        public bool EsContabilizable { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaEntrada { get; set; }
        public int? Minimo { get; set; }
        [StringLength(10)]
        public string? Unidad { get; set; }
    }
}
