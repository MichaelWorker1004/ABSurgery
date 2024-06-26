using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Library.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examinations
{
    [TestFixture] 
	public class ExamOverviewReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetAllAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;

            var mockDal = new Mock<IExamOverviewReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync(expectedUserId))
                .ReturnsAsync(CreateMany<ExamOverviewReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamOverviewReadOnlyList, ExamOverviewReadOnlyList>()
                .WithBusinessObject<IExamOverviewReadOnly, ExamOverviewReadOnly>()
                .Build();
        
            var factory = new ExamOverviewReadOnlyListFactory();
            var sut = await factory.GetAllAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAllAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<ExamOverviewReadOnlyDto>();
            var expectedUserId = 1234;

            var mockDal = new Mock<IExamOverviewReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync(expectedUserId))
                .ReturnsAsync(expectedDtos);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamOverviewReadOnlyList, ExamOverviewReadOnlyList>()
                .WithBusinessObject<IExamOverviewReadOnly, ExamOverviewReadOnly>()
                .Build();
        
            var factory = new ExamOverviewReadOnlyListFactory();
            var sut = await factory.GetAllAsync();
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
