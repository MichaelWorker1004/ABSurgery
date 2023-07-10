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
	public class TitleReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedExamScheduleId = Create<int>();
            
            var mockDal = new Mock<ITitleReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScheduleId))
                .ReturnsAsync(CreateMany<TitleReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ITitleReadOnlyList, TitleReadOnlyList>()
                .WithBusinessObject<ITitleReadOnly, TitleReadOnly>()
                .Build();
        
            var factory = new TitleReadOnlyListFactory();
            var sut = await factory.GetByIdAsync(expectedExamScheduleId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<TitleReadOnlyDto>();
        
            var mockDal = new Mock<ITitleReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ITitleReadOnlyList, TitleReadOnlyList>()
                .WithBusinessObject<ITitleReadOnly, TitleReadOnly>()
                .Build();
        
            var factory = new TitleReadOnlyListFactory();
            var sut = await factory.GetByIdAsync(Create<int>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
