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
	public class ExamScoreReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IExamScoreReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync())
                .ReturnsAsync(CreateMany<ExamScoreReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamScoreReadOnlyList, ExamScoreReadOnlyList>()
                .WithBusinessObject<IExamScoreReadOnly, ExamScoreReadOnly>()
                .Build();
        
            var factory = new ExamScoreReadOnlyListFactory();
            var sut = await factory.GetByIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<ExamScoreReadOnlyDto>();
        
            var mockDal = new Mock<IExamScoreReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamScoreReadOnlyList, ExamScoreReadOnlyList>()
                .WithBusinessObject<IExamScoreReadOnly, ExamScoreReadOnly>()
                .Build();
        
            var factory = new ExamScoreReadOnlyListFactory();
            var sut = await factory.GetByIdAsync();
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
