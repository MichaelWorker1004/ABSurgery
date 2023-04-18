using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.DataAccess.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Users
{
	public class AppUserReadOnlyDalTests : TestBase<string>
    {
        #region GetByCredentialsAsync
        
        //[Test]
        //public async Task GetByCredentialsAsync_ExecutesSprocCorrectly()
        //{
        //    var expectedSprocName = "[dbo].[get_userlogin]";
        //    var expectedEmailAddress = Create<string>();
        //    var expectedPassword = Create<string>();
        //    var expectedParams =
        //        new
        //        {
        //            EmailAddress = expectedEmailAddress,
        //            Password = expectedPassword,
        //        };
        
        //    var sqlManager = new MockSqlConnectionManager();
        //    sqlManager.AddRecord(Create<AppUserReadOnlyDto>());
        
        //    var sut = new AppUserReadOnlyDal(sqlManager);
        //    await sut.GetByCredentialsAsync(
        //        expectedEmailAddress,
        //        expectedPassword);
        
        //    Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
        //    Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        //}
        
        //[Test]
        //public async Task GetByCredentialsAsync_YieldsCorrectResult()
        //{
        //    var expectedDto = Create<AppUserReadOnlyDto>();
        
        //    var sqlManager = new MockSqlConnectionManager();
        //    sqlManager.AddRecord(expectedDto);
        
        //    var sut = new AppUserReadOnlyDal(sqlManager);
        //    var result = await sut.GetByCredentialsAsync(
        //        Create<string>(),
        //        Create<string>());
        
        //    expectedDto.Should().BeEquivalentTo(result,
        //        options => options.ExcludingMissingMembers());
        //}
        
        //#endregion

        //#region GetByTokenAsync
        
        //[Test]
        //public async Task GetByTokenAsync_ExecutesSprocCorrectly()
        //{
        //    var expectedSprocName = "[dbo].[get_user_bytoken]";
        //    var expectedToken = Create<string>();
        //    var expectedParams =
        //        new
        //        {
        //            Token = expectedToken,
        //        };
        
        //    var sqlManager = new MockSqlConnectionManager();
        //    sqlManager.AddRecord(Create<AppUserReadOnlyDto>());
        
        //    var sut = new AppUserReadOnlyDal(sqlManager);
        //    await sut.GetByTokenAsync(expectedToken);
        
        //    Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
        //    Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        //}
        
        //[Test]
        //public async Task GetByTokenAsync_YieldsCorrectResult()
        //{
        //    var expectedDto = Create<AppUserReadOnlyDto>();
        
        //    var sqlManager = new MockSqlConnectionManager();
        //    sqlManager.AddRecord(expectedDto);
        
        //    var sut = new AppUserReadOnlyDal(sqlManager);
        //    var result = await sut.GetByTokenAsync(Create<string>());
        
        //    expectedDto.Should().BeEquivalentTo(result,
        //        options => options.ExcludingMissingMembers());
        //}
        
        #endregion
	}
}
