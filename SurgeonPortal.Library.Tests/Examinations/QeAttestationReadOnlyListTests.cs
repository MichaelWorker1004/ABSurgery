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
	public class QeAttestationReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByExamIdAsync_CallsDalCorrectly()
        {
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IQeAttestationReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(CreateMany<QeAttestationReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IQeAttestationReadOnlyList, QeAttestationReadOnlyList>()
                .WithBusinessObject<IQeAttestationReadOnly, QeAttestationReadOnly>()
                .Build();
        
            var factory = new QeAttestationReadOnlyListFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<QeAttestationReadOnlyDto>();
            var expectedExamId = Create<int>();
            var expectedUserId = 1234;
            
        
            var mockDal = new Mock<IQeAttestationReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserId,
                expectedExamId))
                .ReturnsAsync(expectedDtos);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IQeAttestationReadOnlyList, QeAttestationReadOnlyList>()
                .WithBusinessObject<IQeAttestationReadOnly, QeAttestationReadOnly>()
                .Build();
        
            var factory = new QeAttestationReadOnlyListFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
