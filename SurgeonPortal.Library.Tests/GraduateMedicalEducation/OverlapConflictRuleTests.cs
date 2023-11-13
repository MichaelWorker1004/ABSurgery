using Csla;
using Csla.Core;
using Csla.Rules;
using Moq;
using NUnit.Framework;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.GraduateMedicalEducation;
using System;
using System.Collections.Generic;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.GraduateMedicalEducation
{
	internal class TestableOverlapConflictRule : OverlapConflictRule
	{
		public TestableOverlapConflictRule(IOverlapConflictCommandFactory overlapConflictCommandFactory, IPropertyInfo primaryProperty, IPropertyInfo secondaryProperty, IPropertyInfo userIdProperty, int priority) 
			: base(overlapConflictCommandFactory, primaryProperty, secondaryProperty, userIdProperty, priority)
		{
		}

		public void TestableExecute(IRuleContext context)
		{
			Execute(context);
		}
	}

	public class OverlapConflictRuleTests : TestBase<int>
	{
		[Test]
		public void Execute_CallCommandCorrectly()
		{
			var primaryProperty = new PropertyInfo<DateTime>("startDate");
			var secondaryProperty = new PropertyInfo<DateTime>("endDate");
			var userIdProperty = new PropertyInfo<DateTime>("userId");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.IsNew).Returns(false);
			mockRotation.SetupGet(r => r.Id).Returns(1234);

			var mockCommandFactory = GetCommandFactory(true);

			var startDate = DateTime.Now;
			var endDate = DateTime.Now.AddDays(1);
			var userId = 1;

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, startDate },
				{ secondaryProperty, endDate },
				{ userIdProperty, userId }
			});

			var sut = new TestableOverlapConflictRule(mockCommandFactory.Object, primaryProperty, secondaryProperty, userIdProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockCommandFactory.Verify(cf => cf.CheckOverlapConflicts(userId, startDate, endDate, 1234), Times.Once);
		}

		[Test]
		public void Execute_ShouldAddError_WhenOverlapIsPresent()
		{
			var primaryProperty = new PropertyInfo<DateTime>("startDate");
			var secondaryProperty = new PropertyInfo<DateTime>("endDate");
			var userIdProperty = new PropertyInfo<DateTime>("userId");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.IsNew).Returns(true);

			var mockCommandFactory = GetCommandFactory(true);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, DateTime.Now },
				{ secondaryProperty, DateTime.Now.AddDays(1) },
				{ userIdProperty, 1 }
			});

			var sut = new TestableOverlapConflictRule(mockCommandFactory.Object, primaryProperty, secondaryProperty, userIdProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void Execute_ShouldNotAddError_WhenOverlapIsNotPresent()
		{
			var primaryProperty = new PropertyInfo<DateTime>("startDate");
			var secondaryProperty = new PropertyInfo<DateTime>("endDate");
			var userIdProperty = new PropertyInfo<DateTime>("userId");

			var mockRotation = new Mock<IRotation>();
			mockRotation.SetupGet(r => r.IsNew).Returns(true);

			var mockCommandFactory = GetCommandFactory(false);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(c => c.Target).Returns(mockRotation.Object);
			mockContext.SetupGet(c => c.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
			{
				{ primaryProperty, DateTime.Now },
				{ secondaryProperty, DateTime.Now.AddDays(1) },
				{ userIdProperty, 1 }
			});

			var sut = new TestableOverlapConflictRule(mockCommandFactory.Object, primaryProperty, secondaryProperty, userIdProperty, 1);

			sut.TestableExecute(mockContext.Object);

			mockContext.Verify(c => c.AddErrorResult(primaryProperty, It.IsAny<string>()), Times.Never);
		}

		static Mock<IOverlapConflictCommandFactory> GetCommandFactory(bool overlapConflict)
		{
			var mockCommand = new Mock<IOverlapConflictCommand>();
			mockCommand.SetupGet(c => c.OverlapConflict).Returns(overlapConflict);

			var mockCommandFactory = new Mock<IOverlapConflictCommandFactory>();
			mockCommandFactory.Setup(cf => cf.CheckOverlapConflicts(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int?>())).Returns(mockCommand.Object);

			return mockCommandFactory;
		}
	}
}
