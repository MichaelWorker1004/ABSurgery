using Csla;
using Csla.Configuration;
using Csla.Server.Dashboard;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Library.Users;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Users
{
    [TestFixture] 
	public class AppUserReadOnlyTests : TestBase<string>
    {
        #region GetByCredentialsAsync
        
        //[Test]
        //public async Task GetByCredentialsAsync_CallsDalCorrectly()
        //{
        //    var expectedEmailAddress = Create<string>();
        //    var expectedPassword = Create<string>();

        //    var mockDal = new Mock<IAppUserReadOnlyDal>();
        //    mockDal.Setup(m => m.GetByCredentialsAsync(
        //        expectedEmailAddress,
        //        expectedPassword))
        //        .ReturnsAsync(Create<AppUserReadOnlyDto>());

        //    //Csla.Server.IChildDataPortal<Child> childDataPortal = _testDIContext.CreateChildDataPortal<Child>();
        //    //var mockDataPortal = new Mock<Csla.Server.ChildDataPortal>();
        //    //mockDataPortal
        //    //    .Setup(m => m.FetchAsync<UserClaimReadOnlyList>(It.IsAny<IUserClaimReadOnly>()))
        //    //    .ReturnsAsync(Create<UserClaimReadOnlyList>());

        //    UseMockServiceProvider()
        //        .WithRegisteredInstance(mockDal)
        //        .WithBusinessObject<IUserClaimReadOnlyList, UserClaimReadOnlyList>()
        //        .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
        //        .Build();
        
        //    var factory = new AppUserReadOnlyFactory();
        //    var sut = await factory.GetByCredentialsAsync(
        //        expectedEmailAddress,
        //        expectedPassword);
        
        //    mockDal.VerifyAll();
        //}
        
        //[Test]
        //public async Task GetByCredentialsAsync_LoadsSelfCorrectly()
        //{
        //    var dto = Create<AppUserReadOnlyDto>();
        
        //    var mockDal = new Mock<IAppUserReadOnlyDal>();
        //    mockDal.Setup(m => m.GetByCredentialsAsync(
        //        It.IsAny<string>(),
        //        It.IsAny<string>()))
        //        .ReturnsAsync(dto);
        
        //    UseMockServiceProvider()
                
        //        .WithRegisteredInstance(mockDal)
        //        .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
        //        .Build();
        
        //    var factory = new AppUserReadOnlyFactory();
        //    var sut = await factory.GetByCredentialsAsync(
        //        Create<string>(),
        //        Create<string>());
        
        //    dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        //}
        
        //#endregion

        //#region GetByTokenAsync
        
        //[Test]
        //public async Task GetByTokenAsync_CallsDalCorrectly()
        //{
        //    var expectedToken = Create<string>();
            
        //    var mockDal = new Mock<IAppUserReadOnlyDal>();
        //    mockDal.Setup(m => m.GetByTokenAsync(expectedToken))
        //        .ReturnsAsync(Create<AppUserReadOnlyDto>());
        
        //    UseMockServiceProvider()
                
        //        .WithRegisteredInstance(mockDal)
        //        .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
        //        .Build();
        
        //    var factory = new AppUserReadOnlyFactory();
        //    var sut = await factory.GetByTokenAsync(expectedToken);
        
        //    mockDal.VerifyAll();
        //}
        
        //[Test]
        //public async Task GetByTokenAsync_LoadsSelfCorrectly()
        //{
        //    var dto = Create<AppUserReadOnlyDto>();
        
        //    var mockDal = new Mock<IAppUserReadOnlyDal>();
        //    mockDal.Setup(m => m.GetByTokenAsync(It.IsAny<string>()))
        //        .ReturnsAsync(dto);
        
        //    UseMockServiceProvider()
                
        //        .WithRegisteredInstance(mockDal)
        //        .WithBusinessObject<IAppUserReadOnly, AppUserReadOnly>()
        //        .Build();
        
        //    var factory = new AppUserReadOnlyFactory();
        //    var sut = await factory.GetByTokenAsync(Create<string>());
        
        //    dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        //}
        
        #endregion
	}
}
