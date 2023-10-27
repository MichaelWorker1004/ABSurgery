using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Examiners;
using SurgeonPortal.Library.Contracts.Examiners;
using SurgeonPortal.Library.Examiners;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Examiners
{
    [TestFixture] 
	public class ConflictReadOnlyTests : TestBase<int>
    {
        #region GetByExamHeaderIdAsync
        
        [Test]
        public async Task GetByExamHeaderIdAsync_CallsDalCorrectly()
        {
            var expectedExamHeaderId = Create<int>();
            var expectedExaminerUserId = 1234;
            
            var mockDal = new Mock<IConflictReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamHeaderIdAsync(
                expectedExaminerUserId,
                expectedExamHeaderId))
                .ReturnsAsync(Create<ConflictReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IConflictReadOnly, ConflictReadOnly>()
                .Build();
        
            var factory = new ConflictReadOnlyFactory();
            var sut = await factory.GetByExamHeaderIdAsync(It.IsAny<int>());
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamHeaderIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<ConflictReadOnlyDto>();
            var expectedExamHeaderId = Create<int>();
            var expectedExaminerUserId = 1234;
            
            var mockDal = new Mock<IConflictReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamHeaderIdAsync(
                expectedExaminerUserId,
                expectedExamHeaderId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IConflictReadOnly, ConflictReadOnly>()
                .Build();
        
            var factory = new ConflictReadOnlyFactory();
            var sut = await factory.GetByExamHeaderIdAsync(It.IsAny<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
