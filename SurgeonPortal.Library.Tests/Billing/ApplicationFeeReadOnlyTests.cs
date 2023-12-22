using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Billing;
using SurgeonPortal.Library.Billing;
using SurgeonPortal.Library.Contracts.Billing;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Billing
{
    [TestFixture] 
	public class ApplicationFeeReadOnlyTests : TestBase<int>
    {
        #region GetByExamIdAsync
        
        [Test]
        public async Task GetByExamIdAsync_CallsDalCorrectly()
        {
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IApplicationFeeReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(Create<ApplicationFeeReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IApplicationFeeReadOnly, ApplicationFeeReadOnly>()
                .Build();
        
            var factory = new ApplicationFeeReadOnlyFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<ApplicationFeeReadOnlyDto>();
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IApplicationFeeReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IApplicationFeeReadOnly, ApplicationFeeReadOnly>()
                .Build();
        
            var factory = new ApplicationFeeReadOnlyFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
