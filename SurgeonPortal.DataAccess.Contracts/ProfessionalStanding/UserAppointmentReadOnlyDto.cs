namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public class UserAppointmentReadOnlyDto
    {
        public decimal ApptId { get; set; }
        public int? UserId { get; set; }
        public int? PracticeTypeId { get; set; }
        public string PracticeType { get; set; }
        public int? AppointmentTypeId { get; set; }
        public string AppointmentType { get; set; }
        public int? OrganizationTypeId { get; set; }
        public string AuthorizingOfficial { get; set; }
        public string OrganizationType { get; set; }
        public int? OrganizationId { get; set; }
        public string StateCode { get; set; }
        public string Other { get; set; }
        public string OrganizationName { get; set; }
    }
}
