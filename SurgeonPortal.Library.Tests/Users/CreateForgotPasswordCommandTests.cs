using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Email;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Library.Users;
using SurgeonPortal.Shared.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Users
{
    [TestFixture] 
	public class CreateForgotPasswordCommandTests : TestBase<int>
    {
        private CreateForgotPasswordCommandDto CreateValidDto()
        {
            var dto = Create<CreateForgotPasswordCommandDto>();
        
            dto.UserName = Create<string>();
            dto.ResetGUID = Create<System.Guid>();
            dto.EmailAddress = Create<string>();
            dto.FirstName = Create<string>();
            dto.LastName = Create<string>();
        
            return dto;
        }
        
        #region SendForgotPasswordEmailAsync
        
        [Test]
        public async Task SendForgotPasswordEmailAsync_CallsDalCorrectly()
        {
            var expectedUserName = Create<string>();
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<ICreateForgotPasswordCommandDal>();
            mockDal.Setup(m => m.SendForgotPasswordEmailAsync(expectedUserName))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(GetEmailFactory())
                .WithRegisteredInstance(GetConfiguration())
                .WithBusinessObject<ICreateForgotPasswordCommand, CreateForgotPasswordCommand>()
                .Build();
        
            var factory = new CreateForgotPasswordCommandFactory();
            var sut = await factory.SendForgotPasswordEmailAsync(expectedUserName);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<CreateForgotPasswordCommandDto>());
        }

        #endregion

        private static Mock<IEmailFactory> GetEmailFactory()
        {
            var mockEmailFactory = new Mock<IEmailFactory>();
            mockEmailFactory.Setup(e => e.Create()).Returns(new Mock<IEmail>().Object);
            return mockEmailFactory;
        }

        private static Mock<IOptions<UsersConfiguration>> GetConfiguration()
        {
            var mockOptions = new Mock<IOptions<UsersConfiguration>>();
            mockOptions.Setup(m => m.Value).Returns(new UsersConfiguration());
            return mockOptions;
        }
    }
}
