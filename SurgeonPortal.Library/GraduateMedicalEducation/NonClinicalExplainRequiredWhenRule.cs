using Csla.Core;
using Csla.Rules;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Shared;
using System.Collections.Generic;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
	public class NonClinicalExplainRequiredWhenRule : BusinessRule
	{
		public NonClinicalExplainRequiredWhenRule(IPropertyInfo primaryProperty,
				int priority) : base(primaryProperty)
		{
			Priority = priority;
			InputProperties = new List<IPropertyInfo> { primaryProperty };
		}

		protected override void Execute(IRuleContext context)
		{
			var explain = (string)context.InputPropertyValues[PrimaryProperty];

			var target = context.Target as IRotation;
			var clinicalLevelId = target.ClinicalLevelId;
			var clinicalActivityId = target.ClinicalActivityId;

			if (clinicalLevelId == (int)ClinicalLevels.ClinicalLevel4 || clinicalLevelId == (int)ClinicalLevels.ClinicalLevel5)
			{
				if (clinicalActivityId == (int)ClinicalActivities.NonClinicalResearch || clinicalActivityId == (int)ClinicalActivities.ClinicalNonSurgical)
				{
					if (string.IsNullOrEmpty(explain))
					{
						context.AddErrorResult(PrimaryProperty, "NonClinicalExplain is required when the clinical level is 4 or 5 and the clinical activity is Non-Clinical Research or Clinical (Non-Surgical)");
					}
				}
			}
		}
	}
}
