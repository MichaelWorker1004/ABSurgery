using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.DataAccess.Users;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Users
{
	public class PasswordValidationCommandTests : TestBase<int>
    {
        #region ValidateAsync
        
        [Test]
        public void Validate_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_user_passwordvalidate]";
            var expectedUserId = Create<int>();
            var expectedPassword = Create<string>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                    Password = expectedPassword,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<PasswordValidationCommandDto>());
        
            var sut = new PasswordValidationCommandDal(sqlManager);
            sut.Validate(
                expectedUserId,
                expectedPassword);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
