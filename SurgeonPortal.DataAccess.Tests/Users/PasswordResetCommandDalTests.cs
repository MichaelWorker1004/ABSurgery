using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.DataAccess.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Users
{
	public class PasswordResetCommandDalTests : TestBase<int>
    {
        #region ResetPasswordAsync
        
        [Test]
        public async Task ResetPasswordAsync_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_user_password]";
            var expectedUserId = Create<int>();
            var expectedOldPassword = Create<string>();
            var expectedNewPassword = Create<string>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                    OldPassword = expectedOldPassword,
                    NewPassword = expectedNewPassword,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<PasswordResetCommandDto>());
        
            var sut = new PasswordResetCommandDal(sqlManager);
            await sut.ResetPasswordAsync(
                expectedOldPassword,
                expectedNewPassword);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
