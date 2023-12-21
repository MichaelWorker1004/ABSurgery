using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Library.Examinations;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examinations
{
    [TestFixture] 
	public class QeExamEligibilityReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IQeExamEligibilityReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(CreateMany<QeExamEligibilityReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IQeExamEligibilityReadOnlyList, QeExamEligibilityReadOnlyList>()
                .WithBusinessObject<IQeExamEligibilityReadOnly, QeExamEligibilityReadOnly>()
                .Build();
        
            var factory = new QeExamEligibilityReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<QeExamEligibilityReadOnlyDto>();
            var expectedUserId = 1234;
            
        
            var mockDal = new Mock<IQeExamEligibilityReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(expectedDtos);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IQeExamEligibilityReadOnlyList, QeExamEligibilityReadOnlyList>()
                .WithBusinessObject<IQeExamEligibilityReadOnly, QeExamEligibilityReadOnly>()
                .Build();
        
            var factory = new QeExamEligibilityReadOnlyListFactory();
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
