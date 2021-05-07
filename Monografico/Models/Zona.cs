using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    [Table("Zona")]
    public class Zona
    {
        [Key]
        public int IdZona { get; set; }
        [Required(ErrorMessage = "Introduzca una descripcion para la zona")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        public List<Mesa> Mesas { get; set; }
    }
}
