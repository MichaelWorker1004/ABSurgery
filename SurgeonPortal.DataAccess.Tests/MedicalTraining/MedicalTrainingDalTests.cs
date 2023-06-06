using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.DataAccess.MedicalTraining;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.MedicalTraining
{
	public class MedicalTrainingDalTests : TestBase<string>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_medical_training_byuserid]";
            var expectedUserId = Create<int>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<MedicalTrainingDto>());
        
            var sut = new MedicalTrainingDal(sqlManager);
            await sut.GetByUserIdAsync();
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<MedicalTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new MedicalTrainingDal(sqlManager);
            var result = await sut.GetByUserIdAsync();
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[ins_medical_training]";
            var expectedDto = Create<MedicalTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new MedicalTrainingDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    GraduateProfileId = expectedDto.GraduateProfileId,
                    MedicalSchoolName = expectedDto.MedicalSchoolName,
                    MedicalSchoolCity = expectedDto.MedicalSchoolCity,
                    MedicalSchoolStateId = expectedDto.MedicalSchoolStateId,
                    MedicalSchoolCountryId = expectedDto.MedicalSchoolCountryId,
                    DegreeId = expectedDto.DegreeId,
                    MedicalSchoolCompletionYear = expectedDto.MedicalSchoolCompletionYear,
                    ResidencyProgramName = expectedDto.ResidencyProgramName,
                    ResidencyCompletionYear = expectedDto.ResidencyCompletionYear,
                    ResidencyProgramOther = expectedDto.ResidencyProgramOther,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<MedicalTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new MedicalTrainingDal(sqlManager);
            var result = await sut.InsertAsync(Create<MedicalTrainingDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_medical_training]";
            var expectedDto = Create<MedicalTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new MedicalTrainingDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    Id = expectedDto.Id,
                    GraduateProfileId = expectedDto.GraduateProfileId,
                    MedicalSchoolName = expectedDto.MedicalSchoolName,
                    MedicalSchoolCity = expectedDto.MedicalSchoolCity,
                    MedicalSchoolStateId = expectedDto.MedicalSchoolStateId,
                    MedicalSchoolCountryId = expectedDto.MedicalSchoolCountryId,
                    DegreeId = expectedDto.DegreeId,
                    MedicalSchoolCompletionYear = expectedDto.MedicalSchoolCompletionYear,
                    ResidencyProgramName = expectedDto.ResidencyProgramName,
                    ResidencyCompletionYear = expectedDto.ResidencyCompletionYear,
                    ResidencyProgramOther = expectedDto.ResidencyProgramOther,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<MedicalTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new MedicalTrainingDal(sqlManager);
            var result = await sut.UpdateAsync(Create<MedicalTrainingDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
