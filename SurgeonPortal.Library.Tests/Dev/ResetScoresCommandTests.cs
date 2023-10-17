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
	public class ResetScoresCommandTests : TestBase<int>
    {
        #region ResetExamScoresAsync
        
        [Test]
        public async Task ResetExamScoresAsync_CallsDalCorrectly()
        {
            var expectedExaminerUserId = 1234;
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IResetScoresCommandDal>();
            mockDal.Setup(m => m.ResetExamScoresAsyncAsync(expectedExaminerUserId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IResetScoresCommand, ResetScoresCommand>()
                .Build();
        
            var factory = new ResetScoresCommandFactory();
            var sut = await factory.ResetExamScoresAsync();
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<ResetScoresCommandDto>());
        }
        
        #endregion
	}
}
