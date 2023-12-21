using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.DataAccess.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Users
{
	public class CreateForgotPasswordCommandDalTests : TestBase<int>
    {
        #region SendForgotPasswordEmailAsync
        
        [Test]
        public async Task SendForgotPasswordEmailAsync_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[ins_reset_guid]";
            var expectedUserName = Create<string>();
            var expectedParams = 
                new
                {
                    UserName = expectedUserName,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<CreateForgotPasswordCommandDto>());
        
            var sut = new CreateForgotPasswordCommandDal(sqlManager);
            await sut.SendForgotPasswordEmailAsync(expectedUserName);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
