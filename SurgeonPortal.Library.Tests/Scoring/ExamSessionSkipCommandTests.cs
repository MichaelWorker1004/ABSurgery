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
	public class ExamSessionSkipCommandTests : TestBase<int>
    {
        private ExamSessionSkipCommandDto CreateValidDto()
        {
            var dto = Create<ExamSessionSkipCommandDto>();
        
            dto.ExamScheduleId = Create<int>();
            dto.ExaminerUserId = 1234;
        
            return dto;
        }
        
        #region SkipExamSessionAsync
        
        [Test]
        public async Task SkipExamSessionAsync_CallsDalCorrectly()
        {
            var expectedExamScheduleId = Create<int>();
            var expectedExaminerUserId = 1234;
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IExamSessionSkipCommandDal>();
            mockDal.Setup(m => m.SkipExamSessionAsync(
                expectedExamScheduleId,
                expectedExaminerUserId));
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamSessionSkipCommand, ExamSessionSkipCommand>()
                .Build();
        
            var factory = new ExamSessionSkipCommandFactory();
            var sut = await factory.SkipExamSessionAsync(expectedExamScheduleId);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<ExamSessionSkipCommandDto>());
        }
        
        #endregion
	}
}
