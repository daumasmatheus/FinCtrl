using FinCtrl.Domain.Entities;
using FinCtrl.Infrastructure.Contexts;
using FinCtrl.Persistence.Interfaces;

namespace FinCtrl.Persistence.Repositories
{
    public class TipoRepository : GenericRepository<Tipo> , ITipoRepository
    {
        private readonly FinCtrlDbContext _context;

        public TipoRepository(FinCtrlDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
