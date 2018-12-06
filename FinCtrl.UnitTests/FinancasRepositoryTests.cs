using FinCtrl.Domain.Entities;
using FinCtrl.Infrastructure.Contexts;
using FinCtrl.Persistence.Interfaces;
using FinCtrl.Persistence.Repositories;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace FinCtrl.UnitTests
{
    [TestFixture]
    public class FinancasRepositoryTests
    {
        private FinCtrlDbContext _context;
        private IFinancasRepository _repo;

        string UserId = Guid.NewGuid().ToString();

        [SetUp]
        public void SetUp()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();

            _context = new FinCtrlDbContext(connection);
            _repo = new FinancasRepository(_context);

            using (var ctx = new FinCtrlDbContext(connection))
            {
                ctx.Users.Add(new ApplicationUser() { Id = UserId, UserName = "Test", PasswordHash = "asdasd" });

                ctx.Tipos.Add(new Tipo() { Id = 1, Nome = "Test" });

                ctx.Financas.Add(new Financa() { Nome = "Test 1", TipoId = 1, Valor = 350, UserId = UserId });
                ctx.Financas.Add(new Financa() { Nome = "Test 2", TipoId = 1, Valor = 350, UserId = UserId });
                ctx.Financas.Add(new Financa() { Nome = "Test 3", TipoId = 1, Valor = 350, UserId = UserId });
                ctx.SaveChanges();
            }
        }

        [Test]
        public void FindFinancas_WrongId_ShouldReturnNull()
        {
            var data = _repo.Find(x => x.Id == "aaa");

            data.Should().BeNull();
        }

        [Test]
        public void Find_CorrectId_ShouldReturnData()
        {
            var data = _repo.Find(x => x.Nome == "Test 2");

            data.Nome.Should().Be("Test 2");
            data.Should().NotBeNull();
        }

        [Test]
        public void Find_GivenAndUserId_ShouldReturnDataOfTheSpecifiedUser()
        {
            var data = _repo.GetAll().Where(x => x.UserId == UserId);

            data.Should().HaveCount(3);
        }

        [Test]
        public void InsertFinanca_ShouldInsertNewData()
        {
            const string newFin = "newFin";            
            
            _repo.Add(new Financa() { Nome = newFin, TipoId = 1, UserId = UserId, Valor = 350 });
            _context.SaveChanges();

            var retrievedData = _repo.Find(x => x.Nome == newFin);

            retrievedData.Nome.Should().Be(newFin);
        }

        [Test]
        public void DeleteFinanca_ShouldDeleteTheData()
        {
            var data = _repo.Find(x => x.Nome == "Test 2");
            _repo.Delete(data.Id);
            _context.SaveChanges();

            _context.Financas.Should().NotContain(data);
        }

        [Test]
        public void Update_ShouldUpdateFinancaName()
        {
            Financa retrievedData = _repo.Find(x => x.Nome == "Test 2");

            string newName = "Test Altered";
            retrievedData.Nome = newName;

            _repo.Edit(retrievedData);
            _context.SaveChanges();

            var data = _repo.Find(x => x.Nome == newName);

            data.Nome.Should().Be(newName);
            data.Should().NotBeNull();
        }
    }
}
