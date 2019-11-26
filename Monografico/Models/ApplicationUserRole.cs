using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Models
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public Usuario User { get; set; }
        public Rol Role { get; set; }
    }
}
