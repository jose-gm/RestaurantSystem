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
        public int Id { get; set; }
        public int IdInventario { get; set; }
        public int CantidadAnterior { get; set; }
        public int CantidadActual { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
    }
}
