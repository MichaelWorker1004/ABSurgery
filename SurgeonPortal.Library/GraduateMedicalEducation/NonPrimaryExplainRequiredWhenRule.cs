using Csla.Core;
using Csla.Rules;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Shared;
using System.Collections.Generic;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
	public class NonPrimaryExplainRequiredWhenRule : BusinessRule
	{
		public NonPrimaryExplainRequiredWhenRule(IPropertyInfo primaryProperty,
				int priority) : base(primaryProperty)
		{
			Priority = priority;
			InputProperties = new List<IPropertyInfo> { primaryProperty };
		}

		protected override void Execute(IRuleContext context)
		{
			var explain = (string)context.InputPropertyValues[PrimaryProperty];

			var target = context.Target as IRotation;
			var isEssential = target.IsEssential;
			var clinicalLevelId = target.ClinicalLevelId;

			if (string.IsNullOrEmpty(explain) && !isEssential &&
				(clinicalLevelId == (int)ClinicalLevels.ClinicalLevel4Chief || clinicalLevelId == (int)ClinicalLevels.ClinicalLevel5Chief))
			{
				context.AddErrorResult(PrimaryProperty, "NonPrimaryExplain when isEssential is false and clinical level is 4 Chief or 5 Chief");
			}
		}
	}
}
