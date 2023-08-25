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
	public class CaseScoreReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetByExamScheduleIdAsync_CallsDalCorrectly()
        {
            var expectedExamScheduleId = Create<int>();
            
            var mockDal = new Mock<ICaseScoreReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamScheduleIdAsync(expectedExamScheduleId))
                .ReturnsAsync(CreateMany<CaseScoreReadOnlyDto>());
        
            UseMockServiceProvider()
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
        
            var mockDal = new Mock<ICaseScoreReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamScheduleIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseScoreReadOnlyList, CaseScoreReadOnlyList>()
                .WithBusinessObject<ICaseScoreReadOnly, CaseScoreReadOnly>()
                .Build();
        
            var factory = new CaseScoreReadOnlyListFactory();
            var sut = await factory.GetByExamScheduleIdAsync(Create<int>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
