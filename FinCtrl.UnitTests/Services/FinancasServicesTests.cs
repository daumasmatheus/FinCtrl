using FinCtrl.Infrastructure.Contexts;
using FinCtrl.Persistence.Interfaces;
using FinCtrl.Persistence.UnitOfWork;
using Moq;
using NUnit.Framework;

namespace FinCtrl.Tests.Services
{
    [TestFixture]
    public class FinancasServicesTests
    {
        private Mock<FinCtrlDbContext> mockContext;
        private IUnitOfWork unitOfWork;

        [SetUp]
        public void TestSetup()
        {
            mockContext = new Mock<FinCtrlDbContext>();
            unitOfWork = new UnitOfWork(mockContext.Object);
        }

        [Test]
        public void SaveChanges_CheckIfSaveChangesIsCalled()
        {
            unitOfWork.SaveChanges();
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }        
    }
}
