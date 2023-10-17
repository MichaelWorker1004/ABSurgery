using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.CE;
using SurgeonPortal.Library.CE;
using SurgeonPortal.Library.Contracts.CE;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.CE
{
    [TestFixture] 
	public class GetExamCasesScoredCommandTests : TestBase<int>
    {
        #region GetExamCasesScored
        
        [Test]
        public void GetExamCasesScored_CallsDalCorrectly()
        {
            var expectedExamScheduleId = Create<int>();
            var expectedExaminerUserId = 1234;
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IGetExamCasesScoredCommandDal>();
            mockDal.Setup(m => m.GetExamCasesScored(
                expectedExamScheduleId,
                expectedExaminerUserId))
                .Returns(dto);
            
        
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IGetExamCasesScoredCommand, GetExamCasesScoredCommand>()
                .Build();
        
            var factory = new GetExamCasesScoredCommandFactory();
            var sut = factory.GetExamCasesScored(expectedExamScheduleId);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<GetExamCasesScoredCommandDto>());
        }
        
        #endregion
	}
}
