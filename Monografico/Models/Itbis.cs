using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    public class Itbis
    {
        [Key]
        public int IdItbis { get; set; }
        public int Valor { get; set; }
    }
}
