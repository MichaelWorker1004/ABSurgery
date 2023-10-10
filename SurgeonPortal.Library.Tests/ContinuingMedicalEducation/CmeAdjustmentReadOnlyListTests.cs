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
	public class CmeAdjustmentReadOnlyListTests : TestBase<int>
    {

        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<ICmeAdjustmentReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(CreateMany<CmeAdjustmentReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICmeAdjustmentReadOnlyList, CmeAdjustmentReadOnlyList>()
                .WithBusinessObject<ICmeAdjustmentReadOnly, CmeAdjustmentReadOnly>()
                .Build();
        
            var factory = new CmeAdjustmentReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync(expectedUserId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<CmeAdjustmentReadOnlyDto>();
        
            var mockDal = new Mock<ICmeAdjustmentReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICmeAdjustmentReadOnlyList, CmeAdjustmentReadOnlyList>()
                .WithBusinessObject<ICmeAdjustmentReadOnly, CmeAdjustmentReadOnly>()
                .Build();
        
            var factory = new CmeAdjustmentReadOnlyListFactory();
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
