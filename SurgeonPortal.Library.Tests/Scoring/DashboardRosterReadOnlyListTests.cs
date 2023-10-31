using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using SurgeonPortal.Library.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Scoring
{
    [TestFixture] 
	public class DashboardRosterReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedExamDate = Create<DateTime>();
            var expectedExaminerUserId = 1234;
            
            var mockDal = new Mock<IDashboardRosterReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(
                expectedExaminerUserId,
                expectedExamDate))
                .ReturnsAsync(CreateMany<DashboardRosterReadOnlyDto>());
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDashboardRosterReadOnlyList, DashboardRosterReadOnlyList>()
                .WithBusinessObject<IDashboardRosterReadOnly, DashboardRosterReadOnly>()
                .Build();
        
            var factory = new DashboardRosterReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync(expectedExamDate);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<DashboardRosterReadOnlyDto>();
            var expectedExamDate = Create<DateTime>();
            var expectedExaminerUserId = 1234;
            
        
            var mockDal = new Mock<IDashboardRosterReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(
                expectedExaminerUserId,
                expectedExamDate))
                .ReturnsAsync(expectedDtos);
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDashboardRosterReadOnlyList, DashboardRosterReadOnlyList>()
                .WithBusinessObject<IDashboardRosterReadOnly, DashboardRosterReadOnly>()
                .Build();
        
            var factory = new DashboardRosterReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync(It.IsAny<DateTime>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
