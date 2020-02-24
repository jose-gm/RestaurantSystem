using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    public class Configuracion
    {
        [Key]
        public int IdConfiguracion { get; set; }    
        public int PorcientoLey { get; set; }    
    }
}
