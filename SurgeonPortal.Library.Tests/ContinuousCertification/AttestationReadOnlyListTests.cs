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
	public class AttestationReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetAllByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IAttestationReadOnlyDal>();
            mockDal.Setup(m => m.GetAllByUserIdAsync(expectedUserId))
                .ReturnsAsync(CreateMany<AttestationReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAttestationReadOnlyList, AttestationReadOnlyList>()
                .WithBusinessObject<IAttestationReadOnly, AttestationReadOnly>()
                .Build();
        
            var factory = new AttestationReadOnlyListFactory();
            var sut = await factory.GetAllByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAllByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<AttestationReadOnlyDto>();
            var expectedUserId = 1234;
            
        
            var mockDal = new Mock<IAttestationReadOnlyDal>();
            mockDal.Setup(m => m.GetAllByUserIdAsync(expectedUserId))
                .ReturnsAsync(expectedDtos);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAttestationReadOnlyList, AttestationReadOnlyList>()
                .WithBusinessObject<IAttestationReadOnly, AttestationReadOnly>()
                .Build();
        
            var factory = new AttestationReadOnlyListFactory();
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
