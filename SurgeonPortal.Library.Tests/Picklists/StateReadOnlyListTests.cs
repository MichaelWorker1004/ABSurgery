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
	public class StateReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetByCountryAsync_CallsDalCorrectly()
        {
            var expectedCountryCode = Create<string>();
            
            var mockDal = new Mock<IStateReadOnlyDal>();
            mockDal.Setup(m => m.GetByCountryAsync(expectedCountryCode))
                .ReturnsAsync(CreateMany<StateReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IStateReadOnlyList, StateReadOnlyList>()
                .WithBusinessObject<IStateReadOnly, StateReadOnly>()
                .Build();
        
            var factory = new StateReadOnlyListFactory();
            var sut = await factory.GetByCountryAsync(expectedCountryCode);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByCountryAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<StateReadOnlyDto>();
        
            var mockDal = new Mock<IStateReadOnlyDal>();
            mockDal.Setup(m => m.GetByCountryAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IStateReadOnlyList, StateReadOnlyList>()
                .WithBusinessObject<IStateReadOnly, StateReadOnly>()
                .Build();
        
            var factory = new StateReadOnlyListFactory();
            var sut = await factory.GetByCountryAsync(Create<string>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
