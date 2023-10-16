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
	public class CaseFeedbackReadOnlyTests : TestBase<string>
    {

        #region GetByExaminerIdAsync
        
        [Test]
        public async Task GetByExaminerIdAsync_CallsDalCorrectly()
        {
            var expectedCaseHeaderId = Create<int>();
            
            var mockDal = new Mock<ICaseFeedbackReadOnlyDal>();
            mockDal.Setup(m => m.GetByExaminerIdAsync(expectedCaseHeaderId))
                .ReturnsAsync(Create<CaseFeedbackReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseFeedbackReadOnly, CaseFeedbackReadOnly>()
                .Build();
        
            var factory = new CaseFeedbackReadOnlyFactory();
            var sut = await factory.GetByExaminerIdAsync(expectedCaseHeaderId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExaminerIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<CaseFeedbackReadOnlyDto>();
        
            var mockDal = new Mock<ICaseFeedbackReadOnlyDal>();
            mockDal.Setup(m => m.GetByExaminerIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICaseFeedbackReadOnly, CaseFeedbackReadOnly>()
                .Build();
        
            var factory = new CaseFeedbackReadOnlyFactory();
            var sut = await factory.GetByExaminerIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
