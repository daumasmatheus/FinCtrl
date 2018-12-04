using System.Threading.Tasks;

namespace FinCtrl.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        IFinancasRepository financasRepository { get; }

        Task SaveChanges();
    }
}
