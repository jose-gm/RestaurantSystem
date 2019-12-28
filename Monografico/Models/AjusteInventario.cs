using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("AjusteInventario")]
    public class AjusteInventario
    {
        [Key]
        public int IdAjuste { get; set; }
        public int IdInventario { get; set; }
        public int CantidadAnterior { get; set; }
        [Display(Name = "Cantidad a justar")]
        public int CantidadActual { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }

    }
}
