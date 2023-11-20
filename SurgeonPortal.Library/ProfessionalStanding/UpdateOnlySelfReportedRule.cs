using Csla.Rules;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;

namespace SurgeonPortal.Library.ProfessionalStanding
{
	public class UpdateOnlySelfReportedRule : BusinessRule
	{
		private const string SelfReportingOrganization = "Self";

		public UpdateOnlySelfReportedRule(int priority)
		{
			Priority = priority;
		}

		protected override void Execute(IRuleContext context)
		{
			var target = context.Target as IMedicalLicense;

			if (!target.IsNew && !target.ReportingOrganization.Equals(SelfReportingOrganization, StringComparison.InvariantCultureIgnoreCase))
			{
				context.AddErrorResult("User cannot edit Medical Licenses that are not self reported.");
			}
		}
	}
}
