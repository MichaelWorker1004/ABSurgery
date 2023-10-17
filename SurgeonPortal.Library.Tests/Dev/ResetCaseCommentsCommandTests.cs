using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Dev;
using SurgeonPortal.Library.Contracts.Dev;
using SurgeonPortal.Library.Dev;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Dev
{
    [TestFixture] 
	public class ResetCaseCommentsCommandTests : TestBase<int>
    {
        #region ResetCaseCommentsAsync
        
        [Test]
        public async Task ResetCaseCommentsAsync_CallsDalCorrectly()
        {
            var expectedExaminerUserId = 1234;
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IResetCaseCommentsCommandDal>();
            mockDal.Setup(m => m.ResetCaseCommentsAsyncAsync(expectedExaminerUserId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IResetCaseCommentsCommand, ResetCaseCommentsCommand>()
                .Build();
        
            var factory = new ResetCaseCommentsCommandFactory();
            var sut = await factory.ResetCaseCommentsAsync();
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<ResetCaseCommentsCommandDto>());
        }
        
        #endregion
	}
}
