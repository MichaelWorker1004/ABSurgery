using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Library.Users;
using SurgeonPortal.Shared;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Users
{
    [TestFixture] 
	public class UserClaimReadOnlyListTests : TestBase<int>
    {
        
        //[Test]
        //public async Task GetByIdsAsync_CallsDalCorrectly()
        //{
        //    var expectedUserId = Create<int>();
        //    var expectedApplicationId = Create<int>();
            
        //    var mockDal = new Mock<IUserClaimReadOnlyDal>();
        //    mockDal.Setup(m => m.GetByIdsAsync(
        //        expectedUserId,
        //        expectedApplicationId))
        //        .ReturnsAsync(CreateMany<UserClaimReadOnlyDto>());
        
        //    UseMockServiceProvider()
        //        .WithUserInRoles(ApplicationClaims.User)
        //        .WithRegisteredInstance(mockDal)
        //        .WithBusinessObject<IUserClaimReadOnlyList, UserClaimReadOnlyList>()
        //        .WithBusinessObject<IUserClaimReadOnly, UserClaimReadOnly>()
        //        .Build();
        
        //    var factory = new UserClaimReadOnlyListFactory();
        //    var sut = await factory.GetByIdsAsync(
        //        expectedUserId,
        //        expectedApplicationId);
        
        //    mockDal.VerifyAll();
        //}
        
        //[Test]
        //public async Task GetByIdsAsync_LoadsChildrenCorrectly()
        //{
        //    var expectedDtos = CreateMany<UserClaimReadOnlyDto>();
        
        //    var mockDal = new Mock<IUserClaimReadOnlyDal>();
        //    mockDal.Setup(m => m.GetByIdsAsync(
        //        It.IsAny<int>(),
        //        It.IsAny<int>()))
        //        .ReturnsAsync(expectedDtos);
        
        //    UseMockServiceProvider()
        //        .WithUserInRoles(ApplicationClaims.User)
        //        .WithRegisteredInstance(mockDal)
        //        .WithBusinessObject<IUserClaimReadOnlyList, UserClaimReadOnlyList>()
        //        .WithBusinessObject<IUserClaimReadOnly, UserClaimReadOnly>()
        //        .Build();
        
        //    var factory = new UserClaimReadOnlyListFactory();
        //    var sut = await factory.GetByIdsAsync(
        //        Create<int>(),
        //        Create<int>());
        
        //    Assert.That(sut, Has.Count.EqualTo(3));
        //    expectedDtos.Should().BeEquivalentTo(sut, options => 
        //    {
        //        options.ExcludingMissingMembers();
        //        return options;
        //    });
        //}
	}
}
