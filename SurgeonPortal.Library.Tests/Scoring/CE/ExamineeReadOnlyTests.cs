using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring.CE;
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
            
            var mockDal = new Mock<IExamineeReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScheduleId))
                .ReturnsAsync(Create<ExamineeReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
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
        
            var mockDal = new Mock<IExamineeReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamineeReadOnly, ExamineeReadOnly>()
                .Build();
        
            var factory = new ExamineeReadOnlyFactory();
            var sut = await factory.GetByIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
