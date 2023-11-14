using Csla;
using Csla.Core;
using Csla.Rules;
using Moq;
using NUnit.Framework;
using SurgeonPortal.Library.Contracts.Scoring;
using SurgeonPortal.Library.Scoring;
using System.Collections.Generic;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.Scoring
{
	public class ExamLockedRuleTests: TestBase<int>
	{
		internal class TestableExamLockedRule : ExamLockedRule
		{
			public TestableExamLockedRule(IIsExamSessionLockedCommandFactory isExamSessionLockedCommandFactory, IPropertyInfo primaryProperty, int priority) : base(isExamSessionLockedCommandFactory, primaryProperty, priority)
			{
			}

			public void TestableExecute(IRuleContext context)
			{
				Execute(context);
			}
		}

		[Test]
		public void Execute_ShouldNotAddError_WhenExamIsNotLocked()
		{
			var mockCommandFactory = GetMockedCommandFactory(false);

			var primaryProperty = new PropertyInfo<int>("ExamCaseId");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(x => x.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
				{
					{primaryProperty, 1 }
				});

			var rule = new TestableExamLockedRule(mockCommandFactory.Object, primaryProperty, 1);

			rule.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Execute_ShouldNotAddError_WhenExamIsLocked()
		{
			var mockCommandFactory = GetMockedCommandFactory(true);

			var primaryProperty = new PropertyInfo<int>("ExamCaseId");

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(x => x.InputPropertyValues).Returns(new Dictionary<IPropertyInfo, object>
				{
					{primaryProperty, 1 }
				});

			var rule = new TestableExamLockedRule(mockCommandFactory.Object, primaryProperty, 1);

			rule.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult("Cannot add or update score when exam is locked."), Times.Once);
		}

		static Mock<IIsExamSessionLockedCommandFactory> GetMockedCommandFactory(bool isLocked)
		{
			var mockCommand = new Mock<IIsExamSessionLockedCommand>();
			mockCommand.SetupGet(x => x.IsLocked).Returns(isLocked);

			var mockCommandFactory = new Mock<IIsExamSessionLockedCommandFactory>();
			mockCommandFactory.Setup(x => x.IsExamSessionLocked(It.IsAny<int>())).Returns(mockCommand.Object);

			return mockCommandFactory;
		}
	}
}
