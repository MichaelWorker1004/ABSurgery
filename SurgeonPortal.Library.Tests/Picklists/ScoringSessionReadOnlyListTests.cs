using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using SurgeonPortal.Library.Picklists;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Picklists
{
    [TestFixture] 
	public class ScoringSessionReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetByKeysAsync_CallsDalCorrectly()
        {
            var expectedExamHeaderId = Create<int>();
            
            var mockDal = new Mock<IScoringSessionReadOnlyDal>();
            mockDal.Setup(m => m.GetByKeysAsync(expectedExamHeaderId))
                .ReturnsAsync(CreateMany<ScoringSessionReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IScoringSessionReadOnlyList, ScoringSessionReadOnlyList>()
                .WithBusinessObject<IScoringSessionReadOnly, ScoringSessionReadOnly>()
                .Build();
        
            var factory = new ScoringSessionReadOnlyListFactory();
            var sut = await factory.GetByKeysAsync(expectedExamHeaderId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByKeysAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<ScoringSessionReadOnlyDto>();
        
            var mockDal = new Mock<IScoringSessionReadOnlyDal>();
            mockDal.Setup(m => m.GetByKeysAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IScoringSessionReadOnlyList, ScoringSessionReadOnlyList>()
                .WithBusinessObject<IScoringSessionReadOnly, ScoringSessionReadOnly>()
                .Build();
        
            var factory = new ScoringSessionReadOnlyListFactory();
            var sut = await factory.GetByKeysAsync(Create<int>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
