using FinCtrl.Application.Interfaces.Financas;
using FinCtrl.Application.Services.Financas;
using FinCtrl.Domain.Entities;
using FinCtrl.Persistence.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FinCtrl.Tests.Services
{
    [TestFixture]
    public class FinancasServicesTests
    {
        private IFinancasServices financasServices;
        private Mock<IUnitOfWork> uow;

        [SetUp]
        public void TestSetup()
        {
            var fakeData = new List<Financa>()
            {
                new Financa(){ Nome = "Test 1", TipoId = 1, UserId = Guid.NewGuid().ToString(), Valor = 350}
            };

            uow = new Mock<IUnitOfWork>();
            financasServices = new FinancasServices(uow.Object);
        }

        [Test]
        public void Find_NoParameterIsPassed_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => financasServices.Find(""));
        }

        [Test]
        public void Delete_NoParameterIsPassed_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => financasServices.Delete(""));
        }
    }
}