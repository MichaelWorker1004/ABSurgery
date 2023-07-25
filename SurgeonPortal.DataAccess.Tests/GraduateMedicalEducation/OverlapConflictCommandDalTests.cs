using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.DataAccess.GraduateMedicalEducation;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.GraduateMedicalEducation
{
	public class OverlapConflictCommandDalTests : TestBase<string>
    {
        #region CheckOverlapConflicts
        
        [Test]
        public void CheckOverlapConflicts_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_gme_overlap_conflict]";
            var expectedUserId = Create<int>();
            var expectedStartDate = Create<DateTime>();
            var expectedEndDate = Create<DateTime>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                    StartDate = expectedStartDate,
                    EndDate = expectedEndDate,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<OverlapConflictCommandDto>());
        
            var sut = new OverlapConflictCommandDal(sqlManager);
            sut.CheckOverlapConflicts(
                expectedUserId,
                expectedStartDate,
                expectedEndDate);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
