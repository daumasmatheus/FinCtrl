using FinCtrl.Infrastructure.Contexts;
using FinCtrl.Persistence.Interfaces;
using FinCtrl.Persistence.Repositories;
using System.Threading.Tasks;

namespace FinCtrl.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinCtrlDbContext _context;

        public IFinancasRepository financasRepository { get; private set; }

        public UnitOfWork(FinCtrlDbContext context)
        {
            _context = context;

            financasRepository = new FinancasRepository(_context);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }        
    }
}
