using FinCtrl.Domain.Entities;
using System.Collections.Generic;

namespace FinCtrl.Application.Interfaces.Tipos
{
    public interface ITiposService
    {
        IEnumerable<Tipo> GetTipos();
    }
}