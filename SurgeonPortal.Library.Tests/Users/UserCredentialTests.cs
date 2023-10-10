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
            var dto = new UserCredentialDto
            {
                EmailAddress = "test@test.com",
                Password = "Pass@word123"
            };
    
            return dto;
        }

            #region UserCredential Business Rules
            #endregion

        #region GetByUserIdAsync UserCredential
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IUserCredentialDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(Create<UserCredentialDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserCredential, UserCredential>()
                .WithBusinessObject<IPasswordValidationCommandFactory, PasswordValidationCommandFactory>()
                .Build();
        
            var factory = new UserCredentialFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IUserCredentialDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserCredential, UserCredential>()
                .WithBusinessObject<IPasswordValidationCommandFactory, PasswordValidationCommandFactory>()
                .Build();
        
            var factory = new UserCredentialFactory();
            var sut = await factory.GetByUserIdAsync();
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Update
        
        //[Test]
        //public async Task Update_CallsDalCorrectly()
        //{
        //    var dto = new UserCredentialDto
        //    {
        //        EmailAddress = "test1@test.com",
        //        Password = "Pass@word1"
        //    };
        //    UserCredentialDto passedDto = null;

        //    var passwordValidationCommand = new Mock<IPasswordValidationCommand>();
        //    passwordValidationCommand.SetupGet(e => e.PasswordsMatch).Returns(false);

        //    var passwordValidationCommandFactory = new Mock<IPasswordValidationCommandFactory>();
        //    passwordValidationCommandFactory.Setup(e => e.Validate(It.IsAny<int>(), It.IsAny<string>()))
        //        .Returns(passwordValidationCommand.Object);

        //    var mockDal = new Mock<IUserCredentialDal>();
        //    mockDal.Setup(m => m.GetByUserIdAsync())
        //                .ReturnsAsync(dto);
        //    mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserCredentialDto>()))
        //        .Callback<UserCredentialDto>((p) => passedDto = p)
        //        .ReturnsAsync(dto);
        
        //    UseMockServiceProvider()
        //        .WithMockedIdentity(1234, "SomeUser")
        //        .WithRegisteredInstance(mockDal)
        //        .WithBusinessObject<IUserCredential, UserCredential>()
        //        .WithRegisteredInstance(passwordValidationCommandFactory)
        //        .Build();
        
        //    var factory = new UserCredentialFactory();
        //    var sut = await factory.GetByUserIdAsync();
            
        //    sut.EmailAddress = dto.EmailAddress;
        //    sut.Password = dto.Password;
        
        //    // We now change all properties on the SUT to make it Dirty
        //    // or the SaveAsync() will not be called. :)
        //    dto = CreateValidDto();
        
        //    sut.EmailAddress = dto.EmailAddress;
        //    sut.Password = dto.Password;
        
        //    await sut.SaveAsync();
        
        //    Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
        //    dto.Should().BeEquivalentTo(passedDto,
        //        options => options
        //            .Excluding(m => m.CreatedAtUtc)
        //            .Excluding(m => m.CreatedByUserId)
        //            .Excluding(m => m.LastUpdatedAtUtc)
        //            .Excluding(m => m.LastUpdatedByUserId)
        //        .ExcludingMissingMembers());
        
        //    mockDal.VerifyAll();
        //}
        
        //[Test]
        //public async Task Update_YieldsCorrectResult()
        //{
        //    var dto = new UserCredentialDto
        //    {
        //        EmailAddress = "test1@test.com",
        //        Password = "Pass@word1"
        //    };

        //    var passwordValidationCommand = new Mock<IPasswordValidationCommand>();
        //    passwordValidationCommand.SetupGet(e => e.PasswordsMatch).Returns(false);

        //    var passwordValidationCommandFactory = new Mock<IPasswordValidationCommandFactory>();
        //    passwordValidationCommandFactory.Setup(e => e.Validate(It.IsAny<int>(), It.IsAny<string>()))
        //        .Returns(passwordValidationCommand.Object);

        //    var mockDal = new Mock<IUserCredentialDal>();
        //    mockDal.Setup(m => m.GetByUserIdAsync())
        //                .ReturnsAsync(Create<UserCredentialDto>());
        //    mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserCredentialDto>()))
        //        .ReturnsAsync(dto);
        
        //    UseMockServiceProvider()
        //        .WithMockedIdentity(1234, "SomeUser")
        //        .WithRegisteredInstance(mockDal)
        //        .WithBusinessObject<IUserCredential, UserCredential>()
        //        .WithRegisteredInstance(passwordValidationCommandFactory)
        //        .Build();
        
        //    var factory = new UserCredentialFactory();
        //    var sut = await factory.GetByUserIdAsync();
        //    sut.EmailAddress = dto.EmailAddress;

        //    await sut.SaveAsync();
            
        //    dto.Should().BeEquivalentTo(sut,
        //        options => options
        //            .Excluding(m => m.CreatedAtUtc)
        //            .Excluding(m => m.CreatedByUserId)
        //            .Excluding(m => m.LastUpdatedAtUtc)
        //            .Excluding(m => m.LastUpdatedByUserId)
        //        .ExcludingMissingMembers());
        //}
        
        #endregion
	}
}
