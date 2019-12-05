using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryRol : RepositoryBase<Rol>
    {
        private readonly Contexto _contexto;

        public RepositoryRol(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
