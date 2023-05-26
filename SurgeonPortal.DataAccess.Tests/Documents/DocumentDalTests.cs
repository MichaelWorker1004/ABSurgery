using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Documents;
using SurgeonPortal.DataAccess.Documents;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.Documents
{
	public class DocumentDalTests : TestBase<string>
    {
        #region DeleteAsync
                
        [Test]
        public async Task DeleteAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[delete_userdocument_byid]";
            var expectedDto = Create<DocumentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            
            var sut = new DocumentDal(sqlManager);
            await sut.DeleteAsync(expectedDto);
        
            var p =
                new
                {
                    Id = expectedDto.Id,
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
            var expectedSprocName = "[dbo].[get_document_byid]";
            var expectedId = Create<int>();
            var expectedUserId = Create<int>();
            var expectedParams =
                new
                {
                    Id = expectedId,
                    UserId = expectedUserId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<DocumentDto>());
        
            var sut = new DocumentDal(sqlManager);
            await sut.GetByIdAsync(expectedId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<DocumentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new DocumentDal(sqlManager);
            var result = await sut.GetByIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[ins_userdocument]";
            var expectedDto = Create<DocumentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new DocumentDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    UserId = expectedDto.UserId,
                    StreamId = expectedDto.StreamId,
                    DocumentName = expectedDto.DocumentName,
                    DocumentTypeId = expectedDto.DocumentTypeId,
                    InternalViewOnly = expectedDto.InternalViewOnly,
                    CreatedByUserId = expectedDto.CreatedByUserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<DocumentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new DocumentDal(sqlManager);
            var result = await sut.InsertAsync(Create<DocumentDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_userdocument]";
            var expectedDto = Create<DocumentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new DocumentDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    Id = expectedDto.Id,
                    UserId = expectedDto.UserId,
                    StreamId = expectedDto.StreamId,
                    DocumentName = expectedDto.DocumentName,
                    DocumentTypeId = expectedDto.DocumentTypeId,
                    InternalViewOnly = expectedDto.InternalViewOnly,
                    LastUpdatedByUserId = expectedDto.LastUpdatedByUserId,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<DocumentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new DocumentDal(sqlManager);
            var result = await sut.UpdateAsync(Create<DocumentDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
