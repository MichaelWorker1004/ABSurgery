using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public class SanctionsDto : YtgBusinessBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool HadDrugAlchoholTreatment { get; set; }
        public bool HadHospitalPrivilegesDenied { get; set; }
        public bool HadLicenseRestricted { get; set; }
        public bool HadHospitalPrivilegesRestricted { get; set; }
        public bool HadFelonyConviction { get; set; }
        public bool HadCensure { get; set; }
        public string Explanation { get; set; }
    }
}
