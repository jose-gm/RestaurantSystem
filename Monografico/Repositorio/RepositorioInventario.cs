using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositorioInventario : RepositorioBase2
    {
        public RepositorioInventario(Contexto contexto) : base()
        {
        }

        public override Inventarios Buscar(int id)
        {
            return base.Buscar(id);
        }

        public override bool Eliminar(int id)
        {
            return base.Eliminar(id);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Guardar(Inventarios entity)
        {
            return base.Guardar(entity);
        }

        public override bool Editar(Inventarios entity)
        {
            return base.Editar(entity);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override List<Inventarios> GetList(Expression<Func<Inventarios, bool>> expression)
        {
            return base.GetList(expression);
        }
    }
}
