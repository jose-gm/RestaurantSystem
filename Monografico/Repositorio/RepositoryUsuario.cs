using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryUsuario : RepositoryBase<Usuario>
    {
        private readonly Contexto _contexto;

        public RepositoryUsuario(Contexto contexto): base(contexto)
        {
            _contexto = contexto;
        }
    }
}
