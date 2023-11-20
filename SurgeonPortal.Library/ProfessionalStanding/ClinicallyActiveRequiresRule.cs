using Csla.Core;
using Csla.Rules;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System.Collections.Generic;

namespace SurgeonPortal.Library.ProfessionalStanding
{
	public class ClinicallyActiveRequiresRule : BusinessRule
	{
		private IGetClinicallyActiveCommandFactory _getClinicallyActiveCommandFactory;

		public ClinicallyActiveRequiresRule(IGetClinicallyActiveCommandFactory getClinicallyActiveCommandFactory, 
			IPropertyInfo primaryProperty, 
			int priority)
			: base(primaryProperty)
		{
			_getClinicallyActiveCommandFactory = getClinicallyActiveCommandFactory;
			Priority = priority;
			InputProperties = new List<IPropertyInfo> { primaryProperty };
		}

		protected override void Execute(IRuleContext context)
		{
			var command = _getClinicallyActiveCommandFactory.GetClinicallyActiveByUserId();

			var target = context.Target as IUserProfessionalStanding;

			if (command.ClinicallyActive.HasValue && command.ClinicallyActive.Value)
			{
				if (!target.PrimaryPracticeId.HasValue)
				{
					context.AddErrorResult(PrimaryProperty, "Primary practice is required when clinically active is true");
				}

				if (!target.OrganizationTypeId.HasValue)
				{
					context.AddErrorResult(PrimaryProperty, "Organization type is required when clinically active is true");
				}
			}
			else
			{
				if (string.IsNullOrEmpty(target.ExplanationOfNonPrivileges))
				{
					context.AddErrorResult(PrimaryProperty, "Explanation of non-privaleges is required when clinically active is false");
				}

				if (string.IsNullOrEmpty(target.ExplanationOfNonClinicalActivities))
				{
					context.AddErrorResult(PrimaryProperty, "Explanation of non-clinical activities is required when clinically active is false");
				}
			}
		}
	}
}
