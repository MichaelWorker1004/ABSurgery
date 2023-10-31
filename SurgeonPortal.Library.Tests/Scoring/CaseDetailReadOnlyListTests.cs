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
	public class CaseDetailReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByCaseHeaderIdAsync_CallsDalCorrectly()
        {
            var expectedCaseHeaderId = Create<int>();
            var expectedExaminerUserId = 1234;
            
            var mockDal = new Mock<ICaseDetailReadOnlyDal>();
            mockDal.Setup(m => m.GetByCaseHeaderIdAsync(
                expectedCaseHeaderId,
                expectedExaminerUserId))
                .ReturnsAsync(CreateMany<CaseDetailReadOnlyDto>());
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseDetailReadOnlyList, CaseDetailReadOnlyList>()
                .WithBusinessObject<ICaseDetailReadOnly, CaseDetailReadOnly>()
                .Build();
        
            var factory = new CaseDetailReadOnlyListFactory();
            var sut = await factory.GetByCaseHeaderIdAsync(expectedCaseHeaderId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByCaseHeaderIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<CaseDetailReadOnlyDto>();
            var expectedCaseHeaderId = Create<int>();
            var expectedExaminerUserId = 1234;
            
        
            var mockDal = new Mock<ICaseDetailReadOnlyDal>();
            mockDal.Setup(m => m.GetByCaseHeaderIdAsync(
                expectedCaseHeaderId,
                expectedExaminerUserId))
                .ReturnsAsync(expectedDtos);
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseDetailReadOnlyList, CaseDetailReadOnlyList>()
                .WithBusinessObject<ICaseDetailReadOnly, CaseDetailReadOnly>()
                .Build();
        
            var factory = new CaseDetailReadOnlyListFactory();
            var sut = await factory.GetByCaseHeaderIdAsync(It.IsAny<int>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
