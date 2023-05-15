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
	public class ProgramReadOnlyTests : TestBase<string>
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
            var expectedUserId = Create<int>();
            
            var mockDal = new Mock<IProgramReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedUserId))
                .ReturnsAsync(Create<ProgramReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IProgramReadOnly, ProgramReadOnly>()
                .Build();
        
            var factory = new ProgramReadOnlyFactory();
            var sut = await factory.GetByUserIdAsync(expectedUserId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IProgramReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IProgramReadOnly, ProgramReadOnly>()
                .Build();
        
            var factory = new ProgramReadOnlyFactory();
            var sut = await factory.GetByUserIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
