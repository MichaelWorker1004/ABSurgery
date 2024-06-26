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
	public class CaseRosterReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByScheduleIdAsync_CallsDalCorrectly()
        {
            var expectedScheduleId1 = Create<int>();
            var expectedScheduleId2 = Create<int?>();
            
            var mockDal = new Mock<ICaseRosterReadOnlyDal>();
            mockDal.Setup(m => m.GetByScheduleIdAsync(
                expectedScheduleId1,
                expectedScheduleId2))
                .ReturnsAsync(CreateMany<CaseRosterReadOnlyDto>());
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseRosterReadOnlyList, CaseRosterReadOnlyList>()
                .WithBusinessObject<ICaseRosterReadOnly, CaseRosterReadOnly>()
                .Build();
        
            var factory = new CaseRosterReadOnlyListFactory();
            var sut = await factory.GetByScheduleIdAsync(
                expectedScheduleId1,
                expectedScheduleId2);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByScheduleIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<CaseRosterReadOnlyDto>();
            var expectedScheduleId1 = Create<int>();
            var expectedScheduleId2 = Create<int?>();
            
        
            var mockDal = new Mock<ICaseRosterReadOnlyDal>();
            mockDal.Setup(m => m.GetByScheduleIdAsync(
                expectedScheduleId1,
                expectedScheduleId2))
                .ReturnsAsync(expectedDtos);
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseRosterReadOnlyList, CaseRosterReadOnlyList>()
                .WithBusinessObject<ICaseRosterReadOnly, CaseRosterReadOnly>()
                .Build();
        
            var factory = new CaseRosterReadOnlyListFactory();
            var sut = await factory.GetByScheduleIdAsync(
                expectedScheduleId1,
                expectedScheduleId2);
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
