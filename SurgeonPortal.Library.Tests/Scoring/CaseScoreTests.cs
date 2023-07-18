using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using SurgeonPortal.Library.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Scoring
{
    [TestFixture] 
	public class CaseScoreTests : TestBase<string>
    {
        private CaseScoreDto CreateValidDto()
        {     
            var dto = Create<CaseScoreDto>();

            dto.ExamScoringId = Create<int>();
            dto.ExamCaseId = Create<int>();
            dto.ExaminerUserId = Create<int>();
            dto.ExamineeUserId = Create<int>();
            dto.ExamineeFirstName = Create<string>();
            dto.ExamineeMiddleName = Create<string>();
            dto.ExamineeLastName = Create<string>();
            dto.ExamineeSuffix = Create<string>();
            dto.Score = Create<int>();
            dto.CriticalFail = Create<bool?>();
            dto.Remarks = Create<string>();
            dto.CreatedByUserId = Create<int>();
            dto.CreatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedAtUtc = Create<System.DateTime>();
            dto.LastUpdatedByUserId = Create<int>();
    
            return dto;
        }

            

        #region DeleteAsync
        
        [Test]
        public async Task Delete_CallsDalCorrectly()
        {
            var expectedExamScoringId = Create<int>();
            
            var dto = CreateValidDto();
            CaseScoreDto passedDto = null;
        
            var mockDal = new Mock<ICaseScoreDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScoringId))
                .ReturnsAsync(dto);
            mockDal.Setup(m => m.DeleteAsync(It.IsAny<CaseScoreDto>()))
                .Callback<CaseScoreDto>((p) => passedDto = p)
                .Returns(Task.CompletedTask);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseScore, CaseScore>()
                .Build();
        
            var factory = new CaseScoreFactory();
            var sut = await factory.GetByIdAsync(expectedExamScoringId);
            sut.Delete();
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(sut,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        #endregion

        #region GetByIdAsync CaseScore
        
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedExamScoringId = Create<int>();
            
            var mockDal = new Mock<ICaseScoreDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScoringId))
                .ReturnsAsync(Create<CaseScoreDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseScore, CaseScore>()
                .Build();
        
            var factory = new CaseScoreFactory();
            var sut = await factory.GetByIdAsync(expectedExamScoringId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetById_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<ICaseScoreDal>();
            mockDal.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseScore, CaseScore>()
                .Build();
        
            var factory = new CaseScoreFactory();
            var sut = await factory.GetByIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            CaseScoreDto passedDto = null;
        
            var mockDal = new Mock<ICaseScoreDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<CaseScoreDto>()))
                .Callback<CaseScoreDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseScore, CaseScore>()
                .Build();
        
            var factory = new CaseScoreFactory();
            var sut = factory.Create();
            
            sut.ExamScoringId = dto.ExamScoringId;
            sut.ExamCaseId = dto.ExamCaseId;
            sut.ExaminerUserId = dto.ExaminerUserId;
            sut.ExamineeUserId = dto.ExamineeUserId;
            sut.ExamineeFirstName = dto.ExamineeFirstName;
            sut.ExamineeMiddleName = dto.ExamineeMiddleName;
            sut.ExamineeLastName = dto.ExamineeLastName;
            sut.ExamineeSuffix = dto.ExamineeSuffix;
            sut.Score = dto.Score;
            sut.CriticalFail = dto.CriticalFail;
            sut.Remarks = dto.Remarks;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.ExamScoringId)
                .Excluding(m => m.ExaminerUserId)
                .Excluding(m => m.ExamineeFirstName)
                .Excluding(m => m.ExamineeMiddleName)
                .Excluding(m => m.ExamineeLastName)
                .Excluding(m => m.ExamineeSuffix)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<ICaseScoreDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<CaseScoreDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseScore, CaseScore>()
                .Build();
        
            var factory = new CaseScoreFactory();
            var sut = factory.Create();
            sut.ExamineeUserId = Create<int>();
        
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
            var expectedExamScoringId = Create<int>();
            
            var dto = Create<CaseScoreDto>();
            CaseScoreDto passedDto = null;
        
            var mockDal = new Mock<ICaseScoreDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScoringId))
                        .ReturnsAsync(dto);
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<CaseScoreDto>()))
                .Callback<CaseScoreDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseScore, CaseScore>()
                .Build();
        
            var factory = new CaseScoreFactory();
            var sut = await factory.GetByIdAsync(expectedExamScoringId);
            
            sut.ExamScoringId = dto.ExamScoringId;
            sut.ExamCaseId = dto.ExamCaseId;
            sut.ExaminerUserId = dto.ExaminerUserId;
            sut.ExamineeUserId = dto.ExamineeUserId;
            sut.ExamineeFirstName = dto.ExamineeFirstName;
            sut.ExamineeMiddleName = dto.ExamineeMiddleName;
            sut.ExamineeLastName = dto.ExamineeLastName;
            sut.ExamineeSuffix = dto.ExamineeSuffix;
            sut.Score = dto.Score;
            sut.CriticalFail = dto.CriticalFail;
            sut.Remarks = dto.Remarks;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.ExamScoringId = dto.ExamScoringId;
            sut.ExamCaseId = dto.ExamCaseId;
            sut.ExaminerUserId = dto.ExaminerUserId;
            sut.ExamineeUserId = dto.ExamineeUserId;
            sut.ExamineeFirstName = dto.ExamineeFirstName;
            sut.ExamineeMiddleName = dto.ExamineeMiddleName;
            sut.ExamineeLastName = dto.ExamineeLastName;
            sut.ExamineeSuffix = dto.ExamineeSuffix;
            sut.Score = dto.Score;
            sut.CriticalFail = dto.CriticalFail;
            sut.Remarks = dto.Remarks;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.CreatedAtUtc = dto.CreatedAtUtc;
            sut.LastUpdatedAtUtc = dto.LastUpdatedAtUtc;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.ExamScoringId)
                    .Excluding(m => m.ExaminerUserId)
                    .Excluding(m => m.ExamineeFirstName)
                    .Excluding(m => m.ExamineeMiddleName)
                    .Excluding(m => m.ExamineeLastName)
                    .Excluding(m => m.ExamineeSuffix)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            var expectedExamScoringId = Create<int>();
            
            var dto = Create<CaseScoreDto>();
        
            var mockDal = new Mock<ICaseScoreDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScoringId))
                        .ReturnsAsync(Create<CaseScoreDto>());
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<CaseScoreDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseScore, CaseScore>()
                .Build();
        
            var factory = new CaseScoreFactory();
            var sut = await factory.GetByIdAsync(expectedExamScoringId);
            sut.ExamineeUserId = Create<int>();
        
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