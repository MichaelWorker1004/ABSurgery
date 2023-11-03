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
	public class UserCredentialTests : TestBase<int>
    {
        private UserCredentialDto CreateValidDto()
        {
            var dto = Create<UserCredentialDto>();
        
            dto.UserId = 1234;
            dto.EmailAddress = "test@test1.com";
            dto.Password = "Abc1234!6";
        
            return dto;
        }
        
        #region UserCredential Business Rules
        #endregion

        #region GetByUserIdAsync UserCredential
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IUserCredentialDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<UserCredentialDto>());
            
            var mocks = GetMockedCommand(false);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mocks.MockCommand)
                .WithRegisteredInstance(mocks.MockCommandFactory)
                .WithBusinessObject<IUserCredential, UserCredential>()
                .Build();
        
            var factory = new UserCredentialFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IUserCredentialDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(dto);
            
			var mocks = GetMockedCommand(false);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
				.WithRegisteredInstance(mocks.MockCommand)
				.WithRegisteredInstance(mocks.MockCommandFactory)
                .WithBusinessObject<IUserCredential, UserCredential>()
                .Build();
        
            var factory = new UserCredentialFactory();
            var sut = await factory.GetByUserIdAsync();
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Update
    //    [Test]
    //    public async Task Update_CallsDalCorrectly()
    //    {
    //        var expectedUserId = 1234;
            
    //        var dto = CreateValidDto();
    //        UserCredentialDto passedDto = null;
        
    //        var mockDal = new Mock<IUserCredentialDal>();
    //        mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
    //            .ReturnsAsync(dto);
            
    //        mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserCredentialDto>()))
    //            .Callback<UserCredentialDto>((p) => passedDto = p)
    //            .ReturnsAsync(dto);
        
    //        var mocks = GetMockedCommand(false);
    //        UseMockServiceProvider()
    //            .WithMockedIdentity(1234, "SomeUser")
    //            .WithRegisteredInstance(mockDal)
    //            .WithRegisteredInstance(mocks.MockCommand)
    //            .WithRegisteredInstance(mocks.MockCommandFactory)
    //            .WithBusinessObject<IUserCredential, UserCredential>()
    //            .Build();
        
    //        var factory = new UserCredentialFactory();
    //        var sut = await factory.GetByUserIdAsync();
        
            

    //        try
    //        {
				//sut.Password = $"{dto.Password}1";
				//await sut.SaveAsync();
    //        }
    //        catch (System.Exception ex)
    //        {
    //            TestContext.Out.WriteLine(ex.Message);
    //            throw;
    //        }

    //Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
    //        dto.Should().BeEquivalentTo(passedDto,
    //            options => options
    //            .Excluding(m => m.UserId)
    //            .Excluding(m => m.CreatedAtUtc)
    //            .Excluding(m => m.CreatedByUserId)
    //            .Excluding(m => m.LastUpdatedAtUtc)
    //            .Excluding(m => m.LastUpdatedByUserId)
    //            .Excluding(m => m.EmailAddress)
    //            .ExcludingMissingMembers());
        
    //        mockDal.VerifyAll();
    //    }
        
    //    [Test]
    //    public async Task Update_YieldsCorrectResult()
    //    {
    //        var expectedUserId = 1234;
            
    //        var dto = CreateValidDto();
        
    //        var mockDal = new Mock<IUserCredentialDal>();
    //        mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
    //            .ReturnsAsync(dto);
            
    //        mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserCredentialDto>()))
    //            .ReturnsAsync(dto);
        
    //        var mocks = GetMockedCommand(false);

    //        UseMockServiceProvider()
    //            .WithMockedIdentity(1234, "SomeUser")
    //            .WithRegisteredInstance(mockDal)
    //            .WithRegisteredInstance(mocks.MockCommand)
    //            .WithRegisteredInstance(mocks.MockCommandFactory)
    //            .WithBusinessObject<IUserCredential, UserCredential>()
    //            .Build();
        
    //        var factory = new UserCredentialFactory();
    //        var sut = await factory.GetByUserIdAsync();
    //        sut.EmailAddress = "test2@tst.com";
        
    //        await sut.SaveAsync();
            
    //        dto.Should().BeEquivalentTo(sut,
    //            options => options
    //                .Excluding(m => m.EmailAddress)
    //                .Excluding(m => m.CreatedAtUtc)
    //                .Excluding(m => m.CreatedByUserId)
    //                .Excluding(m => m.LastUpdatedAtUtc)
    //                .Excluding(m => m.LastUpdatedByUserId)
    //            .ExcludingMissingMembers());
    //    }
        
        #endregion
        
        (Mock<IPasswordValidationCommand> MockCommand, Mock<IPasswordValidationCommandFactory> MockCommandFactory) GetMockedCommand(bool passwordsMatch)
        {
            var mockCommandFactory = new Mock<IPasswordValidationCommandFactory>();
            var mockCommand = new Mock<IPasswordValidationCommand>();
            mockCommand.SetupGet(p => p.PasswordsMatch).Returns(passwordsMatch);
            mockCommand.SetupGet(p => p.UserId).Returns(1234);
            mockCommand.SetupGet(p => p.Password).Returns("Abc1234!6");

            mockCommandFactory
                .Setup(f => f.Validate(It.IsAny<int>(),
                        It.IsAny<string>()))
                .Returns(mockCommand.Object);
            
            return (mockCommand, mockCommandFactory);
        }
	}
}
