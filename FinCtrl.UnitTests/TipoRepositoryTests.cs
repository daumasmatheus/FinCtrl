using FinCtrl.Domain.Entities;
using FinCtrl.Infrastructure.Contexts;
using FinCtrl.Persistence.Interfaces;
using FinCtrl.Persistence.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace FinCtrl.UnitTests
{
    [TestFixture]
    public class TipoRepositoryTests
    {
        private FinCtrlDbContext _context;
        private ITipoRepository _repo;

        [SetUp]
        public void SetUp()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();

            _context = new FinCtrlDbContext(connection);
            _repo = new TipoRepository(_context);

            using (var ctx = new FinCtrlDbContext(connection))
            {
                ctx.Tipos.Add(new Tipo() { Id = 1, Nome = "Test 1" });
                ctx.Tipos.Add(new Tipo() { Id = 2, Nome = "Test 2" });
                ctx.SaveChanges();
            }
        }

        [Test]
        public void GetAll_ShouldGetAllDataFromDb()
        {
            var retrievedData = _repo.GetAll();

            retrievedData.Should().NotBeNullOrEmpty();
            retrievedData.Should().HaveCount(2);
        }

        [Test]
        public void Find_GivenAnExistingId_ShouldReturnTheDataWithTheGivenId()
        {
            var retrievedData = _repo.Find(1);

            retrievedData.Should().NotBeNull();
            retrievedData.Id.Should().Be(1);
            retrievedData.Nome.Should().Be("Test 1");
        }

        [Test]
        public void Find_GivenAnExistingName_ShouldReturnTheDataWithTheGivenName()
        {
            var retrievedData = _repo.Find(x => x.Nome == "Test 1");

            retrievedData.Should().NotBeNull();
            retrievedData.Nome.Should().Be("Test 1");
        }

        [Test]
        public void Find_GivenAnInexistentId_ShouldNotReturnData()
        {
            var retrievedData = _repo.Find(3);

            retrievedData.Should().BeNull();
        }

        [Test]
        public void Find_GivenAnInexistentName_ShouldNotReturnData()
        {
            var retrievedData = _repo.Find(x => x.Nome == "blabla");

            retrievedData.Should().BeNull();
        }

        [Test]
        public void Add_ShouldAddNewDataToTheDb()
        {
            string addTest = "Test 3";

            _repo.Add(new Tipo() { Id = 3, Nome = addTest });
            _context.SaveChanges();

            var retrievedData = _repo.Find(3);

            retrievedData.Should().NotBeNull();
            retrievedData.Nome.Should().Be(addTest);
        }        

        [Test]
        public void Edit_ShouldEditAnExistentData()
        {
            string newName = "Test Edited";

            var retrievedData = _repo.Find(1);
            retrievedData.Nome = newName;
            _repo.Edit(retrievedData);
            _context.SaveChanges();

            var editedData = _repo.Find(1);

            editedData.Nome.Should().Be(newName);
            editedData.Id.Should().Be(1);
        }
    }
}
