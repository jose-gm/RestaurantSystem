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
        public int IdCuenta { get; set; }
        public int IdMesa { get; set; }
        public int IdOrden { get; set; }
        public decimal Total { get; set; }
        public string Mesa { get; set; }
        public bool Activa { get; set; }
        public List<Categoria> Categorias { get; set; }
    }
}
