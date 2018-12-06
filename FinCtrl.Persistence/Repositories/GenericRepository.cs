using FinCtrl.Infrastructure.Contexts;
using FinCtrl.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FinCtrl.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private FinCtrlDbContext _context;

        public GenericRepository(FinCtrlDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(string id)
        {
            var entityToDelete = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entityToDelete);
        }

        public void Delete(int id)
        {
            var entityToDelete = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entityToDelete);
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public T Find(string id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).FirstOrDefault();
        }

        public T Find(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
    }
}
