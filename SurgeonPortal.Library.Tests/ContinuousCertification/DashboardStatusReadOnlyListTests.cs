using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.ContinuousCertification
{
    [TestFixture] 
	public class DashboardStatusReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetAllByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IDashboardStatusReadOnlyDal>();
            mockDal.Setup(m => m.GetAllByUserIdAsync(expectedUserId))
                .ReturnsAsync(CreateMany<DashboardStatusReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDashboardStatusReadOnlyList, DashboardStatusReadOnlyList>()
                .WithBusinessObject<IDashboardStatusReadOnly, DashboardStatusReadOnly>()
                .Build();
        
            var factory = new DashboardStatusReadOnlyListFactory();
            var sut = await factory.GetAllByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAllByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<DashboardStatusReadOnlyDto>();
            var expectedUserId = 1234;
            
        
            var mockDal = new Mock<IDashboardStatusReadOnlyDal>();
            mockDal.Setup(m => m.GetAllByUserIdAsync(expectedUserId))
                .ReturnsAsync(expectedDtos);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDashboardStatusReadOnlyList, DashboardStatusReadOnlyList>()
                .WithBusinessObject<IDashboardStatusReadOnly, DashboardStatusReadOnly>()
                .Build();
        
            var factory = new DashboardStatusReadOnlyListFactory();
            var sut = await factory.GetAllByUserIdAsync();
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
