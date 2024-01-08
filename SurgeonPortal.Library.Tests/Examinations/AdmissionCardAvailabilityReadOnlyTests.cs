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
	public class AdmissionCardAvailabilityReadOnlyTests : TestBase<int>
    {
        #region GetByExamIdAsync
        
        [Test]
        public async Task GetByExamIdAsync_CallsDalCorrectly()
        {
            var expectedExamID = Create<int>();
            var expectedUserID = 1234;
            
            var mockDal = new Mock<IAdmissionCardAvailabilityReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserID,
                expectedExamID))
                .ReturnsAsync(Create<AdmissionCardAvailabilityReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdmissionCardAvailabilityReadOnly, AdmissionCardAvailabilityReadOnly>()
                .Build();
        
            var factory = new AdmissionCardAvailabilityReadOnlyFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamID);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<AdmissionCardAvailabilityReadOnlyDto>();
            var expectedExamID = Create<int>();
            var expectedUserID = 1234;
            
            var mockDal = new Mock<IAdmissionCardAvailabilityReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(
                expectedUserID,
                expectedExamID))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAdmissionCardAvailabilityReadOnly, AdmissionCardAvailabilityReadOnly>()
                .Build();
        
            var factory = new AdmissionCardAvailabilityReadOnlyFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamID);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
