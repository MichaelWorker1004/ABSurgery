using Csla;
using Csla.Core;
using Csla.Rules;
using Moq;
using NUnit.Framework;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.GraduateMedicalEducation;
using SurgeonPortal.Shared;
using System.Collections.Generic;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.GraduateMedicalEducation
{
	internal class TestableNonClinicalExplainRequiredWhenRule : NonClinicalExplainRequiredWhenRule
	{
		public TestableNonClinicalExplainRequiredWhenRule(IPropertyInfo primaryProperty, int priority) : base(primaryProperty, priority)
		{
		}

		public void TestableExecute(IRuleContext context)
		{
			Execute(context);
		}
	}

	public class NonClinicalExplainRequiredWhenRuleTests : TestBase<int>
	{
		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel4, (int)ClinicalActivities.NonClinicalResearch)]
		[TestCase((int)ClinicalLevels.ClinicalLevel4, (int)ClinicalActivities.ClinicalNonSurgical)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5, (int)ClinicalActivities.NonClinicalResearch)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5, (int)ClinicalActivities.ClinicalNonSurgical)]
		public void Execute_ShouldAddError_WhenClinicalLevelAndActivityMatchWithNoExplain(int clinicalLevelId, int clinicalActivityId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);
			mockRotation.SetupGet(r => r.ClinicalActivityId).Returns(clinicalActivityId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, null }
			});

			var sut = new TestableNonClinicalExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Once);
		}

		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel4, (int)ClinicalActivities.NonClinicalResearch)]
		[TestCase((int)ClinicalLevels.ClinicalLevel4, (int)ClinicalActivities.ClinicalNonSurgical)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5, (int)ClinicalActivities.NonClinicalResearch)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5, (int)ClinicalActivities.ClinicalNonSurgical)]
		public void Execute_ShouldNotAddError_WhenClinicalLevelAndActivityMatchWithExplain(int clinicalLevelId, int clinicalActivityId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);
			mockRotation.SetupGet(r => r.ClinicalActivityId).Returns(clinicalActivityId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, "test value" }
			});

			var sut = new TestableNonClinicalExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel4, (int)ClinicalActivities.Anesthesia)]
		[TestCase((int)ClinicalLevels.ClinicalLevel4, (int)ClinicalActivities.BreastDisease)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5, (int)ClinicalActivities.Burn)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5, (int)ClinicalActivities.Ent)]
		public void Execute_ShouldNotAddError_WhenClinicalActivityNotMatchWithNoExplain(int clinicalLevelId, int clinicalActivityId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);
			mockRotation.SetupGet(r => r.ClinicalActivityId).Returns(clinicalActivityId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, null }
			});

			var sut = new TestableNonClinicalExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel1, (int)ClinicalActivities.NonClinicalResearch)]
		[TestCase((int)ClinicalLevels.ClinicalLevel2, (int)ClinicalActivities.ClinicalNonSurgical)]
		[TestCase((int)ClinicalLevels.ClinicalLevel3, (int)ClinicalActivities.NonClinicalResearch)]
		[TestCase((int)ClinicalLevels.ClinicalLevel4Chief, (int)ClinicalActivities.ClinicalNonSurgical)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5Chief, (int)ClinicalActivities.ClinicalNonSurgical)]
		public void Execute_ShouldNotAddError_WhenClinicalLevelNotMatchWithNoExplain(int clinicalLevelId, int clinicalActivityId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);
			mockRotation.SetupGet(r => r.ClinicalActivityId).Returns(clinicalActivityId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, null }
			});

			var sut = new TestableNonClinicalExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}
	}
}
