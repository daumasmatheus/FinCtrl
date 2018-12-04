using FinCtrl.Domain.Entities;
using FinCtrl.Infrastructure.Contexts;
using FinCtrl.Persistence.Interfaces;

namespace FinCtrl.Persistence.Repositories
{
    public class FinancasRepository : GenericRepository<Financa>, IFinancasRepository
    {
        private readonly FinCtrlDbContext _context;

        public FinancasRepository(FinCtrlDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
