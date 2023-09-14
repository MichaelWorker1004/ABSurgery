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
	public class ConflictReadOnlyTests : TestBase<string>
    {

        #region GetByExamHeaderIdAsync
        
        [Test]
        public async Task GetByExamHeaderIdAsync_CallsDalCorrectly()
        {
            var expectedExamHeaderId = Create<int>();
            
            var mockDal = new Mock<IConflictReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamHeaderIdAsync(expectedExamHeaderId))
                .ReturnsAsync(Create<ConflictReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IConflictReadOnly, ConflictReadOnly>()
                .Build();
        
            var factory = new ConflictReadOnlyFactory();
            var sut = await factory.GetByExamHeaderIdAsync(expectedExamHeaderId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamHeaderIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<ConflictReadOnlyDto>();
        
            var mockDal = new Mock<IConflictReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamHeaderIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IConflictReadOnly, ConflictReadOnly>()
                .Build();
        
            var factory = new ConflictReadOnlyFactory();
            var sut = await factory.GetByExamHeaderIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
