using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Library.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examinations
{
    [TestFixture] 
	public class QualifyingExamReadOnlyTests : TestBase<int>
    {
        #region GetAsync
        
        [Test]
        public async Task GetAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IQualifyingExamReadOnlyDal>();
            mockDal.Setup(m => m.GetAsync())
                .ReturnsAsync(Create<QualifyingExamReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IQualifyingExamReadOnly, QualifyingExamReadOnly>()
                .Build();
        
            var factory = new QualifyingExamReadOnlyFactory();
            var sut = await factory.GetAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAsync_LoadsSelfCorrectly()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IQualifyingExamReadOnlyDal>();
            mockDal.Setup(m => m.GetAsync())
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IQualifyingExamReadOnly, QualifyingExamReadOnly>()
                .Build();
        
            var factory = new QualifyingExamReadOnlyFactory();
            var sut = await factory.GetAsync();
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
