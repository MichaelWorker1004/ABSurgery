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
	public class ExamTitleReadOnlyTests : TestBase<int>
    {
        #region GetByExamIdAsync
        
        [Test]
        public async Task GetByExamIdAsync_CallsDalCorrectly()
        {
            var expectedExamId = Create<int>();
            
            var mockDal = new Mock<IExamTitleReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(expectedExamId))
                .ReturnsAsync(Create<ExamTitleReadOnlyDto>());
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamTitleReadOnly, ExamTitleReadOnly>()
                .Build();
        
            var factory = new ExamTitleReadOnlyFactory();
            var sut = await factory.GetByExamIdAsync(expectedExamId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamIdAsync_LoadsSelfCorrectly()
        {
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IExamTitleReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamIdAsync(expectedExamId))
                .ReturnsAsync(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamTitleReadOnly, ExamTitleReadOnly>()
                .Build();
        
            var factory = new ExamTitleReadOnlyFactory();
            var sut = await factory.GetByExamIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
