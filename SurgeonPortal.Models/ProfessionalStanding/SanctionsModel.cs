using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.ProfessionalStanding
{
    public class SanctionsModel
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
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
