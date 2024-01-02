using FluentAssertions;
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
	public class ResetForgotPasswordCommandTests : TestBase<int>
    {
        private ResetForgotPasswordCommandDto CreateValidDto()
        {
            var dto = Create<ResetForgotPasswordCommandDto>();
        
            dto.ResetGUID = Create<System.Guid>();
            dto.NewPassword = Create<string>();
            dto.Result = Create<bool?>();
        
            return dto;
        }
        
        #region ResetForgotPasswordAsync
        
        [Test]
        public async Task ResetForgotPasswordAsync_CallsDalCorrectly()
        {
            var expectedResetGUID = Create<Guid>();
            var expectedNewPassword = Create<string>();
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IResetForgotPasswordCommandDal>();
            mockDal.Setup(m => m.ResetForgotPasswordAsync(
                expectedResetGUID,
                expectedNewPassword))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IResetForgotPasswordCommand, ResetForgotPasswordCommand>()
                .Build();
        
            var factory = new ResetForgotPasswordCommandFactory();
            var sut = await factory.ResetForgotPasswordAsync(
                expectedResetGUID,
                expectedNewPassword);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<ResetForgotPasswordCommandDto>());
        }
        
        #endregion
	}
}
