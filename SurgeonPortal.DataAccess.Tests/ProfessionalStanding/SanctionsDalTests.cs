using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.DataAccess.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.ProfessionalStanding
{
	public class SanctionsDalTests : TestBase<int>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_user_sanctions_byuserid]";
            var expectedUserId = Create<int>();
            var expectedParams = 
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<SanctionsDto>());
        
            var sut = new SanctionsDal(sqlManager);
            await sut.GetByUserIdAsync(expectedUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<SanctionsDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new SanctionsDal(sqlManager);
            var result = await sut.GetByUserIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[ins_user_sanctions]";
            var expectedDto = Create<SanctionsDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new SanctionsDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.UserId,
                    HadDrugAlchoholTreatment = expectedDto.HadDrugAlchoholTreatment,
                    HadHospitalPrivilegesDenied = expectedDto.HadHospitalPrivilegesDenied,
                    HadLicenseRestricted = expectedDto.HadLicenseRestricted,
                    HadHospitalPrivilegesRestricted = expectedDto.HadHospitalPrivilegesRestricted,
                    HadFelonyConviction = expectedDto.HadFelonyConviction,
                    HadCensure = expectedDto.HadCensure,
                    Explanation = expectedDto.Explanation,
                    CreatedByUserId = expectedDto.CreatedByUserId,
                    LastUpdatedByUserId = expectedDto.LastUpdatedByUserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<SanctionsDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new SanctionsDal(sqlManager);
            var result = await sut.InsertAsync(Create<SanctionsDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_user_sanctions_byuserid]";
            var expectedDto = Create<SanctionsDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new SanctionsDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.UserId,
                    HadDrugAlchoholTreatment = expectedDto.HadDrugAlchoholTreatment,
                    HadHospitalPrivilegesDenied = expectedDto.HadHospitalPrivilegesDenied,
                    HadLicenseRestricted = expectedDto.HadLicenseRestricted,
                    HadHospitalPrivilegesRestricted = expectedDto.HadHospitalPrivilegesRestricted,
                    HadFelonyConviction = expectedDto.HadFelonyConviction,
                    HadCensure = expectedDto.HadCensure,
                    Explanation = expectedDto.Explanation,
                    LastUpdatedByUserId = expectedDto.UserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<SanctionsDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new SanctionsDal(sqlManager);
            var result = await sut.UpdateAsync(Create<SanctionsDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
