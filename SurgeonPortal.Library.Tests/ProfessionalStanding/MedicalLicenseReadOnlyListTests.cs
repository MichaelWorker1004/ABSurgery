using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.ProfessionalStanding
{
    [TestFixture] 
	public class MedicalLicenseReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IMedicalLicenseReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(CreateMany<MedicalLicenseReadOnlyDto>());
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalLicenseReadOnlyList, MedicalLicenseReadOnlyList>()
                .WithBusinessObject<IMedicalLicenseReadOnly, MedicalLicenseReadOnly>()
                .Build();
        
            var factory = new MedicalLicenseReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<MedicalLicenseReadOnlyDto>();
            var expectedUserId = 1234;
            
        
            var mockDal = new Mock<IMedicalLicenseReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(expectedDtos);
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IMedicalLicenseReadOnlyList, MedicalLicenseReadOnlyList>()
                .WithBusinessObject<IMedicalLicenseReadOnly, MedicalLicenseReadOnly>()
                .Build();
        
            var factory = new MedicalLicenseReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
