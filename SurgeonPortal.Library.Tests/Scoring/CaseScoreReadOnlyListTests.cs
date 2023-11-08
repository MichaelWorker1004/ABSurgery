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
	public class CaseScoreReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByExamScheduleIdAsync_CallsDalCorrectly()
        {
            var expectedExamScheduleId = Create<int>();
            var expectedExaminerUserId = 1234;
            
            var mockDal = new Mock<ICaseScoreReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamScheduleIdAsync(
                expectedExaminerUserId,
                expectedExamScheduleId))
                .ReturnsAsync(CreateMany<CaseScoreReadOnlyDto>());
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseScoreReadOnlyList, CaseScoreReadOnlyList>()
                .WithBusinessObject<ICaseScoreReadOnly, CaseScoreReadOnly>()
                .Build();
        
            var factory = new CaseScoreReadOnlyListFactory();
            var sut = await factory.GetByExamScheduleIdAsync(expectedExamScheduleId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamScheduleIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<CaseScoreReadOnlyDto>();
            var expectedExamScheduleId = Create<int>();
            var expectedExaminerUserId = 1234;
            
        
            var mockDal = new Mock<ICaseScoreReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamScheduleIdAsync(
                expectedExaminerUserId,
                expectedExamScheduleId))
                .ReturnsAsync(expectedDtos);
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseScoreReadOnlyList, CaseScoreReadOnlyList>()
                .WithBusinessObject<ICaseScoreReadOnly, CaseScoreReadOnly>()
                .Build();
        
            var factory = new CaseScoreReadOnlyListFactory();
            var sut = await factory.GetByExamScheduleIdAsync(expectedExamScheduleId);
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
