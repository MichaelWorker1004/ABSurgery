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
	public class RosterReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetByExaminationHeaderIdAsync_CallsDalCorrectly()
        {
            var expectedExamHeaderId = Create<int>();
            
            var mockDal = new Mock<IRosterReadOnlyDal>();
            mockDal.Setup(m => m.GetByExaminationHeaderIdAsync(expectedExamHeaderId))
                .ReturnsAsync(CreateMany<RosterReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRosterReadOnlyList, RosterReadOnlyList>()
                .WithBusinessObject<IRosterReadOnly, RosterReadOnly>()
                .Build();
        
            var factory = new RosterReadOnlyListFactory();
            var sut = await factory.GetByExaminationHeaderIdAsync(expectedExamHeaderId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExaminationHeaderIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<RosterReadOnlyDto>();
        
            var mockDal = new Mock<IRosterReadOnlyDal>();
            mockDal.Setup(m => m.GetByExaminationHeaderIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IRosterReadOnlyList, RosterReadOnlyList>()
                .WithBusinessObject<IRosterReadOnly, RosterReadOnly>()
                .Build();
        
            var factory = new RosterReadOnlyListFactory();
            var sut = await factory.GetByExaminationHeaderIdAsync(Create<int>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
