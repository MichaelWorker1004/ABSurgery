using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Billing;
using SurgeonPortal.Library.Billing;
using SurgeonPortal.Library.Contracts.Billing;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Billing
{
    [TestFixture] 
	public class ExamFeeReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var mockDal = new Mock<IExamFeeReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(CreateMany<ExamFeeReadOnlyDto>());
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamFeeReadOnlyList, ExamFeeReadOnlyList>()
                .WithBusinessObject<IExamFeeReadOnly, ExamFeeReadOnly>()
                .Build();
        
            var factory = new ExamFeeReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<ExamFeeReadOnlyDto>();
            var expectedUserId = 1234;
        
        
            var mockDal = new Mock<IExamFeeReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(expectedDtos);
        
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamFeeReadOnlyList, ExamFeeReadOnlyList>()
                .WithBusinessObject<IExamFeeReadOnly, ExamFeeReadOnly>()
                .Build();
        
            var factory = new ExamFeeReadOnlyListFactory();
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
