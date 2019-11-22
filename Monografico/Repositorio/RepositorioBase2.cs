using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositorioBase2 : IRepository<Inventarios>
    {
        public static List<Inventarios> inventarios;

        public RepositorioBase2()
        {
            if (inventarios == null)
            {
                inventarios = new List<Inventarios>()
                {
                    new Inventarios
                    {
                        Id = 1,
                        Descripcion = "Salsa rica",
                        Cantidad = 3,
                        FechaEntrada = DateTime.Now.Date,
                        EsContabilizable = true,
                        Precio = 20,
                        Minimo = 5,
                        Unidad = "ml"
                    },new Inventarios
                    {
                        Id = 2,
                        Descripcion = "Queso jeo",
                        Cantidad = 3,
                        FechaEntrada = DateTime.Now.Date,
                        EsContabilizable = true,
                        Precio = 10,
                        Minimo = 2,
                        Unidad = "lbs"
                    },new Inventarios
                    {
                        Id = 3,
                        Descripcion = "Pan rica",
                        Cantidad = 7,
                        FechaEntrada = DateTime.Now.Date,
                        EsContabilizable = true,
                        Precio = 30,
                        Minimo = 2,
                        Unidad = "slice"
                    }
                };
            }
        }
        public virtual Inventarios Buscar(int id)
        {
            return inventarios.Find(x => x.Id == id);
        }

        public virtual bool Editar(Inventarios entity)
        {
            bool flag = false;
            try
            {
                var c = inventarios.Find(x => x.Id == entity.Id);
                int index = inventarios.IndexOf(c);
                inventarios[index] = entity;
                flag = true;
            }
            catch (Exception)
            {             
                throw;
            }
            return flag;
        }

        public virtual bool Eliminar(int id)
        {
            bool flag = false;
            try
            {
                var c = inventarios.Find(x => x.Id == id);
                int index = inventarios.IndexOf(c);
                inventarios.RemoveAt(index);
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        public virtual List<Inventarios> GetList(Expression<Func<Inventarios, bool>> expression)
        {
            return inventarios;
        }

        public virtual bool Guardar(Inventarios entity)
        {
            bool flag = false;
            try
            {
                inventarios.Add(entity);

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
