using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring.CE;
using SurgeonPortal.Library.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring.CE;
using SurgeonPortal.Library.Scoring.CE;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Scoring.CE
{
    [TestFixture] 
	public class ExamineeReadOnlyTests : TestBase<int>
    {
        #region GetByIdAsync
        
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedExamScheduleId = Create<int>();
            var expectedExaminerUserId = 1234;
            var expectedExamineeUserId = Create<int>();
            
            var mockDal = new Mock<IExamineeReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScheduleId))
                .ReturnsAsync(Create<ExamineeReadOnlyDto>());
            
            var titleDto = CreateMany<TitleReadOnlyDto>();
            var mockTitleDal = new Mock<ITitleReadOnlyDal>();
            mockTitleDal.Setup(m => m.GetByIdAsync(expectedExamScheduleId, expectedExaminerUserId, expectedExamineeUserId))
                .ReturnsAsync(titleDto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockTitleDal)
                .WithBusinessObject<IExamineeReadOnly, ExamineeReadOnly>()
                .Build();
        
            var factory = new ExamineeReadOnlyFactory();
            var sut = await factory.GetByIdAsync(expectedExamScheduleId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<ExamineeReadOnlyDto>();
            var expectedExamScheduleId = Create<int>();
			var expectedExaminerUserId = 1234;
			var expectedExamineeUserId = Create<int>();
            
            var mockDal = new Mock<IExamineeReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScheduleId))
                .ReturnsAsync(dto);
            
            var titleDto = CreateMany<TitleReadOnlyDto>();
            var mockTitleDal = new Mock<ITitleReadOnlyDal>();
            mockTitleDal.Setup(m => m.GetByIdAsync(expectedExamScheduleId, expectedExaminerUserId, expectedExamineeUserId))
                .ReturnsAsync(titleDto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockTitleDal)
                .WithBusinessObject<IExamineeReadOnly, ExamineeReadOnly>()
                .Build();
        
            var factory = new ExamineeReadOnlyFactory();
            var sut = await factory.GetByIdAsync(expectedExamScheduleId);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
