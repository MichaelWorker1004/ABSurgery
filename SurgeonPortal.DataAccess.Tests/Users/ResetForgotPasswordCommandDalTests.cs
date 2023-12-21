using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.DataAccess.Users;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Users
{
	public class ResetForgotPasswordCommandDalTests : TestBase<int>
    {
        #region ResetForgotPasswordAsync
        
        [Test]
        public async Task ResetForgotPasswordAsync_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[reset_passwork_using_guid]";
            var expectedResetGUID = Create<Guid>();
            var expectedNewPassword = Create<string>();
            var expectedParams = 
                new
                {
                    ResetGUID = expectedResetGUID,
                    NewPassword = expectedNewPassword,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<ResetForgotPasswordCommandDto>());
        
            var sut = new ResetForgotPasswordCommandDal(sqlManager);
            await sut.ResetForgotPasswordAsync(
                expectedResetGUID,
                expectedNewPassword);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
