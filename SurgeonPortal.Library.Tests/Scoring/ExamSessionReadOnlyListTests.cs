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
	public class ExamSessionReadOnlyListTests : TestBase<string>
    {

        [Test]
        public async Task GetByUserIdAsync_CallsDalCorrectly()
        {
            var expectedExamDate = Create<DateTime>();
            
            var mockDal = new Mock<IExamSessionReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(expectedExamDate))
                .ReturnsAsync(CreateMany<ExamSessionReadOnlyDto>());
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamSessionReadOnlyList, ExamSessionReadOnlyList>()
                .WithBusinessObject<IExamSessionReadOnly, ExamSessionReadOnly>()
                .Build();
        
            var factory = new ExamSessionReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync(expectedExamDate);
        
            mockDal.VerifyAll();
        }
        
        [Test]
        public async Task GetByUserIdAsync_LoadsChildrenCorrectly()
        {
            var expectedDtos = CreateMany<ExamSessionReadOnlyDto>();
        
            var mockDal = new Mock<IExamSessionReadOnlyDal>();
            mockDal.Setup(m => m.GetByUserIdAsync(It.IsAny<DateTime>()))
                .ReturnsAsync(expectedDtos);
        
            UseMockServiceProvider()
                .WithUserInRoles(SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim)
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IExamSessionReadOnlyList, ExamSessionReadOnlyList>()
                .WithBusinessObject<IExamSessionReadOnly, ExamSessionReadOnly>()
                .Build();
        
            var factory = new ExamSessionReadOnlyListFactory();
            var sut = await factory.GetByUserIdAsync(Create<DateTime>());
        
            Assert.That(sut, Has.Count.EqualTo(3));
            expectedDtos.Should().BeEquivalentTo(sut, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
	}
}
