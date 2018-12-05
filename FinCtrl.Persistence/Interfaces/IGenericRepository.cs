using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FinCtrl.Persistence.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(string id);        
        T Find(string id);
        T Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        void Edit(T entity);
    }
}
