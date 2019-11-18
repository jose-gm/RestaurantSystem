using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    public class Inventarios
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public bool EsContabilizable { get; set; }
        public DateTime FechaEntrada { get; set; }
        public int Minimo { get; set; }
        [StringLength(10)]
        public string Unidad { get; set; }
    }
}
