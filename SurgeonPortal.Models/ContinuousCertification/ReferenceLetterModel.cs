using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.ContinuousCertification
{
    public class ReferenceLetterModel
    {
        public int? UserId { get; set; }
        public string Official { get; set; }
        public string RoleName { get; set; }
        public string AltRoleName { get; set; }
        public int? RoleId { get; set; }
        public int? AltRoleId { get; set; }
        public string Explain { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Hosp { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string FullName { get; set; }
    }
}
