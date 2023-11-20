using Csla.Core;
using Csla.Rules;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Shared;
using System.Collections.Generic;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
	public class FourMonthRotationExplainRequiredWhenRule : BusinessRule
	{
		public FourMonthRotationExplainRequiredWhenRule(IPropertyInfo primaryProperty,
				int priority) : base(primaryProperty)
		{
			Priority = priority;
			InputProperties = new List<IPropertyInfo> { primaryProperty };
		}

		protected override void Execute(IRuleContext context)
		{
			var explain = (string)context.InputPropertyValues[PrimaryProperty];

			var target = context.Target as IRotation;
			var startDate = target.StartDate;
			var endDate = target.EndDate;
			var clinicalLevelId = target.ClinicalLevelId;

			if (clinicalLevelId == (int)ClinicalLevels.ClinicalLevel4Chief || clinicalLevelId == (int)ClinicalLevels.ClinicalLevel5Chief)
			{
				var duration = endDate - startDate;
				if (duration.Days + 1 > 120 && string.IsNullOrEmpty(explain))
				{
					context.AddErrorResult(PrimaryProperty, "FourMonthRotationExplain is required when the duration is over 4 months and clinical level is 4 Chief or 5 Chief");
				}
			}
		}
	}
}
