using FinCtrl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FinCtrl.Application.Interfaces.Financas
{
    public interface IFinancasServices
    {
        bool Add(Financa financa);
        bool Delete(string Id);
        bool Edit(Financa financa);
        Financa Find(Expression<Func<Financa, bool>> Predicate);
        Financa Find(string Id);
        IEnumerable<Financa> GetFinancas();
        IEnumerable<Financa> GetFinancasByType(int tipoId);
        decimal GetSumOfAllValoresByTipo(int tipoFinanca);
    }
}