using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Email;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Library.Users;
using SurgeonPortal.Shared.ReferenceLetters;
using SurgeonPortal.Shared.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Users
{
    [TestFixture] 
	public class ForgotUsernameCommandTests : TestBase<int>
    {
        private ForgotUsernameCommandDto CreateValidDto()
        {
            var dto = Create<ForgotUsernameCommandDto>();
        
            dto.Email = Create<string>();
            dto.UserName = Create<string>();
            dto.FirstName = Create<string>();
            dto.LastName = Create<string>();
        
            return dto;
        }
        
        #region SendForgotUsernameEmailAsync
        
        [Test]
        public async Task SendForgotUsernameEmailAsync_CallsDalCorrectly()
        {
            var expectedEmail = Create<string>();
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IForgotUsernameCommandDal>();
            mockDal.Setup(m => m.SendForgotUsernameEmailAsync(expectedEmail))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(GetEmailFactory())
                .WithRegisteredInstance(GetConfiguration())
                .WithBusinessObject<IForgotUsernameCommand, ForgotUsernameCommand>()
                .Build();
        
            var factory = new ForgotUsernameCommandFactory();
            var sut = await factory.SendForgotUsernameEmailAsync(expectedEmail);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<ForgotUsernameCommandDto>());
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
