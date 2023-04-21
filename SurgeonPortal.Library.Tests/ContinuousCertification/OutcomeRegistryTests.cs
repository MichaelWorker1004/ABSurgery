using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.ContinuousCertification
{
    [TestFixture] 
	public class OutcomeRegistryTests : TestBase<string>
    {

        #region GetByUserIdAsync
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedUserId = Create<int>();
            
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<OutcomeRegistryDto>());
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = await factory.GetByUserIdAsync(expectedUserId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<OutcomeRegistryDto>();
        
            var mockDal = new Mock<IOutcomeRegistryDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOutcomeRegistry, OutcomeRegistry>()
                .Build();
        
            var factory = new OutcomeRegistryFactory();
            var sut = await factory.GetByUserIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
