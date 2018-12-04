using FinCtrl.Domain.Entities;
using FinCtrl.Infrastructure.Contexts;
using FinCtrl.Persistence.UnitOfWork;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FinCtrl.UnitTests
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        private Mock<DbSet<Financa>> mockDbSet;
        private Mock<FinCtrlDbContext> mockFinCtrlDbContext;        
        private UnitOfWork uow;

        [SetUp]
        public void Setup()
        {
            var fakeData = new List<Financa>
            {
                new Financa { Nome = "Test 1", Observacao = "asd", TipoId = 1, Valor = 250 },
                new Financa { Nome = "Test 2", Observacao = "asd 2", TipoId = 1, Valor = 456 },
                new Financa { Nome = "Test 3", Observacao = "asd 3", TipoId = 2, Valor = 250 },
                new Financa { Nome = "Test 4", Observacao = "asd 4", TipoId = 1, Valor = 300 },
                new Financa { Nome = "Test 5", Observacao = "asd 5", TipoId = 2, Valor = 123 },
            }.AsQueryable();

            mockDbSet = new Mock<DbSet<Financa>>();
            mockDbSet.As<IQueryable<Financa>>().Setup(m => m.Provider).Returns(fakeData.Provider);
            mockDbSet.As<IQueryable<Financa>>().Setup(m => m.Expression).Returns(fakeData.Expression);
            mockDbSet.As<IQueryable<Financa>>().Setup(m => m.ElementType).Returns(fakeData.ElementType);
            mockDbSet.As<IQueryable<Financa>>().Setup(m => m.GetEnumerator()).Returns(fakeData.GetEnumerator);

            mockFinCtrlDbContext = new Mock<FinCtrlDbContext>();
            mockFinCtrlDbContext.Setup(ctx => ctx.Set<Financa>()).Returns(mockDbSet.Object);

            uow = new UnitOfWork(mockFinCtrlDbContext.Object);            
        }

        [TearDown]
        public void TearDown()
        {
            uow = null;
        }

        [Test]
        public void GetAll_ShouldReturnAllData()
        {
            //var data = repository.GetAll();
            var data = uow.financasRepository.GetAll();

            data.Should().NotBeNull();
        }

        [Test]
        public void Find_ShouldReturnAnExpecifiedData()
        {
            //var data = repository.Find(x => x.Nome == "Test 2");
            var data = uow.financasRepository.Find(x => x.Nome == "Test 2");

            data.Should().NotBeNullOrEmpty();
            data.Should().HaveCount(1);
        }       
    }
}
