using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinCtrl.Persistence.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        Task Delete(string id);
        Task<T> Find(string id);
        Task<IEnumerable<T>> GetAll();
        void Edit(T entity);
    }
}
