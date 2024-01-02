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
	public class VerifyForgotGuidCommandDalTests : TestBase<int>
    {
        #region VerifyForgotPasswordGuidAsync
        
        [Test]
        public async Task VerifyForgotPasswordGuidAsync_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_guid_status]";
            var expectedResetGUID = Create<Guid>();
            var expectedParams = 
                new
                {
                    ResetGUID = expectedResetGUID,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<VerifyForgotGuidCommandDto>());
        
            var sut = new VerifyForgotGuidCommandDal(sqlManager);
            await sut.VerifyForgotPasswordGuidAsync(expectedResetGUID);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
