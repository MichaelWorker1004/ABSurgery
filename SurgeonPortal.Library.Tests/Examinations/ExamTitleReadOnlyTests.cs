using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Library.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examinations
{
    [TestFixture] 
	public class ExamTitleReadOnlyTests : TestBase<string>
    {

        #region GetByExamIdAsync
        
        [Test]
        public async Task GetByExamIdAsync_CallsDalCorrectly()
        {
            var expectedExamId = Create<int>();
            
            var mockDal = new Mock<IExamTitleReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(expectedExamId))
                .ReturnsAsync(Create<ExamTitleReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamTitleReadOnly, ExamTitleReadOnly>()
                .Build();
        
            var factory = new ExamTitleReadOnlyFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<ExamTitleReadOnlyDto>();
        
            var mockDal = new Mock<IExamTitleReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamTitleReadOnly, ExamTitleReadOnly>()
                .Build();
        
            var factory = new ExamTitleReadOnlyFactory();
            var sut = await factory.GetByExamIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
