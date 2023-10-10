using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.GraduateMedicalEducation;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.GraduateMedicalEducation
{
    [TestFixture] 
	public class RotationReadOnlyListTests : TestBase<int>
    {

        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IRotationReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(CreateMany<RotationReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRotationReadOnlyList, RotationReadOnlyList>()
                .WithBusinessObject<IRotationReadOnly, RotationReadOnly>()
                .Build();
        
            var factory = new RotationReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<RotationReadOnlyDto>();
        
            var mockDal = new Mock<IRotationReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRotationReadOnlyList, RotationReadOnlyList>()
                .WithBusinessObject<IRotationReadOnly, RotationReadOnly>()
                .Build();
        
            var factory = new RotationReadOnlyListFactory();
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
