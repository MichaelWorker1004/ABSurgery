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
	public class PrimaryPracticeReadOnlyListTests : TestBase<int>
    {

        [Test]
        public async Task GetAllAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IPrimaryPracticeReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync())
                .ReturnsAsync(CreateMany<PrimaryPracticeReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IPrimaryPracticeReadOnlyList, PrimaryPracticeReadOnlyList>()
                .WithBusinessObject<IPrimaryPracticeReadOnly, PrimaryPracticeReadOnly>()
                .Build();
        
            var factory = new PrimaryPracticeReadOnlyListFactory();
            var sut = await factory.GetAllAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAllAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<PrimaryPracticeReadOnlyDto>();
        
            var mockDal = new Mock<IPrimaryPracticeReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IPrimaryPracticeReadOnlyList, PrimaryPracticeReadOnlyList>()
                .WithBusinessObject<IPrimaryPracticeReadOnly, PrimaryPracticeReadOnly>()
                .Build();
        
            var factory = new PrimaryPracticeReadOnlyListFactory();
            var sut = await factory.GetAllAsync();
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
