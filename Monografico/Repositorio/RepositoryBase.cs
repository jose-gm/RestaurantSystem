using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryBase<T> : IDisposable, IRepository<T> where T : class
    {
        internal Contexto _contexto;

        public RepositoryBase(Contexto contexto)
        {
            _contexto = contexto;
        }

        /// <summary>
        /// Permite guardar una entidad en la base de datos
        /// </summary>
        /// <param name="entity">Una instancia de la entidad a guardar</param>
        /// <returns>Retorna True si guardo o Falso si falló </returns>
        public async virtual Task<bool> Add(T entity)
        {
            bool paso = false;
            try
            {
                await _contexto.Set<T>().AddAsync(entity);
                await _contexto.SaveChangesAsync();
                paso = true;
                
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        /// <summary>
        /// Permite Modificar una entidad en la base de datos 
        /// </summary>
        /// <param name="entity">Una instancia de la entidad a guardar</param>
        /// <returns>Retorna True si Modifico o Falso si falló </returns>
        public async virtual Task<bool> Update(T entity)
        {
            bool paso = false;
            try
            {
                _contexto.Set<T>().Attach(entity);
                _contexto.Entry(entity).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();             
                paso = true;
                
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        /// <summary>
        /// Permite Eliminar una entidad en la base de datos
        /// </summary>
        ///<param name="id">El Id de la entidad que se desea eliminar </param>
        /// <returns>Retorna True si Eliminó o Falso si falló </returns>
        public async virtual Task<bool> Remove(int id)
        {
            bool paso = false;
            try
            {
                T entity = _contexto.Set<T>().Find(id);
                _contexto.Set<T>().Remove(entity);

                await _contexto.SaveChangesAsync();
                paso = true;

                _contexto.Dispose();
            }
            catch (Exception)
            { throw; }
            return paso;
        }

        /// <summary>
        /// Permite Buscar una entidad en la base de datos
        /// </summary>
        ///<param name="id">El Id de la entidad que se desea encontrar </param>
        /// <returns>Retorna la persona encontrada </returns>
        public async virtual Task<T> Find(int id)
        {
            T entity;
            try
            {
                entity = await _contexto.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }

        /// <summary>
        /// Permite extraer una lista de Personas de la base de datos
        /// </summary> 
        ///<param name="expression">Expression Lambda conteniendo los filtros de busqueda </param>
        ///// <returns>Retorna una lista de entidades</returns>
        public async virtual Task<List<T>> GetList(Expression<Func<T, bool>> expression)
        {
            List<T> lista = new List<T>();
            try
            {
                lista = await _contexto.Set<T>().Where(expression).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        
       

        public void Dispose()
        {
            _contexto.Dispose();
        }

    }
}
