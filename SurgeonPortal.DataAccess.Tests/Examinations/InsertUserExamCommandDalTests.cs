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
    public class InsertUserExamCommandDalTests : TestBase<int>
    {
        #region InsertUserExamAsync

        [Test]
        public async Task InsertUserExamAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[ins_user_exam]";
            var expectedUserId = Create<int>();
            var expectedExamHeaderId = Create<int>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                    ExamHeaderId = expectedExamHeaderId
                };

            var sqlManager = new MockSqlConnectionManager();

            var sut = new InsertUserExamCommandDal(sqlManager);
            await sut.InsertUserExamAsync(expectedUserId, expectedExamHeaderId);

            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }

        #endregion
    }
}
