using FluentAssertions;
using Microsoft.Extensions.Logging;
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
	public class CaseFeedbackReadOnlyTests : TestBase<int>
    {
        #region GetByExaminerIdAsync
        
        [Test]
        public async Task GetByExaminerIdAsync_CallsDalCorrectly()
        {
            var expectedCaseHeaderId = Create<int>();
            var expectedExaminerUserId = 1234;
            
            var mockDal = new Mock<ICaseFeedbackReadOnlyDal>();
            mockDal.Setup(m => m.GetByExaminerIdAsync(
                expectedExaminerUserId,
                expectedCaseHeaderId))
                .ReturnsAsync(Create<CaseFeedbackReadOnlyDto>());

            var mockLogger = new Mock<ILogger<CaseFeedbackReadOnly>>();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockLogger)
                .WithBusinessObject<ICaseFeedbackReadOnly, CaseFeedbackReadOnly>()
                .Build();
        
            var factory = new CaseFeedbackReadOnlyFactory();
            var sut = await factory.GetByExaminerIdAsync(expectedCaseHeaderId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExaminerIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<CaseFeedbackReadOnlyDto>();
            var expectedCaseHeaderId = Create<int>();
            var expectedExaminerUserId = 1234;
        
            var mockDal = new Mock<ICaseFeedbackReadOnlyDal>();
            mockDal.Setup(m => m.GetByExaminerIdAsync(
                expectedExaminerUserId,
                expectedCaseHeaderId))
                .ReturnsAsync(dto);

            var mockLogger = new Mock<ILogger<CaseFeedbackReadOnly>>();

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockLogger)
                .WithBusinessObject<ICaseFeedbackReadOnly, CaseFeedbackReadOnly>()
                .Build();
        
            var factory = new CaseFeedbackReadOnlyFactory();
            var sut = await factory.GetByExaminerIdAsync(expectedCaseHeaderId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
