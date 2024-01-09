using NUnit.Framework;
using SurgeonPortal.DataAccess.Examinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Examinations
{
    public class RegistrationCompleteCommandDalTests : TestBase<int>
    {
        #region CompleteRegistrationAsync

        [Test]
        public async Task CompleteRegistrationAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_user_exam_registration_complete]";
            var expectedUserId = Create<int>();
            var expectedExamHeaderId = Create<int>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                    ExamHeaderId = expectedExamHeaderId
                };

            var sqlManager = new MockSqlConnectionManager();

            var sut = new RegistrationCompleteCommandDal(sqlManager);
            await sut.CompleteRegistrationAsync(expectedUserId, expectedExamHeaderId);

            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }

        #endregion
    }
}
