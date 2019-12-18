using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.Models
{
    public class AppSettings
    {
        public string StrConn { get; set; }
        public string Nombre { get; set; }
        public string NombreAbreviado { get; set; }
        public string RutaImagenesUsers { get; set; }
        public string NombreCompania { get; set; }
        public string EnProduccion { get; set; }
        public string UserSinFoto { get; set; }
        public string UrlFotosUsuario { get; set; }
    }
}
