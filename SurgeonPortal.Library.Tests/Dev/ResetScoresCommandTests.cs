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
        private ResetScoresCommandDto CreateValidDto()
        {     
            var dto = Create<ResetScoresCommandDto>();

            dto.ExaminerUserId = Create<int>();
    
            return dto;
        }

        #region ResetExamScoresAsync
        
        [Test]
        public async Task ResetExamScoresAsync_CallsDalCorrectly()
        {
            
            var dto = Create<ResetScoresCommandDto>();
        
            var mockDal = new Mock<IResetScoresCommandDal>();
            mockDal.Setup(m => m.ResetExamScoresAsync())
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IResetScoresCommand, ResetScoresCommand>()
                .Build();
        
            var factory = new ResetScoresCommandFactory();
            var sut = await factory.ResetExamScoresAsync(expectedExaminerUserId);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<ResetScoresCommandDto>());
        }
        
        #endregion
	}
}
