using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.ProfessionalStanding
{
    [TestFixture] 
	public class GetClinicallyActiveCommandTests : TestBase<int>
    {
        private GetClinicallyActiveCommandDto CreateValidDto()
        {
            var dto = Create<GetClinicallyActiveCommandDto>();
        
            dto.UserId = 1234;
            dto.ClinicallyActive = Create<bool?>();
        
            return dto;
        }
        
        #region GetClinicallyActiveByUserId
        
        [Test]
        public void GetClinicallyActiveByUserId_CallsDalCorrectly()
        {
            var expectedUserId = 1234;
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IGetClinicallyActiveCommandDal>();
            mockDal.Setup(m => m.GetClinicallyActiveByUserId(expectedUserId))
                .Returns(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IGetClinicallyActiveCommand, GetClinicallyActiveCommand>()
                .Build();
        
            var factory = new GetClinicallyActiveCommandFactory();
            var sut = factory.GetClinicallyActiveByUserId();
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<GetClinicallyActiveCommandDto>());
        }
        
        #endregion
	}
}
