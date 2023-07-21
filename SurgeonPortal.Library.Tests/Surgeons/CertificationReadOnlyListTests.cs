using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Surgeons;
using SurgeonPortal.Library.Contracts.Surgeons;
using SurgeonPortal.Library.Surgeons;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Surgeons
{
    [TestFixture] 
	public class CertificationReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<ICertificationReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(CreateMany<CertificationReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICertificationReadOnlyList, CertificationReadOnlyList>()
                .WithBusinessObject<ICertificationReadOnly, CertificationReadOnly>()
                .Build();
        
            var factory = new CertificationReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<CertificationReadOnlyDto>();
        
            var mockDal = new Mock<ICertificationReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICertificationReadOnlyList, CertificationReadOnlyList>()
                .WithBusinessObject<ICertificationReadOnly, CertificationReadOnly>()
                .Build();
        
            var factory = new CertificationReadOnlyListFactory();
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
