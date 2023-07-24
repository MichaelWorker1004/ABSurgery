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
	public class ExamStatusReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetAllAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IExamStatusReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync())
                .ReturnsAsync(CreateMany<ExamStatusReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamStatusReadOnlyList, ExamStatusReadOnlyList>()
                .WithBusinessObject<IExamStatusReadOnly, ExamStatusReadOnly>()
                .Build();
        
            var factory = new ExamStatusReadOnlyListFactory();
            var sut = await factory.GetAllAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAllAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<ExamStatusReadOnlyDto>();
        
            var mockDal = new Mock<IExamStatusReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamStatusReadOnlyList, ExamStatusReadOnlyList>()
                .WithBusinessObject<IExamStatusReadOnly, ExamStatusReadOnly>()
                .Build();
        
            var factory = new ExamStatusReadOnlyListFactory();
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
