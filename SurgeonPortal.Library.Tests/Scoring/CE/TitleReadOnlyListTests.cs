using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.DataAccess.Contracts.Scoring.CE;
using SurgeonPortal.Library.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring.CE;
using SurgeonPortal.Library.Scoring;
using SurgeonPortal.Library.Scoring.CE;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Scoring.CE
{
    [TestFixture] 
	public class TitleReadOnlyListTests : TestBase<int>
    {
        [Test]
        public async Task GetByIdAsync_CallsDalCorrectly()
        {
            var expectedExamScheduleId = Create<int>();
            
            var mockDal = new Mock<ITitleReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScheduleId))
                .ReturnsAsync(CreateMany<TitleReadOnlyDto>());

            var mockCaseDetailDal = new Mock<ICaseDetailReadOnlyDal>();
            mockCaseDetailDal.Setup(m => m.GetByCaseHeaderIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(CreateMany<CaseDetailReadOnlyDto>());

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockCaseDetailDal)
                .WithBusinessObject<ITitleReadOnlyList, TitleReadOnlyList>()
                .WithBusinessObject<ITitleReadOnly, TitleReadOnly>()
                .WithBusinessObject<ICaseDetailReadOnly, CaseDetailReadOnly>()
                .Build();
        
            var factory = new TitleReadOnlyListFactory();
            var sut = await factory.GetByIdAsync(expectedExamScheduleId);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<TitleReadOnlyDto>();
            var expectedExamScheduleId = Create<int>();
        
            var mockDal = new Mock<ITitleReadOnlyDal>();
            mockDal.Setup(m => m.GetByIdAsync(expectedExamScheduleId))
                .ReturnsAsync(expectedDtos);

            var mockCaseDetailDal = new Mock<ICaseDetailReadOnlyDal>();
            mockCaseDetailDal.Setup(m => m.GetByCaseHeaderIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(CreateMany<CaseDetailReadOnlyDto>());

            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithRegisteredInstance(mockCaseDetailDal)
                .WithBusinessObject<ITitleReadOnlyList, TitleReadOnlyList>()
                .WithBusinessObject<ITitleReadOnly, TitleReadOnly>()
                .WithBusinessObject<ICaseDetailReadOnly, CaseDetailReadOnly>()
                .Build();
        
            var factory = new TitleReadOnlyListFactory();
            var sut = await factory.GetByIdAsync(expectedExamScheduleId);
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
