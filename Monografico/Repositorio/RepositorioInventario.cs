using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositorioInventario : RepositorioBase<Inventarios>
    {
        public RepositorioInventario(Contexto contexto) : base(contexto)
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

        public override bool Modificar(Inventarios entity)
        {
            return base.Modificar(entity);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
