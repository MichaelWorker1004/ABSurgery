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
	internal class TestableExplainWhenRequiredRule : ExplainRequiredWhenRule
	{
		public TestableExplainWhenRequiredRule(IPropertyInfo primaryProperty, int priority) : base(primaryProperty, priority)
		{
		}

		public void TestableExecute(IRuleContext context)
		{
			Execute(context);
		}
	}

	public class ExplainWhenRequiredRuleTests : TestBase<int>
	{
		[Test]
		public void Execute_ShoulAddError_WhenClinicalLevelOtherAndNoExplain()
		{
			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns((int)ClinicalLevels.OtherClinicalFellowship);

			var primaryProperty = new PropertyInfo<string>("explain");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{primaryProperty, null }
			});

			var sut = new TestableExplainWhenRequiredRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void Execute_ShoulNotAddError_WhenClinicalLevelOtherAndExplainPresent()
		{
			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns((int)ClinicalLevels.OtherClinicalFellowship);

			var primaryProperty = new PropertyInfo<string>("explain");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{primaryProperty, "text value" }
			});

			var sut = new TestableExplainWhenRequiredRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		[Test]
		[TestCase((int)ClinicalLevels.ClinicalLevel1)]
		[TestCase((int)ClinicalLevels.ClinicalLevel2)]
		[TestCase((int)ClinicalLevels.ClinicalLevel3)]
		[TestCase((int)ClinicalLevels.ClinicalLevel4)]
		[TestCase((int)ClinicalLevels.ClinicalLevel4Chief)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5)]
		[TestCase((int)ClinicalLevels.ClinicalLevel5Chief)]
		[TestCase((int)ClinicalLevels.Research)]
		public void Execute_ShoulNotAddError_WhenClinicalLevelNotOther(int clinicalLevelId)
		{
			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.ClinicalLevelId).Returns(clinicalLevelId);

			var primaryProperty = new PropertyInfo<string>("explain");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{primaryProperty, null }
			});

			var sut = new TestableExplainWhenRequiredRule(primaryProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}
	}
}
