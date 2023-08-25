using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.CE;
using SurgeonPortal.Library.CE;
using SurgeonPortal.Library.Contracts.CE;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.CE
{
    [TestFixture] 
	public class ExamScoreTests : TestBase<string>
    {
        private ExamScoreDto CreateValidDto()
        {     
            var dto = Create<ExamScoreDto>();

            dto.ExamScheduleScoreId = Create<int>();
            dto.ExamScheduleId = Create<int>();
            dto.ExaminerUserId = Create<int>();
            dto.ExaminerScore = Create<int>();
            dto.Submitted = Create<bool?>();
            dto.SubmittedDateTimeUTC = Create<System.DateTime?>();
            dto.CreatedByUserId = Create<int>();
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = Create<int>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
    
            return dto;
        }

            

        #region GetByIdAsync ExamScore
        
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedExamScheduleScoreId = Create<int>();
            
            var mockDal = new Mock<IExamScoreDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScheduleScoreId))
                .ReturnsAsync(Create<ExamScoreDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamScore, ExamScore>()
                .Build();
        
            var factory = new ExamScoreFactory();
            var sut = await factory.GetByIdAsync(expectedExamScheduleScoreId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetById_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IExamScoreDal>();
            mockDal.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamScore, ExamScore>()
                .Build();
        
            var factory = new ExamScoreFactory();
            var sut = await factory.GetByIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            ExamScoreDto passedDto = null;
        
            var mockDal = new Mock<IExamScoreDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<ExamScoreDto>()))
                .Callback<ExamScoreDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamScore, ExamScore>()
                .Build();
        
            var factory = new ExamScoreFactory();
            var sut = factory.Create();
            
            sut.ExamScheduleScoreId = dto.ExamScheduleScoreId;
            sut.ExamScheduleId = dto.ExamScheduleId;
            sut.ExaminerUserId = dto.ExaminerUserId;
            sut.ExaminerScore = dto.ExaminerScore;
            sut.Submitted = dto.Submitted;
            sut.SubmittedDateTimeUTC = dto.SubmittedDateTimeUTC;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.ExamScheduleScoreId)
                .Excluding(m => m.ExaminerUserId)
                .Excluding(m => m.ExaminerScore)
                .Excluding(m => m.Submitted)
                .Excluding(m => m.SubmittedDateTimeUTC)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IExamScoreDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<ExamScoreDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamScore, ExamScore>()
                .Build();
        
            var factory = new ExamScoreFactory();
            var sut = factory.Create();
            sut.ExamScheduleId = Create<int>();
        
            await sut.SaveAsync();
            
            dto.Should().BeEquivalentTo(sut,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .ExcludingMissingMembers());
        }
        
        #endregion

        #region Update
        
        [Test]
        public async Task Update_CallsDalCorrectly()
        {
            var expectedExamScheduleScoreId = Create<int>();
            
            var dto = Create<ExamScoreDto>();
            ExamScoreDto passedDto = null;
        
            var mockDal = new Mock<IExamScoreDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScheduleScoreId))
                        .ReturnsAsync(dto);
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<ExamScoreDto>()))
                .Callback<ExamScoreDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamScore, ExamScore>()
                .Build();
        
            var factory = new ExamScoreFactory();
            var sut = await factory.GetByIdAsync(expectedExamScheduleScoreId);
            
            sut.ExamScheduleScoreId = dto.ExamScheduleScoreId;
            sut.ExamScheduleId = dto.ExamScheduleId;
            sut.ExaminerUserId = dto.ExaminerUserId;
            sut.ExaminerScore = dto.ExaminerScore;
            sut.Submitted = dto.Submitted;
            sut.SubmittedDateTimeUTC = dto.SubmittedDateTimeUTC;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.ExamScheduleScoreId = dto.ExamScheduleScoreId;
            sut.ExamScheduleId = dto.ExamScheduleId;
            sut.ExaminerUserId = dto.ExaminerUserId;
            sut.ExaminerScore = dto.ExaminerScore;
            sut.Submitted = dto.Submitted;
            sut.SubmittedDateTimeUTC = dto.SubmittedDateTimeUTC;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.ExamScheduleId)
                    .Excluding(m => m.ExaminerUserId)
                    .Excluding(m => m.Submitted)
                    .Excluding(m => m.SubmittedDateTimeUTC)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            var expectedExamScheduleScoreId = Create<int>();
            
            var dto = Create<ExamScoreDto>();
        
            var mockDal = new Mock<IExamScoreDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScheduleScoreId))
                        .ReturnsAsync(Create<ExamScoreDto>());
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<ExamScoreDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamScore, ExamScore>()
                .Build();
        
            var factory = new ExamScoreFactory();
            var sut = await factory.GetByIdAsync(expectedExamScheduleScoreId);
            sut.ExamScheduleScoreId = Create<int>();
        
            await sut.SaveAsync();
            
            dto.Should().BeEquivalentTo(sut,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        }
        
        #endregion
	}
}
