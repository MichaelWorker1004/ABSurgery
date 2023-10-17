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
	public class FellowshipProgramReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetAllAsync_CallsDalCorrectly()
        {
            var expectedFellowshipType = Create<string>();
            
            var mockDal = new Mock<IFellowshipProgramReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync(expectedFellowshipType))
                .ReturnsAsync(CreateMany<FellowshipProgramReadOnlyDto>());
        
                
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IFellowshipProgramReadOnlyList, FellowshipProgramReadOnlyList>()
                .WithBusinessObject<IFellowshipProgramReadOnly, FellowshipProgramReadOnly>()
                .Build();
        
            var factory = new FellowshipProgramReadOnlyListFactory();
            var sut = await factory.GetAllAsync(expectedFellowshipType);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAllAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<FellowshipProgramReadOnlyDto>();
            var expectedFellowshipType = Create<string>();
            
        
            var mockDal = new Mock<IFellowshipProgramReadOnlyDal>();
            mockDal.Setup(m => m.GetAllAsync(expectedFellowshipType))
                .ReturnsAsync(expectedDtos);
        
                
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IFellowshipProgramReadOnlyList, FellowshipProgramReadOnlyList>()
                .WithBusinessObject<IFellowshipProgramReadOnly, FellowshipProgramReadOnly>()
                .Build();
        
            var factory = new FellowshipProgramReadOnlyListFactory();
            var sut = await factory.GetAllAsync(Create<string>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
