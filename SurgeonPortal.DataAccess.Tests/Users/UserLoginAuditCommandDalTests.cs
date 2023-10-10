using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.DataAccess.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Users
{
	public class UserLoginAuditCommandDalTests : TestBase<int>
    {
        #region AuditAsync
        
        //[Test]
        //public async Task AuditAsync_CallsSprocCorrectly()
        //{
        //    var expectedSprocName = "[dbo].[insert_user_login_audit]";
        //    var expectedUserId = Create<int>();
        //    var expectedEmailAddress = Create<string>();
        //    var expectedApplicationId = Create<int>();
        //    var expectedLoginIpAddress = Create<string>();
        //    var expectedLoginUserAgent = Create<string>();
        //    var expectedLoginSuccess = Create<bool>();
        //    var expectedLoginFailureReason = Create<string>();
        //    var expectedCreatedByUserId = Create<int>();
        //    var expectedLastUpdatedByUserId = Create<int>();
        //    var expectedParams =
        //        new
        //        {
        //            UserId = expectedUserId,
        //            EmailAddress = expectedEmailAddress,
        //            ApplicationId = expectedApplicationId,
        //            LoginIpAddress = expectedLoginIpAddress,
        //            LoginUserAgent = expectedLoginUserAgent,
        //            LoginSuccess = expectedLoginSuccess,
        //            LoginFailureReason = expectedLoginFailureReason,
        //            CreatedByUserId = expectedCreatedByUserId,
        //            LastUpdatedByUserId = expectedLastUpdatedByUserId,
        //        };
        
        //    var sqlManager = new MockSqlConnectionManager();
        //    sqlManager.AddRecord(Create<UserLoginAuditCommandDto>());
        
        //    var sut = new UserLoginAuditCommandDal(sqlManager);
        //    await sut.AuditAsync(
        //        expectedUserId,
        //        expectedEmailAddress,
        //        expectedApplicationId,
        //        expectedLoginIpAddress,
        //        expectedLoginUserAgent,
        //        expectedLoginSuccess,
        //        expectedLoginFailureReason);
        
        //    Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
        //    Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        //}
        
        #endregion
	}
}
