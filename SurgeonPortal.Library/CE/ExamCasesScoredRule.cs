using Csla.Rules;
using SurgeonPortal.Library.Contracts.CE;

namespace SurgeonPortal.Library.CE
{
	public class ExamCasesScoredRule : BusinessRule
	{
		private IGetExamCasesScoredCommandFactory _getExamCasesScoredCommandFactory;

		public ExamCasesScoredRule(IGetExamCasesScoredCommandFactory getExamCasesScoredCommandFactory, int priority)
		{
			Priority = priority;
			_getExamCasesScoredCommandFactory = getExamCasesScoredCommandFactory;
		}

		protected override void Execute(IRuleContext context)
		{
			var target = context.Target as IExamScore;

			var command = _getExamCasesScoredCommandFactory.GetExamCasesScored(target.ExamScheduleId);

			if (!command.CasesScored)
			{
				context.AddErrorResult("All exam cases must be scored before saving the exam score.");
			}
		}
	}
}
