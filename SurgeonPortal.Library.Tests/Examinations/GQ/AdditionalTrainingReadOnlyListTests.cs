using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations.GQ;
using SurgeonPortal.Library.Contracts.Examinations.GQ;
using SurgeonPortal.Library.Examinations.GQ;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examinations.GQ
{
    [TestFixture] 
	public class AdditionalTrainingReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetAllByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IAdditionalTrainingReadOnlyDal>();
            mockDal.Setup(m => m.GetAllByUserIdAsync(expectedUserId))
                .ReturnsAsync(CreateMany<AdditionalTrainingReadOnlyDto>());
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdditionalTrainingReadOnlyList, AdditionalTrainingReadOnlyList>()
                .WithBusinessObject<IAdditionalTrainingReadOnly, AdditionalTrainingReadOnly>()
                .Build();
        
            var factory = new AdditionalTrainingReadOnlyListFactory();
            var sut = await factory.GetAllByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAllByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<AdditionalTrainingReadOnlyDto>();
            var expectedUserId = 1234;
            
        
            var mockDal = new Mock<IAdditionalTrainingReadOnlyDal>();
            mockDal.Setup(m => m.GetAllByUserIdAsync(expectedUserId))
                .ReturnsAsync(expectedDtos);
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdditionalTrainingReadOnlyList, AdditionalTrainingReadOnlyList>()
                .WithBusinessObject<IAdditionalTrainingReadOnly, AdditionalTrainingReadOnly>()
                .Build();
        
            var factory = new AdditionalTrainingReadOnlyListFactory();
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
