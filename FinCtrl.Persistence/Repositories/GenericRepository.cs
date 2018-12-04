using FinCtrl.Infrastructure.Contexts;
using FinCtrl.Persistence.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

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

        public async Task Delete(string id)
        {
            var entityToDelete = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entityToDelete);
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> Find(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
