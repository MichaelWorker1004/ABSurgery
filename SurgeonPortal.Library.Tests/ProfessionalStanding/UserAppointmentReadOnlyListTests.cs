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
	public class UserAppointmentReadOnlyListTests : TestBase<int>
    {

        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IUserAppointmentReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(CreateMany<UserAppointmentReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserAppointmentReadOnlyList, UserAppointmentReadOnlyList>()
                .WithBusinessObject<IUserAppointmentReadOnly, UserAppointmentReadOnly>()
                .Build();
        
            var factory = new UserAppointmentReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<UserAppointmentReadOnlyDto>();
        
            var mockDal = new Mock<IUserAppointmentReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserAppointmentReadOnlyList, UserAppointmentReadOnlyList>()
                .WithBusinessObject<IUserAppointmentReadOnly, UserAppointmentReadOnly>()
                .Build();
        
            var factory = new UserAppointmentReadOnlyListFactory();
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
