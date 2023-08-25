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
	public class CaseDetailReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetByCaseHeaderIdAsync_CallsDalCorrectly()
        {
            var expectedCaseHeaderId = Create<int>();
            
            var mockDal = new Mock<ICaseDetailReadOnlyDal>();
            mockDal.Setup(m => m.GetByCaseHeaderIdAsync(expectedCaseHeaderId))
                .ReturnsAsync(CreateMany<CaseDetailReadOnlyDto>());
        
            UseMockServiceProvider()
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
        
            var mockDal = new Mock<ICaseDetailReadOnlyDal>();
            mockDal.Setup(m => m.GetByCaseHeaderIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseDetailReadOnlyList, CaseDetailReadOnlyList>()
                .WithBusinessObject<ICaseDetailReadOnly, CaseDetailReadOnly>()
                .Build();
        
            var factory = new CaseDetailReadOnlyListFactory();
            var sut = await factory.GetByCaseHeaderIdAsync(Create<int>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
