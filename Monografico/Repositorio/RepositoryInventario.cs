using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryInventario : RepositoryBase<Inventario>
    {
        public RepositoryInventario(Contexto contexto) : base(contexto)
        {
        }
    }
}
