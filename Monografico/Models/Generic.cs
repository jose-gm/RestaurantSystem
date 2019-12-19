using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.Models
{
    public class Generic
    {
        public string ID { get; set; }
        public string Descripcion { get; set; }

        public Generic()
        {
            ID = string.Empty;
            Descripcion = string.Empty;
        }
    }
}
