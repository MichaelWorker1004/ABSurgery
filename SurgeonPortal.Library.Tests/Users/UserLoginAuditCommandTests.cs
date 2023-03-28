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
	public class UserLoginAuditCommandTests : TestBase<string>
    {
        private UserLoginAuditCommandDto CreateValidDto()
        {     
            var dto = Create<UserLoginAuditCommandDto>();

            dto.UserId = Create<int>();
            dto.EmailAddress = Create<string>();
            dto.ApplicationId = Create<int>();
            dto.LoginIpAddress = Create<string>();
            dto.LoginUserAgent = Create<string>();
            dto.LoginSuccess = Create<bool>();
            dto.LoginFailureReason = Create<string>();
    
            return dto;
        }

        #region AuditAsync
        
        [Test]
        public async Task AuditAsync_CallsDalCorrectly()
        {
            var expectedUserId = Create<int>();
            var expectedEmailAddress = Create<string>();
            var expectedApplicationId = Create<int>();
            var expectedLoginIpAddress = Create<string>();
            var expectedLoginUserAgent = Create<string>();
            var expectedLoginSuccess = Create<bool>();
            var expectedLoginFailureReason = Create<string>();
            
            var dto = Create<UserLoginAuditCommandDto>();
        
            var mockDal = new Mock<IUserLoginAuditCommandDal>();
            mockDal.Setup(m => m.AuditAsync(
                expectedUserId,
                expectedEmailAddress,
                expectedApplicationId,
                expectedLoginIpAddress,
                expectedLoginUserAgent,
                expectedLoginSuccess,
                expectedLoginFailureReason))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserLoginAuditCommand, UserLoginAuditCommand>()
                .Build();
        
            var factory = new UserLoginAuditCommandFactory();
            var sut = await factory.AuditAsync(
                expectedUserId,
                expectedEmailAddress,
                expectedApplicationId,
                expectedLoginIpAddress,
                expectedLoginUserAgent,
                expectedLoginSuccess,
                expectedLoginFailureReason);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<UserLoginAuditCommandDto>());
        }
        
        #endregion
	}
}
