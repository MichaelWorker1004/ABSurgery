using Csla.Core;
using Csla.Rules;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Shared;
using System.Collections.Generic;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
	public class ExplainRequiredWhenRule : BusinessRule
	{
		public ExplainRequiredWhenRule(IPropertyInfo primaryProperty, int priority) : base(primaryProperty)
		{
			Priority = priority;
			InputProperties = new List<IPropertyInfo> { primaryProperty };
		}

		protected override void Execute(IRuleContext context)
		{
			var explain = (string)context.InputPropertyValues[PrimaryProperty];
			var target = context.Target as IRotation;

			if (target.ClinicalLevelId == (int)ClinicalLevels.OtherClinicalFellowship && string.IsNullOrEmpty(explain))
			{
				context.AddErrorResult(PrimaryProperty, "Explain is required when clinical level is Other Clinical Fellowship");
			}
		}
	}
}
