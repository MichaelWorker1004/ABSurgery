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
	public class PasswordResetCommandTests : TestBase<int>
    {
        private PasswordResetCommandDto CreateValidDto()
        {     
            var dto = Create<PasswordResetCommandDto>();

            dto.UserId = 1234;
            dto.OldPassword = Create<string>();
            dto.NewPassword = Create<string>();
            dto.PasswordReset = Create<bool?>();
    
            return dto;
        }

        #region ResetPasswordAsync
        
        [Test]
        public async Task ResetPasswordAsync_CallsDalCorrectly()
        {
            var expectedOldPassword = Create<string>();
            var expectedNewPassword = Create<string>();
            var expectedUserId = 1234;
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IPasswordResetCommandDal>();
            mockDal.Setup(m => m.ResetPasswordAsync(
                expectedUserId,
                expectedOldPassword,
                expectedNewPassword))
                .ReturnsAsync(dto);
        
                
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IPasswordResetCommand, PasswordResetCommand>()
                .Build();
        
            var factory = new PasswordResetCommandFactory();
            var sut = await factory.ResetPasswordAsync(
                expectedOldPassword,
                expectedNewPassword);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<PasswordResetCommandDto>());
        }
        
        #endregion
	}
}
