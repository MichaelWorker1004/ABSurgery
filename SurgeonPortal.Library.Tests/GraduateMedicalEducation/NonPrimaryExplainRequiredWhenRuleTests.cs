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
	internal class TestableNonPrimaryExplainRequiredWhenRule : NonPrimaryExplainRequiredWhenRule
	{
		public TestableNonPrimaryExplainRequiredWhenRule(IPropertyInfo primaryProperty, int priority) : base(primaryProperty, priority)
		{
		}

		public void TestableExecute(IRuleContext context)
		{
			Execute(context);
		}
	}

	public class NonPrimaryExplainRequiredWhenRuleTests : TestBase<int>
	{
		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel4Chief)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5Chief)]
		public void Execute_ShouldAddError_WhenClinicalLevelMatchNotEssentialAndNoExplain(int clinicalLevelId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.IsEssential).Returns(false);
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(r => r.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(r => r.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, null }
			});

			var sut = new TestableNonPrimaryExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Once);
		}

		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel4Chief)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5Chief)]
		public void Execute_ShouldAddError_WhenClinicalLevelMatchNotEssentialAndExplain(int clinicalLevelId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.IsEssential).Returns(false);
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(r => r.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(r => r.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, "test value" }
			});

			var sut = new TestableNonPrimaryExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel4Chief)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5Chief)]
		public void Execute_ShouldAddError_WhenClinicalLevelMatchAndEssentialAndNoExplain(int clinicalLevelId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.IsEssential).Returns(true);
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(r => r.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(r => r.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, null }
			});

			var sut = new TestableNonPrimaryExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel1)]
		[TestCase((int)ClinicalLevels.ClinicalLevel2)]
		[TestCase((int)ClinicalLevels.ClinicalLevel3)]
		[TestCase((int)ClinicalLevels.ClinicalLevel4)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5)]
		[TestCase((int)ClinicalLevels.OtherClinicalFellowship)]
		[TestCase((int)ClinicalLevels.Research)]
		public void Execute_ShouldAddError_WhenClinicalLevelNotMatchNotEssentialAndNoExplain(int clinicalLevelId)
		{
			var primaryProperty = new PropertyInfo<string>("explain");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.IsEssential).Returns(false);
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(r => r.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(r => r.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, null }
			});

			var sut = new TestableNonPrimaryExplainRequiredWhenRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}
	}
}
