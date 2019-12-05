using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryOrden : RepositoryBase<Orden>
    {
        private readonly Contexto _contexto;

        public RepositoryOrden(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
