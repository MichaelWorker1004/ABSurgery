using FluentAssertions;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.DataAccess.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.UnitTest;
using Ytg.UnitTest.ConnectionManager;

namespace SurgeonPortal.DataAccess.Tests.ProfessionalStanding
{
	public class UserAppointmentDalTests : TestBase<string>
    {
        #region DeleteAsync
                
        [Test]
        public async Task DeleteAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[del_userhospappt]";
            var expectedDto = Create<UserAppointmentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            
            var sut = new UserAppointmentDal(sqlManager);
            await sut.DeleteAsync(expectedDto);
        
            var p =
                new
                {
                    ApptId = expectedDto.ApptId,
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
            var expectedSprocName = "[dbo].[get_userhospappts_byid]";
            var expectedApptId = Create<int>();
            var expectedParams =
                new
                {
                    ApptId = expectedApptId,
                };
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(Create<UserAppointmentDto>());
        
            var sut = new UserAppointmentDal(sqlManager);
            await sut.GetByIdAsync(expectedApptId);
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(expectedParams));
        }
        
        [Test]
        public async Task GetByIdAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserAppointmentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserAppointmentDal(sqlManager);
            var result = await sut.GetByIdAsync(Create<int>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region InsertAsync
        
        [Test]
        public async Task InsertAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[insert_userhospappt]";
            var expectedDto = Create<UserAppointmentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserAppointmentDal(sqlManager);
            await sut.InsertAsync(expectedDto);
        
            var p =
                new
                {
                    PracticeTypeId = expectedDto.PracticeTypeId,
                    AppointmentTypeId = expectedDto.AppointmentTypeId,
                    OrganizationTypeId = expectedDto.OrganizationTypeId,
                    StateCode = expectedDto.StateCode,
                    OrganizationId = expectedDto.OrganizationId,
                    AuthorizingOfficial = expectedDto.AuthorizingOfficial,
                    Other = expectedDto.Other,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task InsertAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserAppointmentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserAppointmentDal(sqlManager);
            var result = await sut.InsertAsync(Create<UserAppointmentDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region UpdateAsync
        
        [Test]
        public async Task UpdateAsync_ExecutesSprocCorrectly()
        {
            var expectedSprocName = "[dbo].[update_userhospappt]";
            var expectedDto = Create<UserAppointmentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserAppointmentDal(sqlManager);
            await sut.UpdateAsync(expectedDto);
        
            var p =
                new
                {
                    ApptId = expectedDto.ApptId,
                    PracticeTypeId = expectedDto.PracticeTypeId,
                    AppointmentTypeId = expectedDto.AppointmentTypeId,
                    OrganizationTypeId = expectedDto.OrganizationTypeId,
                    StateCode = expectedDto.StateCode,
                    OrganizationId = expectedDto.OrganizationId,
                    AuthorizingOfficial = expectedDto.AuthorizingOfficial,
                    Other = expectedDto.Other,
                };
        
            Assert.That(sqlManager.SqlConnection.ShouldCallStoredProcedure(expectedSprocName));
            Assert.That(sqlManager.SqlConnection.ShouldPassParameters(p));
        }
        
        [Test]
        public async Task UpdateAsync_YieldsCorrectResult()
        {
            var expectedDto = Create<UserAppointmentDto>();
        
            var sqlManager = new MockSqlConnectionManager();
            sqlManager.AddRecord(expectedDto);
        
            var sut = new UserAppointmentDal(sqlManager);
            var result = await sut.UpdateAsync(Create<UserAppointmentDto>());
        
            expectedDto.Should().BeEquivalentTo(result,
                options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
