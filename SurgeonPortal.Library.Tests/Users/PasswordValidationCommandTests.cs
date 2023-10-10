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
	public class PasswordValidationCommandTests : TestBase<int>
    {
        private PasswordValidationCommandDto CreateValidDto()
        {     
            var dto = Create<PasswordValidationCommandDto>();

            dto.PasswordsMatch = Create<bool?>();
            dto.Password = Create<string>();
            dto.UserId = Create<int>();
    
            return dto;
        }

        #region Validate
        
        [Test]
        public void Validate_CallsDalCorrectly()
        {
            var expectedUserId = Create<int>();
            var expectedPassword = Create<string>();
            
            var dto = Create<PasswordValidationCommandDto>();
        
            var mockDal = new Mock<IPasswordValidationCommandDal>();
            mockDal.Setup(m => m.Validate(
                expectedUserId,
                expectedPassword))
                .Returns(dto);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IPasswordValidationCommand, PasswordValidationCommand>()
                .Build();
        
            var factory = new PasswordValidationCommandFactory();
            var sut = factory.Validate(
                expectedUserId,
                expectedPassword);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<PasswordValidationCommandDto>());
        }
        
        #endregion
	}
}
