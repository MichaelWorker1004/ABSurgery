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
	public class StateReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByCountryAsync_CallsDalCorrectly()
        {
            var expectedCountryCode = Create<string>();
            
            var mockDal = new Mock<IStateReadOnlyDal>();
            mockDal.Setup(m => m.GetByCountryAsync(expectedCountryCode))
                .ReturnsAsync(CreateMany<StateReadOnlyDto>());
        
                
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
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
            var expectedCountryCode = Create<string>();
            
        
            var mockDal = new Mock<IStateReadOnlyDal>();
            mockDal.Setup(m => m.GetByCountryAsync(expectedCountryCode))
                .ReturnsAsync(expectedDtos);
        
                
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IStateReadOnlyList, StateReadOnlyList>()
                .WithBusinessObject<IStateReadOnly, StateReadOnly>()
                .Build();
        
            var factory = new StateReadOnlyListFactory();
            var sut = await factory.GetByCountryAsync(expectedCountryCode);
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
