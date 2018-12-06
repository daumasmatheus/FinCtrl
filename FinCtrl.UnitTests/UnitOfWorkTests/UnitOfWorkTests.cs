using FinCtrl.Domain.Entities;
using FinCtrl.Infrastructure.Contexts;
using FinCtrl.Persistence.Interfaces;
using FinCtrl.Persistence.UnitOfWork;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace FinCtrl.Tests.UnitOfWorkTests
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private FinCtrlDbContext _context;
        private IUnitOfWork Uow;

        string UserId = Guid.NewGuid().ToString();

        [SetUp]
        public void SetUp()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();

            _context = new FinCtrlDbContext(connection);
            Uow = new UnitOfWork(_context);

            using (var ctx = new FinCtrlDbContext(connection))
            {
                ctx.Users.Add(new ApplicationUser() { Id = UserId, UserName = "Test", PasswordHash = "asdasd" });

                ctx.Tipos.Add(new Tipo() { Id = 1, Nome = "Test" });

                ctx.Financas.Add(new Financa() { Nome = "Test", TipoId = 1, Valor = 350, UserId = UserId });

                ctx.SaveChanges();
            }
        }

        //FINANCAS UOW TESTS

        [Test]
        public void Add_ShouldAddNewFinanca()
        {
            Financa finTest = new Financa()
            {
                Nome = "fintest",
                TipoId = 1,
                Valor = 350,
                UserId = UserId
            };

            Uow.financasRepository.Add(finTest);
            Uow.SaveChanges();

            var retrievedData = Uow.financasRepository.Find(finTest.Id);

            retrievedData.Should().NotBeNull();
            retrievedData.Nome.Should().Be(finTest.Nome);
        }

        [Test]
        public void GetAll_ShouldGetAllFinancasFromDB()
        {
            Uow.financasRepository.Add(new Financa() { Nome = "Test1", TipoId = 1, Valor = 350, UserId = UserId });
            Uow.financasRepository.Add(new Financa() { Nome = "Test2", TipoId = 1, Valor = 350, UserId = UserId });
            Uow.financasRepository.Add(new Financa() { Nome = "Test3", TipoId = 1, Valor = 350, UserId = UserId });
            Uow.SaveChanges();

            var retrievedData = Uow.financasRepository.GetAll();

            retrievedData.Should().NotBeNullOrEmpty();
            retrievedData.Should().HaveCount(4);
        }

        [Test]
        public void Find_ShouldGetFinancaById()
        {
            Financa finTest = new Financa() { Nome = "Test-new", TipoId = 1, Valor = 350, UserId = UserId };

            Uow.financasRepository.Add(finTest);
            Uow.SaveChanges();

            var retrievedData = Uow.financasRepository.Find(finTest.Id);

            retrievedData.Should().NotBeNull();
            retrievedData.Id.Should().Be(finTest.Id);
            retrievedData.Nome.Should().Be(finTest.Nome);
        }

        [Test]
        public void Find_ShouldGetFinancaByName()
        {
            Financa finTest = new Financa() { Nome = "Test-new", TipoId = 1, Valor = 350, UserId = UserId };

            Uow.financasRepository.Add(finTest);
            Uow.SaveChanges();

            var retrievedData = Uow.financasRepository.Find(x => x.Nome == finTest.Nome);

            retrievedData.Should().NotBeNull();
            retrievedData.Nome.Should().Be(finTest.Nome);
        }

        [Test]
        public void Edit_ShouldEditAnExistentData()
        {
            string newName = "Test Edit";

            var retrievedData = Uow.financasRepository.Find(x => x.Nome == "Test");
            retrievedData.Nome = newName;
            Uow.financasRepository.Edit(retrievedData);
            Uow.SaveChanges();

            var dataEdited = Uow.financasRepository.Find(retrievedData.Id);

            dataEdited.Should().NotBeNull();
            dataEdited.Nome.Should().Be(newName);
        }

        [Test]
        public void Delete_ShouldDeleteAnExistentData()
        {
            var retrievedData = Uow.financasRepository.Find(x => x.Nome == "Test");
            Uow.financasRepository.Delete(retrievedData.Id);
            Uow.SaveChanges();

            var newData = Uow.financasRepository.GetAll();

            newData.Should().NotContain(retrievedData);
        }
    }
}
