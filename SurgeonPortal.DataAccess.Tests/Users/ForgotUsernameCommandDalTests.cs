using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.DataAccess.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Users
{
	public class ForgotUsernameCommandDalTests : TestBase<int>
    {
        #region SendForgotUsernameEmailAsync
        
        [Test]
        public async Task SendForgotUsernameEmailAsync_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_forgotten_username]";
            var expectedEmail = Create<string>();
            var expectedParams = 
                new
                {
                    Email = expectedEmail,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<ForgotUsernameCommandDto>());
        
            var sut = new ForgotUsernameCommandDal(sqlManager);
            await sut.SendForgotUsernameEmailAsync(expectedEmail);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
