using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations.GQ;
using SurgeonPortal.Library.Contracts.Examinations.GQ;
using SurgeonPortal.Library.Examinations.GQ;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examinations.GQ
{
    [TestFixture] 
	public class AdditionalTrainingReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetAllByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = Create<int>();
            
            var mockDal = new Mock<IAdditionalTrainingReadOnlyDal>();
            mockDal.Setup(m => m.GetAllByUserIdAsync(expectedUserId))
                .ReturnsAsync(CreateMany<AdditionalTrainingReadOnlyDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdditionalTrainingReadOnlyList, AdditionalTrainingReadOnlyList>()
                .WithBusinessObject<IAdditionalTrainingReadOnly, AdditionalTrainingReadOnly>()
                .Build();
        
            var factory = new AdditionalTrainingReadOnlyListFactory();
            var sut = await factory.GetAllByUserIdAsync(expectedUserId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetAllByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<AdditionalTrainingReadOnlyDto>();
        
            var mockDal = new Mock<IAdditionalTrainingReadOnlyDal>();
            mockDal.Setup(m => m.GetAllByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdditionalTrainingReadOnlyList, AdditionalTrainingReadOnlyList>()
                .WithBusinessObject<IAdditionalTrainingReadOnly, AdditionalTrainingReadOnly>()
                .Build();
        
            var factory = new AdditionalTrainingReadOnlyListFactory();
            var sut = await factory.GetAllByUserIdAsync(Create<int>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}