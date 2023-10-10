using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Library.MedicalTraining;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.MedicalTraining
{
    [TestFixture] 
	public class FellowshipReadOnlyListTests : TestBase<int>
    {

        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IFellowshipReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(CreateMany<FellowshipReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IFellowshipReadOnlyList, FellowshipReadOnlyList>()
                .WithBusinessObject<IFellowshipReadOnly, FellowshipReadOnly>()
                .Build();
        
            var factory = new FellowshipReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<FellowshipReadOnlyDto>();
        
            var mockDal = new Mock<IFellowshipReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IFellowshipReadOnlyList, FellowshipReadOnlyList>()
                .WithBusinessObject<IFellowshipReadOnly, FellowshipReadOnly>()
                .Build();
        
            var factory = new FellowshipReadOnlyListFactory();
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
