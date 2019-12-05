using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.Models
{
    public class Usuario : IdentityUser<int>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool Sexo { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public List<ApplicationUserRole> UsuarioRoles { get; set; }
    }
}
