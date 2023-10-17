using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.DataAccess.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.ProfessionalStanding
{
	public class MedicalLicenseDalTests : TestBase<int>
    {
        #region DeleteAsync
                
        [Test]
        public async Task DeleteAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[del_userlicense]";
            var expectedDto = Create<MedicalLicenseDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            
            var sut = new MedicalLicenseDal(sqlManager);
            await sut.DeleteAsync(expectedDto);
        
            var p =
                new
                {
                    LicenseId = expectedDto.LicenseId,
                    UserId = expectedDto.UserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        #endregion

        #region GetByIdAsync
        
        [Test]
        public async Task GetByIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_userlicenses_byid]";
            var expectedLicenseId = Create<int>();
            var expectedParams =
                new
                {
                    LicenseId = expectedLicenseId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<MedicalLicenseDto>());
        
            var sut = new MedicalLicenseDal(sqlManager);
            await sut.GetByIdAsync(expectedLicenseId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<MedicalLicenseDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new MedicalLicenseDal(sqlManager);
            var result = await sut.GetByIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[insert_userlicenses]";
            var expectedDto = Create<MedicalLicenseDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new MedicalLicenseDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.UserId,
                    IssuingStateId = expectedDto.IssuingStateId,
                    LicenseNumber = expectedDto.LicenseNumber,
                    LicenseTypeId = expectedDto.LicenseTypeId,
                    ReportingOrganization = expectedDto.ReportingOrganization,
                    IssueDate = expectedDto.IssueDate,
                    ExpireDate = expectedDto.ExpireDate,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<MedicalLicenseDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new MedicalLicenseDal(sqlManager);
            var result = await sut.InsertAsync(Create<MedicalLicenseDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_userlicenses]";
            var expectedDto = Create<MedicalLicenseDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new MedicalLicenseDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    LicenseId = expectedDto.LicenseId,
                    UserId = expectedDto.UserId,
                    IssuingStateId = expectedDto.IssuingStateId,
                    LicenseNumber = expectedDto.LicenseNumber,
                    LicenseTypeId = expectedDto.LicenseTypeId,
                    ReportingOrganization = expectedDto.ReportingOrganization,
                    IssueDate = expectedDto.IssueDate,
                    ExpireDate = expectedDto.ExpireDate,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<MedicalLicenseDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new MedicalLicenseDal(sqlManager);
            var result = await sut.UpdateAsync(Create<MedicalLicenseDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
