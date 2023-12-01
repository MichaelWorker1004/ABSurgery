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
	public class UserTokenTests : TestBase<int>
    {
        private UserTokenDto CreateValidDto()
        {
            var dto = Create<UserTokenDto>();
        
            dto.UserId = Create<int>();
            dto.Token = Create<string>();
            dto.ExpiresAt = Create<System.DateTime>();
    
            return dto;
        }

            

        #region GetActiveAsync UserToken
        
        [Test]
        public async Task GetActiveAsync_CallsDalCorrectly()
        {
            var expectedToken = Create<string>();
            
            var mockDal = new Mock<IUserTokenDal>();
            mockDal.Setup(m => m.GetActiveAsync(expectedToken))
                .ReturnsAsync(Create<UserTokenDto>());
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserToken, UserToken>()
                .Build();
        
            var factory = new UserTokenFactory();
            var sut = await factory.GetActiveAsync(expectedToken);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetActive_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
            var expectedToken = Create<string>();
        
            var mockDal = new Mock<IUserTokenDal>();
            mockDal.Setup(m => m.GetActiveAsync(expectedToken))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserToken, UserToken>()
                .Build();
        
            var factory = new UserTokenFactory();
            var sut = await factory.GetActiveAsync(expectedToken);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            UserTokenDto passedDto = null;
        
            var mockDal = new Mock<IUserTokenDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<UserTokenDto>()))
                .Callback<UserTokenDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserToken, UserToken>()
                .Build();
        
            var factory = new UserTokenFactory();
            var sut = factory.Create();
            
            sut.UserId = dto.UserId;
            sut.Token = dto.Token;
            sut.ExpiresAt = dto.ExpiresAt;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
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
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IUserTokenDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<UserTokenDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IUserToken, UserToken>()
                .Build();
        
            var factory = new UserTokenFactory();
            var sut = factory.Create();
            sut.UserId = dto.UserId;
            sut.Token = dto.Token;
            sut.ExpiresAt = dto.ExpiresAt;
        
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
