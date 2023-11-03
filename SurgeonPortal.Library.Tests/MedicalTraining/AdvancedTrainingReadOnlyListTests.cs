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
	public class AdvancedTrainingReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IAdvancedTrainingReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(CreateMany<AdvancedTrainingReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTrainingReadOnlyList, AdvancedTrainingReadOnlyList>()
                .WithBusinessObject<IAdvancedTrainingReadOnly, AdvancedTrainingReadOnly>()
                .Build();
        
            var factory = new AdvancedTrainingReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<AdvancedTrainingReadOnlyDto>();
            var expectedUserId = 1234;
            
        
            var mockDal = new Mock<IAdvancedTrainingReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(expectedDtos);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdvancedTrainingReadOnlyList, AdvancedTrainingReadOnlyList>()
                .WithBusinessObject<IAdvancedTrainingReadOnly, AdvancedTrainingReadOnly>()
                .Build();
        
            var factory = new AdvancedTrainingReadOnlyListFactory();
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
