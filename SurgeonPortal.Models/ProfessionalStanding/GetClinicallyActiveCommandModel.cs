using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.ProfessionalStanding
{
    public class GetClinicallyActiveCommandModel
    {
        public int? UserId { get; set; }
        public bool? ClinicallyActive { get; set; }
    }
}
