using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Documents;
using SurgeonPortal.Library.Contracts.Documents;
using SurgeonPortal.Library.Documents;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Documents
{
    [TestFixture] 
	public class DocumentReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IDocumentReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(CreateMany<DocumentReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocumentReadOnlyList, DocumentReadOnlyList>()
                .WithBusinessObject<IDocumentReadOnly, DocumentReadOnly>()
                .Build();
        
            var factory = new DocumentReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<DocumentReadOnlyDto>();
        
            var mockDal = new Mock<IDocumentReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IDocumentReadOnlyList, DocumentReadOnlyList>()
                .WithBusinessObject<IDocumentReadOnly, DocumentReadOnly>()
                .Build();
        
            var factory = new DocumentReadOnlyListFactory();
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
