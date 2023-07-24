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
	public class ExamHistoryReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IExamHistoryReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(CreateMany<ExamHistoryReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamHistoryReadOnlyList, ExamHistoryReadOnlyList>()
                .WithBusinessObject<IExamHistoryReadOnly, ExamHistoryReadOnly>()
                .Build();
        
            var factory = new ExamHistoryReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<ExamHistoryReadOnlyDto>();
        
            var mockDal = new Mock<IExamHistoryReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamHistoryReadOnlyList, ExamHistoryReadOnlyList>()
                .WithBusinessObject<IExamHistoryReadOnly, ExamHistoryReadOnly>()
                .Build();
        
            var factory = new ExamHistoryReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
