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
	public class CmeCreditReadOnlyTests : TestBase<int>
    {
        #region GetByIdAsync
        
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedCmeId = Create<int>();
            
            var mockDal = new Mock<ICmeCreditReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedCmeId))
                .ReturnsAsync(Create<CmeCreditReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICmeCreditReadOnly, CmeCreditReadOnly>()
                .Build();
        
            var factory = new CmeCreditReadOnlyFactory();
            var sut = await factory.GetByIdAsync(expectedCmeId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<CmeCreditReadOnlyDto>();
            var expectedCmeId = Create<int>();
            
            var mockDal = new Mock<ICmeCreditReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedCmeId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICmeCreditReadOnly, CmeCreditReadOnly>()
                .Build();
        
            var factory = new CmeCreditReadOnlyFactory();
            var sut = await factory.GetByIdAsync(expectedCmeId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
