using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.DataAccess.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.ProfessionalStanding
{
	public class GetClinicallyActiveCommandDalTests : TestBase<string>
    {
        #region GetClinicallyActiveByUserId
        
        [Test]
        public void GetClinicallyActiveByUserId_CallsSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_user_clinically_active_byuserid]";
            var expectedUserId = Create<int>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<GetClinicallyActiveCommandDto>());
        
            var sut = new GetClinicallyActiveCommandDal(sqlManager);
            sut.GetClinicallyActiveByUserId();
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        #endregion
	}
}
