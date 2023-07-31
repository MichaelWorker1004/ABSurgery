using FluentAssertions;
using Moq;
using NUnit.Framework;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.GraduateMedicalEducation;
using System;
using System.Threading.Tasks;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.GraduateMedicalEducation
{
    [TestFixture] 
	public class OverlapConflictCommandTests : TestBase<string>
    {
        private OverlapConflictCommandDto CreateValidDto()
        {     
            var dto = Create<OverlapConflictCommandDto>();

            dto.UserId = Create<int>();
            dto.StartDate = Create<System.DateTime>();
            dto.EndDate = Create<System.DateTime>();
            dto.OverlapConflict = Create<bool>();
            dto.RotationId = Create<int?>();
    
            return dto;
        }

        #region CheckOverlapConflicts
        
        [Test]
        public void CheckOverlapConflicts_CallsDalCorrectly()
        {
            var expectedUserId = Create<int>();
            var expectedStartDate = Create<DateTime>();
            var expectedEndDate = Create<DateTime>();
            var expectedRotationId = Create<int?>();
            
            var dto = Create<OverlapConflictCommandDto>();
        
            var mockDal = new Mock<IOverlapConflictCommandDal>();
            mockDal.Setup(m => m.CheckOverlapConflicts(
                expectedUserId,
                expectedStartDate,
                expectedEndDate,
                expectedRotationId))
                .Returns(dto);
        
            UseMockServiceProvider()
                
                .WithRegisteredInstance(mockDal)
                .WithBusinessObject<IOverlapConflictCommand, OverlapConflictCommand>()
                .Build();
        
            var factory = new OverlapConflictCommandFactory();
            var sut = factory.CheckOverlapConflicts(
                expectedUserId,
                expectedStartDate,
                expectedEndDate,
                expectedRotationId);
        
            mockDal.VerifyAll();
        
            Assert.That(dto, Is.Not.Null);
        	Assert.That(dto, Is.TypeOf<OverlapConflictCommandDto>());
        }
        
        #endregion
	}
}
