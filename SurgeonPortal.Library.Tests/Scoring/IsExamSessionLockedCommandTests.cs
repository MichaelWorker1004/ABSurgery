using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using SurgeonPortal.Library.Scoring;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Scoring
{
    [TestFixture] 
	public class IsExamSessionLockedCommandTests : TestBase<int>
    {
        private IsExamSessionLockedCommandDto CreateValidDto()
        {     
            var dto = Create<IsExamSessionLockedCommandDto>();

            dto.ExamCaseId = Create<int>();
            dto.IsLocked = Create<bool?>();
    
            return dto;
        }

        #region IsExamSessionLocked
        
        [Test]
        public void IsExamSessionLocked_CallsDalCorrectly()
        {
            var expectedExamCaseId = Create<int>();
            
            var dto = CreateValidDto();
        
            var mockDal = new Mock<IIsExamSessionLockedCommandDal>();
            mockDal.Setup(m => m.IsExamSessionLocked(expectedExamCaseId))
                .Returns(dto);
        
                
            UseMockServiceProvider()
                .WithMockedIdentity(1234, "SomeUser")
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IIsExamSessionLockedCommand, IsExamSessionLockedCommand>()
                .Build();
        
            var factory = new IsExamSessionLockedCommandFactory();
            var sut = factory.IsExamSessionLocked(expectedExamCaseId);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<IsExamSessionLockedCommandDto>());
        }
        
        #endregion
	}
}
