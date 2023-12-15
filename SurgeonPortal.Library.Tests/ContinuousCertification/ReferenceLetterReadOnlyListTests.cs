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
	public class ReferenceLetterReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetAllByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IReferenceLetterReadOnlyDal>();
            mockDal.Setup(m => m.GetAllByUserIdAsync(expectedUserId))
                .ReturnsAsync(CreateMany<ReferenceLetterReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IReferenceLetterReadOnlyList, ReferenceLetterReadOnlyList>()
                .WithBusinessObject<IReferenceLetterReadOnly, ReferenceLetterReadOnly>()
                .Build();
        
            var factory = new ReferenceLetterReadOnlyListFactory();
            var sut = await factory.GetAllByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAllByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<ReferenceLetterReadOnlyDto>();
            var expectedUserId = 1234;
            
        
            var mockDal = new Mock<IReferenceLetterReadOnlyDal>();
            mockDal.Setup(m => m.GetAllByUserIdAsync(expectedUserId))
                .ReturnsAsync(expectedDtos);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IReferenceLetterReadOnlyList, ReferenceLetterReadOnlyList>()
                .WithBusinessObject<IReferenceLetterReadOnly, ReferenceLetterReadOnly>()
                .Build();
        
            var factory = new ReferenceLetterReadOnlyListFactory();
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
