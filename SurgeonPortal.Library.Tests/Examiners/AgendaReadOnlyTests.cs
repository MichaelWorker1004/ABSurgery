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
	public class AgendaReadOnlyTests : TestBase<int>
    {

        #region GetByExamHeaderIdAsync
        
        [Test]
        public async Task GetByExamHeaderIdAsync_CallsDalCorrectly()
        {
            var expectedExamHeaderId = Create<int>();
            
            var mockDal = new Mock<IAgendaReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamHeaderIdAsync(expectedExamHeaderId))
                .ReturnsAsync(Create<AgendaReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAgendaReadOnly, AgendaReadOnly>()
                .Build();
        
            var factory = new AgendaReadOnlyFactory();
            var sut = await factory.GetByExamHeaderIdAsync(expectedExamHeaderId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByExamHeaderIdAsync_LoadsSelfCorrectly()
        {
            var dto = Create<AgendaReadOnlyDto>();
        
            var mockDal = new Mock<IAgendaReadOnlyDal>();
            mockDal.Setup(m => m.GetByExamHeaderIdAsync(It.IsAny<int>()))
                .ReturnsAsync(dto);
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IAgendaReadOnly, AgendaReadOnly>()
                .Build();
        
            var factory = new AgendaReadOnlyFactory();
            var sut = await factory.GetByExamHeaderIdAsync(Create<int>());
        
            dto.Should().BeEquivalentTo(sut, options => options.ExcludingMissingMembers());
        }
        
        #endregion
	}
}
