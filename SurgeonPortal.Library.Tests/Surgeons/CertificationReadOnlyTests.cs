using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Surgeons;
using SurgeonPortal.Library.Contracts.Surgeons;
using SurgeonPortal.Library.Surgeons;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Surgeons
{
    [TestFixture] 
	public class CertificationReadOnlyTests : TestBase<string>
    {
        private CertificationReadOnlyDto CreateValidDto()
        {     
            var dto = Create<CertificationReadOnlyDto>();

            dto.Speciality = Create<string>();
            dto.CertificateId = Create<string>();
            dto.InitialCertificationDate = Create<string>();
            dto.EndDateDisplay = Create<string>();
    
            return dto;
        }

            

        #region GetByAbsIdAsync CertificationReadOnly
        
        [Test]
        public async Task GetByAbsIdAsync_CallsDalCorrectly()
        {
            var expectedAbsId = Create<string>();
            
            var mockDal = new Mock<ICertificationReadOnlyDal>();
            mockDal.Setup(m => m.GetByAbsIdAsync(expectedAbsId))
                .ReturnsAsync(Create<CertificationReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICertificationReadOnly, CertificationReadOnly>()
                .Build();
        
            var factory = new CertificationReadOnlyFactory();
            var sut = await factory.GetByAbsIdAsync(expectedAbsId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByAbsId_YieldsCorrectResult()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<ICertificationReadOnlyDal>();
            mockDal.Setup(m => m.GetByAbsIdAsync(It.IsAny<string>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity()
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<ICertificationReadOnly, CertificationReadOnly>()
                .Build();
        
            var factory = new CertificationReadOnlyFactory();
            var sut = await factory.GetByAbsIdAsync(Create<string>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
