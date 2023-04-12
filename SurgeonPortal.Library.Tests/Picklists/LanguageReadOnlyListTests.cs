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
	public class LanguageReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetAllAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<ILanguageReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync())
                .ReturnsAsync(CreateMany<LanguageReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ILanguageReadOnlyList, LanguageReadOnlyList>()
                .WithBusinessObject<ILanguageReadOnly, LanguageReadOnly>()
                .Build();
        
            var factory = new LanguageReadOnlyListFactory();
            var sut = await factory.GetAllAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAllAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<LanguageReadOnlyDto>();
        
            var mockDal = new Mock<ILanguageReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ILanguageReadOnlyList, LanguageReadOnlyList>()
                .WithBusinessObject<ILanguageReadOnly, LanguageReadOnly>()
                .Build();
        
            var factory = new LanguageReadOnlyListFactory();
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
