using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FinCtrl.Persistence.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        Task Delete(string id);
        Task<T> Find(string id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        void Edit(T entity);
    }
}
