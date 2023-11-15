using Csla;
using Csla.Core;
using Csla.Rules;
using Moq;
using NUnit.Framework;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.GraduateMedicalEducation;
using SurgeonPortal.Shared;
using System;
using System.Collections.Generic;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.GraduateMedicalEducation
{
	internal class TestableFourMonthRotationExplainRequiredWhenRule : FourMonthRotationExplainRequiredWhenRule
	{
		public TestableFourMonthRotationExplainRequiredWhenRule(IPropertyInfo primaryProperty, int priority) : base(primaryProperty, priority)
		{
		}

		public void TestableExecute(IRuleContext context)
		{
			Execute(context);
		}
	}
	public class FourMonthRotationExplainRequiredWhenRuleTests : TestBase<int>
	{
		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel4Chief)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5Chief)]
		public void Execute_ShouldAddError_WhenClinicalLevelChiefWithLongDurationNoExplain(int clinicalLevelId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.StartDate).Returns(DateTime.Now);
			mockRotation.SetupGet(r => r.EndDate).Returns(DateTime.Now.AddDays(120));
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, null }
			});

			var sut = new TestableFourMonthRotationExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Once);
		}

		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel4Chief)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5Chief)]
		public void Execute_ShouldNotAddError_WhenClinicalLevelChiefWithLongDurationWithExplain(int clinicalLevelId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.StartDate).Returns(DateTime.Now);
			mockRotation.SetupGet(r => r.EndDate).Returns(DateTime.Now.AddDays(120));
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, "text value" }
			});

			var sut = new TestableFourMonthRotationExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel4Chief)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5Chief)]
		public void Execute_ShouldNotAddError_WhenClinicalLevelChiefWithShortDurationWithNoExplain(int clinicalLevelId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.StartDate).Returns(DateTime.Now);
			mockRotation.SetupGet(r => r.EndDate).Returns(DateTime.Now.AddDays(119));
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, null }
			});

			var sut = new TestableFourMonthRotationExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel1)]
		[TestCase((int)ClinicalLevels.ClinicalLevel2)]
		[TestCase((int)ClinicalLevels.ClinicalLevel3)]
		[TestCase((int)ClinicalLevels.ClinicalLevel4)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5)]
		[TestCase((int)ClinicalLevels.OtherClinicalFellowship)]
		[TestCase((int)ClinicalLevels.Research)]
		public void Execute_ShouldNotAddError_WhenClinicalLevelNotChiefWithLongDurationWithNoExplain(int clinicalLevelId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.StartDate).Returns(DateTime.Now);
			mockRotation.SetupGet(r => r.EndDate).Returns(DateTime.Now.AddDays(120));
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, null }
			});

			var sut = new TestableFourMonthRotationExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}
	}
}
