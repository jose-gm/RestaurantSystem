using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.ViewModels
{
    public class CuentaViewModel
    {
        public int IdUsuario { get; set; }
        public int IdMesa { get; set; }
        public decimal Total { get; set; }
        public int Mesa { get; set; }
        public string Usuario { get; set; }
        public bool Activa { get; set; }
        public bool IsCajaAbierta { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Cuenta> Cuentas { get; set; }
    }
}
