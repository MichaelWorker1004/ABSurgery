using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface ISanctions : IYtgBusinessBase
    {
        int Id { get; set; }
        int UserId { get; set; }
        bool HadDrugAlchoholTreatment { get; set; }
        bool HadHospitalPrivilegesDenied { get; set; }
        bool HadLicenseRestricted { get; set; }
        bool HadHospitalPrivilegesRestricted { get; set; }
        bool HadFelonyConviction { get; set; }
        bool HadCensure { get; set; }
        string Explanation { get; set; }
    }
}
