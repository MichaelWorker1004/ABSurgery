using Csla.Core;
using Csla.Rules;
using SurgeonPortal.Library.Contracts.Scoring;
using System.Collections.Generic;

namespace SurgeonPortal.Library.Scoring
{
	public class ExamLockedRule : BusinessRule
	{
		private IIsExamSessionLockedCommandFactory _isExamSessionLockedCommandFactory;

		public ExamLockedRule(IIsExamSessionLockedCommandFactory isExamSessionLockedCommandFactory,
			IPropertyInfo primaryProperty,
			int priority)
			: base(primaryProperty)
		{
			_isExamSessionLockedCommandFactory = isExamSessionLockedCommandFactory;
			InputProperties = new List<IPropertyInfo> { primaryProperty };
			Priority = priority;
		}

		protected override void Execute(IRuleContext context)
		{
			var examCaseId = (int)context.InputPropertyValues[PrimaryProperty];

			var command = _isExamSessionLockedCommandFactory.IsExamSessionLocked(examCaseId);

			if (command.IsLocked.HasValue && command.IsLocked.Value)
			{
				context.AddErrorResult("Cannot add or update score when exam is locked.");
			}
		}
	}
}
