using FinCtrl.Infrastructure.Contexts;
using FinCtrl.Persistence.Interfaces;
using FinCtrl.Persistence.Repositories;

namespace FinCtrl.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinCtrlDbContext _context;

        public IFinancasRepository financasRepository { get; private set; }
        public ITipoRepository tipoRepository { get; private set; }

        public UnitOfWork(FinCtrlDbContext context)
        {
            _context = context;

            financasRepository = new FinancasRepository(_context);
            tipoRepository = new TipoRepository(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }        
    }
}
