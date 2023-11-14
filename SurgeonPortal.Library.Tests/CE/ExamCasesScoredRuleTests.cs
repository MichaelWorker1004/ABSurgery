using Csla.Rules;
using Moq;
using NUnit.Framework;
using SurgeonPortal.Library.CE;
using SurgeonPortal.Library.Contracts.CE;
using Ytg.UnitTest;

namespace SurgeonPortal.Library.Tests.CE
{
	internal class TestableExamCasesScoredRule : ExamCasesScoredRule
	{
		public TestableExamCasesScoredRule(IGetExamCasesScoredCommandFactory getExamCasesScoredCommandFactory, int priority) : base(getExamCasesScoredCommandFactory, priority)
		{
		}

		public void TestableExecute(IRuleContext context)
		{
			Execute(context);
		}
	}

	public class ExamCasesScoredRuleTests : TestBase<int>
	{
		[Test]
		public void Execute_ShouldNotAddError_WhenScored()
		{
			var (mockCommand, mockFactory) = GetMockedCommand(true);

			var mockExamScore = new Mock<IExamScore>();
			mockExamScore.SetupGet(x => x.ExamScheduleId).Returns(1);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(x => x.Target).Returns(mockExamScore.Object);

			var rule = new TestableExamCasesScoredRule(mockFactory.Object, 1);

			rule.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Execute_ShouldAddError_WhenNotScored()
		{
			var (mockCommand, mockFactory) = GetMockedCommand(false);

			var mockExamScore = new Mock<IExamScore>();
			mockExamScore.SetupGet(x => x.ExamScheduleId).Returns(1);

			var mockContext = new Mock<IRuleContext>();
			mockContext.SetupGet(x => x.Target).Returns(mockExamScore.Object);

			var rule = new TestableExamCasesScoredRule(mockFactory.Object, 1);

			rule.TestableExecute(mockContext.Object);

			mockContext.Verify(x => x.AddErrorResult(It.IsAny<string>()), Times.Once);
		}

		static (Mock<IGetExamCasesScoredCommand>, Mock<IGetExamCasesScoredCommandFactory>) GetMockedCommand(bool casesScored)
		{
			var mockCommand = new Mock<IGetExamCasesScoredCommand>();
			mockCommand.SetupGet(x => x.CasesScored).Returns(casesScored);

			var mockCommandFactory = new Mock<IGetExamCasesScoredCommandFactory>();
			mockCommandFactory.Setup(x => x.GetExamCasesScored(It.IsAny<int>())).Returns(mockCommand.Object);

			return (mockCommand, mockCommandFactory);

		}
	}
}
