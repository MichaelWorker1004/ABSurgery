using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.ContinuousCertification;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.ContinuousCertification
{
	public class OutcomeRegistryDalTests : TestBase<string>
    {
        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_outcomeregistry_getbyuserid]";
            var expectedUserId = Create<int>();
            var expectedParams =
                new
                {
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<OutcomeRegistryDto>());
        
            var sut = new OutcomeRegistryDal(sqlManager);
            await sut.GetByUserIdAsync(expectedUserId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByUserIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<OutcomeRegistryDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new OutcomeRegistryDal(sqlManager);
            var result = await sut.GetByUserIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[ins_outcomeregistry_getbyuserid]";
            var expectedDto = Create<OutcomeRegistryDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new OutcomeRegistryDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.UserId,
                    SurgeonSpecificRegistry = expectedDto.SurgeonSpecificRegistry,
                    RegistryComments = expectedDto.RegistryComments,
                    RegisteredWithACHQC = expectedDto.RegisteredWithACHQC,
                    RegisteredWithCESQIP = expectedDto.RegisteredWithCESQIP,
                    RegisteredWithMBSAQIP = expectedDto.RegisteredWithMBSAQIP,
                    RegisteredWithABA = expectedDto.RegisteredWithABA,
                    RegisteredWithASBS = expectedDto.RegisteredWithASBS,
                    RegisteredWithStatewideCollaboratives = expectedDto.RegisteredWithStatewideCollaboratives,
                    RegisteredWithABMS = expectedDto.RegisteredWithABMS,
                    RegisteredWithNCDB = expectedDto.RegisteredWithNCDB,
                    RegisteredWithRQRS = expectedDto.RegisteredWithRQRS,
                    RegisteredWithNSQIP = expectedDto.RegisteredWithNSQIP,
                    RegisteredWithNTDB = expectedDto.RegisteredWithNTDB,
                    RegisteredWithSTS = expectedDto.RegisteredWithSTS,
                    RegisteredWithTQIP = expectedDto.RegisteredWithTQIP,
                    RegisteredWithUNOS = expectedDto.RegisteredWithUNOS,
                    RegisteredWithNCDR = expectedDto.RegisteredWithNCDR,
                    RegisteredWithSVS = expectedDto.RegisteredWithSVS,
                    RegisteredWithELSO = expectedDto.RegisteredWithELSO,
                    UserConfirmed = expectedDto.UserConfirmed,
                    UserConfirmedDateUtc = expectedDto.UserConfirmedDateUtc,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<OutcomeRegistryDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new OutcomeRegistryDal(sqlManager);
            var result = await sut.InsertAsync(Create<OutcomeRegistryDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_outcomeregistry_getbyuserid]";
            var expectedDto = Create<OutcomeRegistryDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new OutcomeRegistryDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    SurgeonSpecificRegistry = expectedDto.SurgeonSpecificRegistry,
                    RegistryComments = expectedDto.RegistryComments,
                    RegisteredWithACHQC = expectedDto.RegisteredWithACHQC,
                    RegisteredWithCESQIP = expectedDto.RegisteredWithCESQIP,
                    RegisteredWithMBSAQIP = expectedDto.RegisteredWithMBSAQIP,
                    RegisteredWithABA = expectedDto.RegisteredWithABA,
                    RegisteredWithASBS = expectedDto.RegisteredWithASBS,
                    RegisteredWithStatewideCollaboratives = expectedDto.RegisteredWithStatewideCollaboratives,
                    RegisteredWithABMS = expectedDto.RegisteredWithABMS,
                    RegisteredWithNCDB = expectedDto.RegisteredWithNCDB,
                    RegisteredWithRQRS = expectedDto.RegisteredWithRQRS,
                    RegisteredWithNSQIP = expectedDto.RegisteredWithNSQIP,
                    RegisteredWithNTDB = expectedDto.RegisteredWithNTDB,
                    RegisteredWithSTS = expectedDto.RegisteredWithSTS,
                    RegisteredWithTQIP = expectedDto.RegisteredWithTQIP,
                    RegisteredWithUNOS = expectedDto.RegisteredWithUNOS,
                    RegisteredWithNCDR = expectedDto.RegisteredWithNCDR,
                    RegisteredWithSVS = expectedDto.RegisteredWithSVS,
                    RegisteredWithELSO = expectedDto.RegisteredWithELSO,
                    UserConfirmed = expectedDto.UserConfirmed,
                    UserConfirmedDateUtc = expectedDto.UserConfirmedDateUtc,
                    UserId = expectedDto.UserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<OutcomeRegistryDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new OutcomeRegistryDal(sqlManager);
            var result = await sut.UpdateAsync(Create<OutcomeRegistryDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}