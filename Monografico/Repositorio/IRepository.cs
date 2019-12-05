using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetList(Expression<Func<T, bool>> expression);
        Task<T> Find(int id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Remove(int id);
    }
}
