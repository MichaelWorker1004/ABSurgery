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
	public class VerifyForgotGuidCommandTests : TestBase<int>
    {
        private VerifyForgotGuidCommandDto CreateValidDto()
        {
            var dto = Create<VerifyForgotGuidCommandDto>();
        
            dto.ResetGUID = Create<System.Guid>();
            dto.Result = Create<bool>();
        
            return dto;
        }
        
        #region VerifyForgotPasswordGuidAsync
        
        [Test]
        public async Task VerifyForgotPasswordGuidAsync_CallsDalCorrectly()
        {
            var expectedResetGUID = Create<Guid>();
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IVerifyForgotGuidCommandDal>();
            mockDal.Setup(m => m.VerifyForgotPasswordGuidAsync(expectedResetGUID))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IVerifyForgotGuidCommand, VerifyForgotGuidCommand>()
                .Build();
        
            var factory = new VerifyForgotGuidCommandFactory();
            var sut = await factory.VerifyForgotPasswordGuidAsync(expectedResetGUID);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<VerifyForgotGuidCommandDto>());
        }
        
        #endregion
	}
}
