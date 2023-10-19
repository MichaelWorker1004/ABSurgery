using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using SurgeonPortal.Library.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Scoring
{
    [TestFixture] 
	public class ExamSessionLockCommandTests : TestBase<int>
    {
        private ExamSessionLockCommandDto CreateValidDto()
        {
            var dto = Create<ExamSessionLockCommandDto>();
        
            dto.ExamscheduleId = Create<int>();
        
            return dto;
        }
        
        #region LockExamSessionAsync
        
        [Test]
        public async Task LockExamSessionAsync_CallsDalCorrectly()
        {
            var expectedExamscheduleId = Create<int>();
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IExamSessionLockCommandDal>();
            mockDal.Setup(m => m.LockExamSessionAsync(expectedExamscheduleId));
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamSessionLockCommand, ExamSessionLockCommand>()
                .Build();
        
            var factory = new ExamSessionLockCommandFactory();
            var sut = await factory.LockExamSessionAsync(expectedExamscheduleId);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<ExamSessionLockCommandDto>());
        }
        
        #endregion
	}
}
