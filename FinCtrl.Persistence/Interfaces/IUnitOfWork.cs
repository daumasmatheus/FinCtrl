namespace FinCtrl.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        IFinancasRepository financasRepository { get; }
        ITipoRepository tipoRepository { get; }

        void SaveChanges();
    }
}
