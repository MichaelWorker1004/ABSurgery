using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Library.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Users
{
    [TestFixture] 
	public class AppUserReadOnlyTests : TestBase<int>
    {
        #region GetByCredentialsAsync
        
        [Test]
        public async Task GetByCredentialsAsync_CallsDalCorrectly()
        {
            var expectedUserName = Create<string>();
            var expectedPassword = Create<string>();
            
            var mockDal = new Mock<IAppUserReadOnlyDal>();
            mockDal.Setup(m => m.GetByCredentialsAsync(
                expectedUserName,
                expectedPassword))
                .ReturnsAsync(Create<AppUserReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
                .Build();
        
            var factory = new AppUserReadOnlyFactory();
            var sut = await factory.GetByCredentialsAsync(
                It.IsAny<string>(),
                It.IsAny<string>());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByCredentialsAsync_LoadsSelfCorrectly()
        {
            var dto = Create<AppUserReadOnlyDto>();
        
            var mockDal = new Mock<IAppUserReadOnlyDal>();
            mockDal.Setup(m => m.GetByCredentialsAsync(
                expectedUserName,
                expectedPassword))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
                .Build();
        
            var factory = new AppUserReadOnlyFactory();
            var sut = await factory.GetByCredentialsAsync(
                Create<string>(),
                Create<string>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region GetByTokenAsync
        
        [Test]
        public async Task GetByTokenAsync_CallsDalCorrectly()
        {
            var expectedToken = Create<string>();
            
            var mockDal = new Mock<IAppUserReadOnlyDal>();
            mockDal.Setup(m => m.GetByTokenAsync(expectedToken))
                .ReturnsAsync(Create<AppUserReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
                .Build();
        
            var factory = new AppUserReadOnlyFactory();
            var sut = await factory.GetByTokenAsync(It.IsAny<string>());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByTokenAsync_LoadsSelfCorrectly()
        {
            var dto = Create<AppUserReadOnlyDto>();
        
            var mockDal = new Mock<IAppUserReadOnlyDal>();
            mockDal.Setup(m => m.GetByTokenAsync(expectedToken))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
                .Build();
        
            var factory = new AppUserReadOnlyFactory();
            var sut = await factory.GetByTokenAsync(Create<string>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
