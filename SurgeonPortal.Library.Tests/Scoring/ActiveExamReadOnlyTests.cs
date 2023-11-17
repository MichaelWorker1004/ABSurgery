using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using SurgeonPortal.Library.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Scoring
{
    [TestFixture] 
	public class ActiveExamReadOnlyTests : TestBase<int>
    {
        #region GetByExaminerUserIdAsync
        
        [Test]
        public async Task GetByExaminerUserIdAsync_CallsDalCorrectly()
        {
            var expectedCurrentDate = Create<DateTime>();
            var expectedExaminerUserId = 1234;
            
            var mockDal = new Mock<IActiveExamReadOnlyDal>();
            mockDal.Setup(m => m.GetByExaminerUserIdAsync(
                expectedExaminerUserId,
                expectedCurrentDate))
                .ReturnsAsync(Create<ActiveExamReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IActiveExamReadOnly, ActiveExamReadOnly>()
                .Build();
        
            var factory = new ActiveExamReadOnlyFactory();
            var sut = await factory.GetByExaminerUserIdAsync(expectedCurrentDate);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExaminerUserIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<ActiveExamReadOnlyDto>();
            var expectedCurrentDate = Create<DateTime>();
            var expectedExaminerUserId = 1234;
            
            var mockDal = new Mock<IActiveExamReadOnlyDal>();
            mockDal.Setup(m => m.GetByExaminerUserIdAsync(
                expectedExaminerUserId,
                expectedCurrentDate))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IActiveExamReadOnly, ActiveExamReadOnly>()
                .Build();
        
            var factory = new ActiveExamReadOnlyFactory();
            var sut = await factory.GetByExaminerUserIdAsync(expectedCurrentDate);
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
