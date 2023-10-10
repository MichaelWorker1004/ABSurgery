using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.DataAccess.GraduateMedicalEducation;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.GraduateMedicalEducation
{
	public class RotationDalTests : TestBase<int>
    {
        #region DeleteAsync
                
        [Test]
        public async Task DeleteAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[delete_gmerotations_byid]";
            var expectedDto = Create<RotationDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            
            var sut = new RotationDal(sqlManager);
            await sut.DeleteAsync(expectedDto);
        
            var p =
                new
                {
                    Id = expectedDto.Id,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        #endregion

        #region GetByIdAsync
        
        [Test]
        public async Task GetByIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_gmerotations_byid]";
            var expectedId = Create<int>();
            var expectedParams =
                new
                {
                    Id = expectedId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<RotationDto>());
        
            var sut = new RotationDal(sqlManager);
            await sut.GetByIdAsync(expectedId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<RotationDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new RotationDal(sqlManager);
            var result = await sut.GetByIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[ins_gmerotations]";
            var expectedDto = Create<RotationDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new RotationDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.,
                    StartDate = expectedDto.StartDate,
                    EndDate = expectedDto.EndDate,
                    ClinicalLevelId = expectedDto.ClinicalLevelId,
                    ClinicalActivityId = expectedDto.ClinicalActivityId,
                    ProgramName = expectedDto.ProgramName,
                    NonSurgicalActivity = expectedDto.NonSurgicalActivity,
                    AlternateInstitutionName = expectedDto.AlternateInstitutionName,
                    IsInternationalRotation = expectedDto.IsInternationalRotation,
                    Other = expectedDto.Other,
                    FourMonthRotationExplain = expectedDto.FourMonthRotationExplain,
                    NonPrimaryExplain = expectedDto.NonPrimaryExplain,
                    NonClinicalExplain = expectedDto.NonClinicalExplain,
                    CreatedByUserId = expectedDto.,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<RotationDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new RotationDal(sqlManager);
            var result = await sut.InsertAsync(Create<RotationDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_gmerotations]";
            var expectedDto = Create<RotationDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new RotationDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    Id = expectedDto.Id,
                    UserId = expectedDto.,
                    StartDate = expectedDto.StartDate,
                    EndDate = expectedDto.EndDate,
                    ClinicalLevelId = expectedDto.ClinicalLevelId,
                    ClinicalActivityId = expectedDto.ClinicalActivityId,
                    ProgramName = expectedDto.ProgramName,
                    NonSurgicalActivity = expectedDto.NonSurgicalActivity,
                    AlternateInstitutionName = expectedDto.AlternateInstitutionName,
                    IsInternationalRotation = expectedDto.IsInternationalRotation,
                    Other = expectedDto.Other,
                    FourMonthRotationExplain = expectedDto.FourMonthRotationExplain,
                    NonPrimaryExplain = expectedDto.NonPrimaryExplain,
                    NonClinicalExplain = expectedDto.NonClinicalExplain,
                    LastUpdatedByUserId = expectedDto.,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<RotationDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new RotationDal(sqlManager);
            var result = await sut.UpdateAsync(Create<RotationDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
