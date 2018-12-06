﻿using FinCtrl.Domain.Entities;
using FinCtrl.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FinCtrl.Application.Services.Financas
{
    public class FinancasServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public FinancasServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Financa> GetFinancas()
        {
            return _unitOfWork.financasRepository.GetAll();
        }

        public Financa Find(string Id)
        {
            if (String.IsNullOrEmpty(Id) || String.IsNullOrWhiteSpace(Id))
                throw new ArgumentNullException("Identificador não informado.");

            return _unitOfWork.financasRepository.Find(Id);
        }

        public Financa Find(Expression<Func<Financa, bool>> Predicate)
        {
            return _unitOfWork.financasRepository.Find(Predicate);
        }

        public bool Delete(string Id)
        {
            if (String.IsNullOrEmpty(Id))
                throw new ArgumentNullException("Identificador não informado.");

            _unitOfWork.financasRepository.Delete(Id);
            _unitOfWork.SaveChanges();

            return true;
        }

        public bool Edit(Financa financa)
        {
            _unitOfWork.financasRepository.Edit(financa);
            _unitOfWork.SaveChanges();

            return true;
        }

        public bool Add(Financa financa)
        {
            _unitOfWork.financasRepository.Add(financa);
            _unitOfWork.SaveChanges();

            return true;
        }
    }        
}
