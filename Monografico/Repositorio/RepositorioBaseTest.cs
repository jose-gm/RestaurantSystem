using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositorioBaseTest<T> : IRepository<T> where T : class
    {
        static List<T> lista;

        public RepositorioBaseTest()
        {
            if(lista == null)
                lista = new List<T>();
        }

        public T Buscar(int id)
        {
            return lista.Find(x => (int)x.GetType().GetProperty("Id").GetValue(x) == id);
        }

        public bool Editar(T entity)
        {
            bool flag = false;
            try
            {
                int id = (int)entity.GetType().GetProperty("Id").GetValue(entity);
                var c = lista.Find(x => (int)x.GetType().GetProperty("Id").GetValue(x) == id);
                int index = lista.IndexOf(c);
                lista[index] = entity;
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        public bool Eliminar(int id)
        {
            bool flag = false;
            try
            {
                var c = lista.Find(x => (int)x.GetType().GetProperty("Id").GetValue(x) == id);
                int index = lista.IndexOf(c);
                lista.RemoveAt(index);
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        public List<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return lista;
        }

        public bool Guardar(T entity)
        {
            bool flag = false;
            try
            {
                lista.Add(entity);

                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
    }
}
