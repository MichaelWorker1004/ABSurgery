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
	public class UserCredentialTests : TestBase<string>
    {
        private UserCredentialDto CreateValidDto()
        {     
            var dto = Create<UserCredentialDto>();

            dto.UserId = Create<int>();
            dto.EmailAddress = Create<string>();
            dto.Password = Create<string>();
    
            return dto;
        }

            

        #region GetByUserIdAsync UserCredential
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = Create<int>();
            
            var mockDal = new Mock<IUserCredentialDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<UserCredentialDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserCredential, UserCredential>()
                .Build();
        
            var factory = new UserCredentialFactory();
            var sut = await factory.GetByUserIdAsync(expectedUserId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IUserCredentialDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserCredential, UserCredential>()
                .Build();
        
            var factory = new UserCredentialFactory();
            var sut = await factory.GetByUserIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Update
        
        [Test]
        public async Task Update_CallsDalCorrectly()
        {
            var expectedId = Create<int>();
        
            var dto = Create<UserCredentialDto>();
            UserCredentialDto passedDto = null;
        
            var mockDal = new Mock<IUserCredentialDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedId))
                        .ReturnsAsync(dto);
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserCredentialDto>()))
                .Callback<UserCredentialDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserCredential, UserCredential>()
                .Build();
        
            var factory = new UserCredentialFactory();
            var sut = await factory.GetByUserIdAsync(expectedId);
            
            sut.UserId = dto.UserId;
            sut.EmailAddress = dto.EmailAddress;
            sut.Password = dto.Password;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.UserId = dto.UserId;
            sut.EmailAddress = dto.EmailAddress;
            sut.Password = dto.Password;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            var dto = Create<UserCredentialDto>();
        
            var mockDal = new Mock<IUserCredentialDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(It.IsAny<int>()))
                        .ReturnsAsync(Create<UserCredentialDto>());
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<UserCredentialDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserCredential, UserCredential>()
                .Build();
        
            var factory = new UserCredentialFactory();
            var sut = await factory.GetByUserIdAsync(Create<int>());
            sut.UserId = Create<int>();
        
            await sut.SaveAsync();
            
            dto.Should().BeEquivalentTo(sut,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        }
        
        #endregion
	}
}
