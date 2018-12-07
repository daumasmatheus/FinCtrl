using FinCtrl.Application.Interfaces.Tipos;
using FinCtrl.Domain.Entities;
using FinCtrl.Persistence.Interfaces;
using System.Collections.Generic;

namespace FinCtrl.Application.Services.Tipos
{
    public class TiposService : ITiposService
    {
        private readonly IUnitOfWork _uow;

        public TiposService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<Tipo> GetTipos()
        {
            return _uow.tipoRepository.GetAll();
        }
    }
}
