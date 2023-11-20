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
            
            var mockClaimsDal = new Mock<IUserClaimReadOnlyDal>();
            mockClaimsDal.Setup(m => m.GetByIdsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(CreateMany<UserClaimReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockClaimsDal)
                .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
                .WithBusinessObject<IUserClaimReadOnlyList, UserClaimReadOnlyList>()
                .WithBusinessObject<IUserClaimReadOnly, UserClaimReadOnly>()
                .Build();
        
            var factory = new AppUserReadOnlyFactory();
            var sut = await factory.GetByCredentialsAsync(
                expectedUserName,
                expectedPassword);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByCredentialsAsync_LoadsSelfCorrectly()
        {
            var dto = Create<AppUserReadOnlyDto>();
            var expectedUserName = Create<string>();
            var expectedPassword = Create<string>();
            
            var mockDal = new Mock<IAppUserReadOnlyDal>();
            mockDal.Setup(m => m.GetByCredentialsAsync(
                expectedUserName,
                expectedPassword))
                .ReturnsAsync(dto);
            
            var mockClaimsDal = new Mock<IUserClaimReadOnlyDal>();
            mockClaimsDal.Setup(m => m.GetByIdsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(CreateMany<UserClaimReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockClaimsDal)
                .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
                .WithBusinessObject<IUserClaimReadOnlyList, UserClaimReadOnlyList>()
                .WithBusinessObject<IUserClaimReadOnly, UserClaimReadOnly>()
                .Build();
        
            var factory = new AppUserReadOnlyFactory();
            var sut = await factory.GetByCredentialsAsync(
                expectedUserName,
                expectedPassword);
        
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
            
            var mockClaimsDal = new Mock<IUserClaimReadOnlyDal>();
            mockClaimsDal.Setup(m => m.GetByIdsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(CreateMany<UserClaimReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockClaimsDal)
                .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
                .WithBusinessObject<IUserClaimReadOnlyList, UserClaimReadOnlyList>()
                .WithBusinessObject<IUserClaimReadOnly, UserClaimReadOnly>()
                .Build();
        
            var factory = new AppUserReadOnlyFactory();
            var sut = await factory.GetByTokenAsync(expectedToken);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByTokenAsync_LoadsSelfCorrectly()
        {
            var dto = Create<AppUserReadOnlyDto>();
            var expectedToken = Create<string>();
            
            var mockDal = new Mock<IAppUserReadOnlyDal>();
            mockDal.Setup(m => m.GetByTokenAsync(expectedToken))
                .ReturnsAsync(dto);
            
            var mockClaimsDal = new Mock<IUserClaimReadOnlyDal>();
            mockClaimsDal.Setup(m => m.GetByIdsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(CreateMany<UserClaimReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockClaimsDal)
                .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
                .WithBusinessObject<IUserClaimReadOnlyList, UserClaimReadOnlyList>()
                .WithBusinessObject<IUserClaimReadOnly, UserClaimReadOnly>()
                .Build();
        
            var factory = new AppUserReadOnlyFactory();
            var sut = await factory.GetByTokenAsync(expectedToken);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
