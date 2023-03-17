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
	public class PasswordValidationCommandTests : TestBase<string>
    {
        private PasswordValidationCommandDto CreateValidDto()
        {     
            var dto = Create<PasswordValidationCommandDto>();

            dto.PasswordsMatch = Create<bool?>();
            dto.Password = Create<string>();
            dto.UserId = Create<int>();
    
            return dto;
        }

        #region ValidateAsync
        
        [Test]
        public async Task ValidateAsync_CallsDalCorrectly()
        {
            var expectedUserId = Create<int>();
            var expectedPassword = Create<string>();
            
            var dto = Create<PasswordValidationCommandDto>();
        
            var mockDal = new Mock<IPasswordValidationCommandDal>();
            mockDal.Setup(m => m.ValidateAsync(
                expectedUserId,
                expectedPassword))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IPasswordValidationCommand, PasswordValidationCommand>()
                .Build();
        
            var factory = new PasswordValidationCommandFactory();
            var sut = await factory.ValidateAsync(
                expectedUserId,
                expectedPassword);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<PasswordValidationCommandDto>());
        }
        
        #endregion
	}
}
