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
	public class CaseFeedbackTests : TestBase<int>
    {
        private CaseFeedbackDto CreateValidDto()
        {     
            var dto = Create<CaseFeedbackDto>();

            dto.Id = Create<int>();
            dto.UserId = Create<int>();
            dto.CaseHeaderId = Create<int>();
            dto.Feedback = Create<string>();
            dto.CreatedByUserId = Create<int>();
            dto.LastUpdatedByUserId = Create<int>();
    
            return dto;
        }

            

        #region DeleteAsync
        
        [Test]
        public async Task Delete_CallsDalCorrectly()
        {
            var expectedId = Create<int>();
            
            var dto = CreateValidDto();
            CaseFeedbackDto passedDto = null;
        
            var mockDal = new Mock<ICaseFeedbackDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                .ReturnsAsync(dto);
            mockDal.Setup(m => m.DeleteAsync(It.IsAny<CaseFeedbackDto>()))
                .Callback<CaseFeedbackDto>((p) => passedDto = p)
                .Returns(Task.CompletedTask);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseFeedback, CaseFeedback>()
                .Build();
        
            var factory = new CaseFeedbackFactory();
            var sut = await factory.GetByIdAsync(expectedId);
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

        #region GetByExaminerIdAsync CaseFeedback
        
        [Test]
        public async Task GetByExaminerIdAsync_CallsDalCorrectly()
        {
            var expectedCaseHeaderId = Create<int>();
            
            var mockDal = new Mock<ICaseFeedbackDal>();
            mockDal.Setup(m => m.GetByExaminerIdAsync(expectedCaseHeaderId))
                .ReturnsAsync(Create<CaseFeedbackDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseFeedback, CaseFeedback>()
                .Build();
        
            var factory = new CaseFeedbackFactory();
            var sut = await factory.GetByExaminerIdAsync(expectedCaseHeaderId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExaminerId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<ICaseFeedbackDal>();
            mockDal.Setup(m => m.GetByExaminerIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseFeedback, CaseFeedback>()
                .Build();
        
            var factory = new CaseFeedbackFactory();
            var sut = await factory.GetByExaminerIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region GetByIdAsync CaseFeedback
        
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedId = Create<int>();
            
            var mockDal = new Mock<ICaseFeedbackDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                .ReturnsAsync(Create<CaseFeedbackDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseFeedback, CaseFeedback>()
                .Build();
        
            var factory = new CaseFeedbackFactory();
            var sut = await factory.GetByIdAsync(expectedId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetById_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<ICaseFeedbackDal>();
            mockDal.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseFeedback, CaseFeedback>()
                .Build();
        
            var factory = new CaseFeedbackFactory();
            var sut = await factory.GetByIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion

        #region Create
        
        [Test]
        public async Task Create_CallsDalCorrectly()
        {
            var dto = CreateValidDto();
            CaseFeedbackDto passedDto = null;
        
            var mockDal = new Mock<ICaseFeedbackDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<CaseFeedbackDto>()))
                .Callback<CaseFeedbackDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseFeedback, CaseFeedback>()
                .Build();
        
            var factory = new CaseFeedbackFactory();
            var sut = factory.Create();
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.CaseHeaderId = dto.CaseHeaderId;
            sut.Feedback = dto.Feedback;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null);
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                .Excluding(m => m.CreatedAtUtc)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedAtUtc)
                .Excluding(m => m.LastUpdatedByUserId)
                .Excluding(m => m.Id)
                .Excluding(m => m.CreatedByUserId)
                .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Create_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<ICaseFeedbackDal>();
            mockDal.Setup(m => m.InsertAsync(It.IsAny<CaseFeedbackDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseFeedback, CaseFeedback>()
                .Build();
        
            var factory = new CaseFeedbackFactory();
            var sut = factory.Create();
            sut.UserId = Create<int>();
        
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
            var expectedId = Create<int>();
            
            var dto = Create<CaseFeedbackDto>();
            CaseFeedbackDto passedDto = null;
        
            var mockDal = new Mock<ICaseFeedbackDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                        .ReturnsAsync(dto);
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<CaseFeedbackDto>()))
                .Callback<CaseFeedbackDto>((p) => passedDto = p)
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseFeedback, CaseFeedback>()
                .Build();
        
            var factory = new CaseFeedbackFactory();
            var sut = await factory.GetByIdAsync(expectedId);
            
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.CaseHeaderId = dto.CaseHeaderId;
            sut.Feedback = dto.Feedback;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            // We now change all properties on the SUT to make it Dirty
            // or the SaveAsync() will not be called. :)
            dto = CreateValidDto();
        
            sut.Id = dto.Id;
            sut.UserId = dto.UserId;
            sut.CaseHeaderId = dto.CaseHeaderId;
            sut.Feedback = dto.Feedback;
            sut.CreatedByUserId = dto.CreatedByUserId;
            sut.LastUpdatedByUserId = dto.LastUpdatedByUserId;
        
            await sut.SaveAsync();
        
            Assert.That(passedDto, Is.Not.Null, "The passedDto is null, which can mean that the DataPortal method was not called.");
        
            dto.Should().BeEquivalentTo(passedDto,
                options => options
                    .Excluding(m => m.CreatedAtUtc)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedAtUtc)
                    .Excluding(m => m.LastUpdatedByUserId)
                    .Excluding(m => m.UserId)
                    .Excluding(m => m.CreatedByUserId)
                    .Excluding(m => m.LastUpdatedByUserId)
                .ExcludingMissingMembers());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task Update_YieldsCorrectResult()
        {
            var expectedId = Create<int>();
            
            var dto = Create<CaseFeedbackDto>();
        
            var mockDal = new Mock<ICaseFeedbackDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedId))
                        .ReturnsAsync(Create<CaseFeedbackDto>());
            mockDal.Setup(m => m.UpdateAsync(It.IsAny<CaseFeedbackDto>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseFeedback, CaseFeedback>()
                .Build();
        
            var factory = new CaseFeedbackFactory();
            var sut = await factory.GetByIdAsync(expectedId);
            sut.Id = Create<int>();
        
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
