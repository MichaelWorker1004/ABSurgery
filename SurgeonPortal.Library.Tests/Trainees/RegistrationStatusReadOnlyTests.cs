using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Trainees;
using SurgeonPortal.Library.Contracts.Trainees;
using SurgeonPortal.Library.Trainees;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Trainees
{
    [TestFixture] 
	public class RegistrationStatusReadOnlyTests : TestBase<int>
    {

        #region GetByExamCodeAsync
        
        [Test]
        public async Task GetByExamCodeAsync_CallsDalCorrectly()
        {
            var expectedExamCode = Create<string>();
            
            var mockDal = new Mock<IRegistrationStatusReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamCodeAsync(expectedExamCode))
                .ReturnsAsync(Create<RegistrationStatusReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRegistrationStatusReadOnly, RegistrationStatusReadOnly>()
                .Build();
        
            var factory = new RegistrationStatusReadOnlyFactory();
            var sut = await factory.GetByExamCodeAsync(expectedExamCode);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamCodeAsync_LoadsSelfCorrectly()
        {
            var dto = Create<RegistrationStatusReadOnlyDto>();
        
            var mockDal = new Mock<IRegistrationStatusReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamCodeAsync(It.IsAny<string>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRegistrationStatusReadOnly, RegistrationStatusReadOnly>()
                .Build();
        
            var factory = new RegistrationStatusReadOnlyFactory();
            var sut = await factory.GetByExamCodeAsync(Create<string>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
