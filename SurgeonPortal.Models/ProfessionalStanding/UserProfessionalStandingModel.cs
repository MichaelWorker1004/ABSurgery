using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.ProfessionalStanding
{
    public class UserProfessionalStandingModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PrimaryPracticeId { get; set; }
        public string PrimaryPractice { get; set; }
        public int? OrganizationTypeId { get; set; }
        public string OrganizationType { get; set; }
        public string ExplanationOfNonPrivileges { get; set; }
        public string ExplanationOfNonClinicalActivities { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
