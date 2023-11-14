using Csla;
using Csla.Core;
using Csla.Rules;
using Moq;
using NUnit.Framework;
using SurgeonPortal.Library.Users;
using System.Collections.Generic;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Users
{
	internal class TestableEitherOrRequiredRule : EitherOrRequiredRule
	{
		public TestableEitherOrRequiredRule(IPropertyInfo primaryProperty, IPropertyInfo secondaryProperty, int priority) : base(primaryProperty, secondaryProperty, priority)
		{
		}

		public void TestableExecute(IRuleContext context)
		{
			Execute(context);
		}
	}

	public class EitherOrRequiredRuleTests : TestBase<int>
	{
		[Test]
		public void EitherOrRequiredRule_ReturnsNoError_WhenRulePasses_WithBothProperties()
		{
			var primaryProperty = new PropertyInfo<int>("named", "friendNamed");
			var secondaryProperty = new PropertyInfo<string>("secondary", "friendlySecondary");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{primaryProperty, 10 },
				{secondaryProperty, "abc" }
			});

			var rule = new TestableEitherOrRequiredRule(primaryProperty, secondaryProperty, 1);

			rule.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void EitherOrRequiredRule_ReturnsNoError_WhenRulePasses_WithOneProperties()
		{
			var primaryProperty = new PropertyInfo<int>("named", "friendNamed");
			var secondaryProperty = new PropertyInfo<string>("secondary", "friendlySecondary");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{primaryProperty, 10 },
				{secondaryProperty, null }
			});

			var rule = new TestableEitherOrRequiredRule(primaryProperty, secondaryProperty, 1);

			rule.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void EitherOrRequiredRule_AddsCorrectError_WhenRuleFails()
		{
			var primaryProperty = new PropertyInfo<int>("named", "friendNamed");
			var secondaryProperty = new PropertyInfo<string>("secondary", "friendlySecondary");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{primaryProperty, null },
				{secondaryProperty, null }
			});

			var rule = new TestableEitherOrRequiredRule(primaryProperty, secondaryProperty, 1);

			rule.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(It.Is<string>(s => s.Contains(primaryProperty.FriendlyName) && s.Contains(secondaryProperty.FriendlyName))), Times.Once);
		}
	}
}
