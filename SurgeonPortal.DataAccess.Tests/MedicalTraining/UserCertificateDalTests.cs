using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.DataAccess.MedicalTraining;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.MedicalTraining
{
    public class UserCertificateDalTests : TestBase<int>
    {
        private readonly ILogger<UserCertificateDal> _logger;

        public UserCertificateDalTests(ILogger<UserCertificateDal> logger)
        {
            _logger = logger;
        }


        #region DeleteAsync

        //[Test]
        public async Task DeleteAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[del_usercertificate]";
            var expectedDto = Create<UserCertificateDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            
            var sut = new UserCertificateDal(sqlManager, _logger);
            await sut.DeleteAsync(expectedDto);
        
            var p =
                new
                {
                    CertificateId = expectedDto.CertificateId,
                    UserId = expectedDto.UserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        #endregion

        #region GetByIdAsync
        
        //[Test]
        public async Task GetByIdAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[get_usercertificates_byid]";
            var expectedCertificateId = Create<int>();
            var expectedParams = 
                new
                {
                    CertificateId = expectedCertificateId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<UserCertificateDto>());
        
            var sut = new UserCertificateDal(sqlManager, _logger);
            await sut.GetByIdAsync(expectedCertificateId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        //[Test]
        public async Task GetByIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserCertificateDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserCertificateDal(sqlManager, _logger);
            var result = await sut.GetByIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        //[Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[upsert_usercertificates]";
            var expectedDto = Create<UserCertificateDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserCertificateDal(sqlManager, _logger);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.UserId,
                    DocumentId = expectedDto.DocumentId,
                    CertificateTypeId = expectedDto.CertificateTypeId,
                    IssueDate = expectedDto.IssueDate,
                    CertificateNumber = expectedDto.CertificateNumber,
                    CreatedByUserId = expectedDto.UserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        //[Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserCertificateDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserCertificateDal(sqlManager, _logger);
            var result = await sut.InsertAsync(Create<UserCertificateDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
