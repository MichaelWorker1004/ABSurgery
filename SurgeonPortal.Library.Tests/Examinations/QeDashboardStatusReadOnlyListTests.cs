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
	public class QeDashboardStatusReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByExamIdAsync_CallsDalCorrectly()
        {
            var expectedExamheaderId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IQeDashboardStatusReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamheaderId))
                .ReturnsAsync(CreateMany<QeDashboardStatusReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IQeDashboardStatusReadOnlyList, QeDashboardStatusReadOnlyList>()
                .WithBusinessObject<IQeDashboardStatusReadOnly, QeDashboardStatusReadOnly>()
                .Build();
        
            var factory = new QeDashboardStatusReadOnlyListFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamheaderId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<QeDashboardStatusReadOnlyDto>();
            var expectedExamheaderId = Create<int>();
            var expectedUserId = 1234;
            
        
            var mockDal = new Mock<IQeDashboardStatusReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamheaderId))
                .ReturnsAsync(expectedDtos);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IQeDashboardStatusReadOnlyList, QeDashboardStatusReadOnlyList>()
                .WithBusinessObject<IQeDashboardStatusReadOnly, QeDashboardStatusReadOnly>()
                .Build();
        
            var factory = new QeDashboardStatusReadOnlyListFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamheaderId);
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
