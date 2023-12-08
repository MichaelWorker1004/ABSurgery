using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.User;
using SurgeonPortal.Library.Contracts.User;
using SurgeonPortal.Library.User;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.User
{
    [TestFixture] 
	public class CertificationStatusReadOnlyTests : TestBase<int>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<ICertificationStatusReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<CertificationStatusReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICertificationStatusReadOnly, CertificationStatusReadOnly>()
                .Build();
        
            var factory = new CertificationStatusReadOnlyFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<CertificationStatusReadOnlyDto>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<ICertificationStatusReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICertificationStatusReadOnly, CertificationStatusReadOnly>()
                .Build();
        
            var factory = new CertificationStatusReadOnlyFactory();
            var sut = await factory.GetByUserIdAsync();
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
