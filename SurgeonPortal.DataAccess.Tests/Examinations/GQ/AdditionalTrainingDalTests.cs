using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations.GQ;
using SurgeonPortal.DataAccess.Examinations.GQ;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Examinations.GQ
{
	public class AdditionalTrainingDalTests : TestBase<int>
    {
        #region GetByTrainingIdAsync
        
        [Test]
        public async Task GetByTrainingIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_additionaltraining_bytrainingid]";
            var expectedTrainingId = Create<int>();
            var expectedParams = 
                new
                {
                    TrainingId = expectedTrainingId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<AdditionalTrainingDto>());
        
            var sut = new AdditionalTrainingDal(sqlManager);
            await sut.GetByTrainingIdAsync(expectedTrainingId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByTrainingIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<AdditionalTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AdditionalTrainingDal(sqlManager);
            var result = await sut.GetByTrainingIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[ins_additionaltraining_bytrainingid]";
            var expectedDto = Create<AdditionalTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AdditionalTrainingDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    DateEnded = expectedDto.DateEnded,
                    DateStarted = expectedDto.DateStarted,
                    Other = expectedDto.Other,
                    InstitutionId = expectedDto.InstitutionId,
                    City = expectedDto.City,
                    StateId = expectedDto.StateId,
                    TypeOfTraining = expectedDto.TypeOfTraining,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<AdditionalTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AdditionalTrainingDal(sqlManager);
            var result = await sut.InsertAsync(Create<AdditionalTrainingDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_additionaltraining_bytrainingid]";
            var expectedDto = Create<AdditionalTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AdditionalTrainingDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    TrainingId = expectedDto.TrainingId,
                    DateEnded = expectedDto.DateEnded,
                    DateStarted = expectedDto.DateStarted,
                    Other = expectedDto.Other,
                    InstitutionId = expectedDto.InstitutionId,
                    City = expectedDto.City,
                    StateId = expectedDto.StateId,
                    TypeOfTraining = expectedDto.TypeOfTraining,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<AdditionalTrainingDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new AdditionalTrainingDal(sqlManager);
            var result = await sut.UpdateAsync(Create<AdditionalTrainingDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
