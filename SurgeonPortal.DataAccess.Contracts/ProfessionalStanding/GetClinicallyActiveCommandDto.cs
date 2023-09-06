using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
	public class GetClinicallyActiveCommandDto : YtgBusinessBaseDto
	{
		public int? UserId { get; set; }
		public bool? ClinicallyActive { get; set; }
	}
}
