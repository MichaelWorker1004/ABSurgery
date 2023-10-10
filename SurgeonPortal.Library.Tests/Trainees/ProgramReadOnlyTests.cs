using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Trainees;
using SurgeonPortal.Library.Contracts.Trainees;
using SurgeonPortal.Library.Trainees;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Trainees
{
    [TestFixture] 
	public class ProgramReadOnlyTests : TestBase<int>
    {
        private ProgramReadOnlyDto CreateValidDto()
        {     
            var dto = Create<ProgramReadOnlyDto>();

            dto.ProgramName = Create<string>();
            dto.ProgramDirector = Create<string>();
            dto.ProgramNumber = Create<string>();
            dto.Exam = Create<string>();
            dto.ClinicalLevel = Create<string>();
            dto.City = Create<string>();
            dto.State = Create<string>();
    
            return dto;
        }

            

        #region GetByUserIdAsync ProgramReadOnly
        
        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            
            var mockDal = new Mock<IProgramReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(Create<ProgramReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IProgramReadOnly, ProgramReadOnly>()
                .Build();
        
            var factory = new ProgramReadOnlyFactory();
            var sut = await factory.GetByUserIdAsync();
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IProgramReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync())
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IProgramReadOnly, ProgramReadOnly>()
                .Build();
        
            var factory = new ProgramReadOnlyFactory();
            var sut = await factory.GetByUserIdAsync();
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
