using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryZona : RepositoryBase<Zona>
    {
        private readonly Contexto _contexto;

        public RepositoryZona(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
