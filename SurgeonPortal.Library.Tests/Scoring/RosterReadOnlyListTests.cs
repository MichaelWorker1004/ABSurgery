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
	public class RosterReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByExaminationHeaderIdAsync_CallsDalCorrectly()
        {
            var expectedExamHeaderId = Create<int>();
            var expectedExaminerUserId = 1234;
            
            var mockDal = new Mock<IRosterReadOnlyDal>();
            mockDal.Setup(m => m.GetByExaminationHeaderIdAsync(
                expectedExaminerUserId,
                expectedExamHeaderId))
                .ReturnsAsync(CreateMany<RosterReadOnlyDto>());
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRosterReadOnlyList, RosterReadOnlyList>()
                .WithBusinessObject<IRosterReadOnly, RosterReadOnly>()
                .Build();
        
            var factory = new RosterReadOnlyListFactory();
            var sut = await factory.GetByExaminationHeaderIdAsync(expectedExamHeaderId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExaminationHeaderIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<RosterReadOnlyDto>();
            var expectedExamHeaderId = Create<int>();
            var expectedExaminerUserId = 1234;
            
        
            var mockDal = new Mock<IRosterReadOnlyDal>();
            mockDal.Setup(m => m.GetByExaminationHeaderIdAsync(
                expectedExaminerUserId,
                expectedExamHeaderId))
                .ReturnsAsync(expectedDtos);
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRosterReadOnlyList, RosterReadOnlyList>()
                .WithBusinessObject<IRosterReadOnly, RosterReadOnly>()
                .Build();
        
            var factory = new RosterReadOnlyListFactory();
            var sut = await factory.GetByExaminationHeaderIdAsync(expectedExamHeaderId);
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
