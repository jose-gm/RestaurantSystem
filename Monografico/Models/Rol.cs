using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    public class Rol : IdentityRole<int>
    {
        public List<ApplicationUserRole> UsuarioRoles { get; set; }
    }
}
