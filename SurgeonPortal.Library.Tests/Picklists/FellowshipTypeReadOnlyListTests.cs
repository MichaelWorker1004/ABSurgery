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
	public class FellowshipTypeReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IFellowshipTypeReadOnlyDal>();
            mockDal.Setup(m => m.GetAsync())
                .ReturnsAsync(CreateMany<FellowshipTypeReadOnlyDto>());
        
                
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IFellowshipTypeReadOnlyList, FellowshipTypeReadOnlyList>()
                .WithBusinessObject<IFellowshipTypeReadOnly, FellowshipTypeReadOnly>()
                .Build();
        
            var factory = new FellowshipTypeReadOnlyListFactory();
            var sut = await factory.GetAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<FellowshipTypeReadOnlyDto>();
        
        
            var mockDal = new Mock<IFellowshipTypeReadOnlyDal>();
            mockDal.Setup(m => m.GetAsync())
                .ReturnsAsync(expectedDtos);
        
                
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IFellowshipTypeReadOnlyList, FellowshipTypeReadOnlyList>()
                .WithBusinessObject<IFellowshipTypeReadOnly, FellowshipTypeReadOnly>()
                .Build();
        
            var factory = new FellowshipTypeReadOnlyListFactory();
            var sut = await factory.GetAsync();
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
