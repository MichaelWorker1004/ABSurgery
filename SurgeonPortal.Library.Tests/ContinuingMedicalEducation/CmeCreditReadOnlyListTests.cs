using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ContinuingMedicalEducation;
using SurgeonPortal.Library.ContinuingMedicalEducation;
using SurgeonPortal.Library.Contracts.ContinuingMedicalEducation;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.ContinuingMedicalEducation
{
    [TestFixture] 
	public class CmeCreditReadOnlyListTests : TestBase<int>
    {

        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<ICmeCreditReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(CreateMany<CmeCreditReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICmeCreditReadOnlyList, CmeCreditReadOnlyList>()
                .WithBusinessObject<ICmeCreditReadOnly, CmeCreditReadOnly>()
                .Build();
        
            var factory = new CmeCreditReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync(expectedUserId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<CmeCreditReadOnlyDto>();
        
            var mockDal = new Mock<ICmeCreditReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICmeCreditReadOnlyList, CmeCreditReadOnlyList>()
                .WithBusinessObject<ICmeCreditReadOnly, CmeCreditReadOnly>()
                .Build();
        
            var factory = new CmeCreditReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync(Create<int>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
