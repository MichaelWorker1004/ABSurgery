using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IUserAppointment : IYtgBusinessBase
    {
        decimal ApptId { get; set; }
        int? UserId { get; set; }
        int? PracticeTypeId { get; set; }
        string PracticeType { get; set; }
        int? AppointmentTypeId { get; set; }
        string AppointmentType { get; set; }
        int? OrganizationTypeId { get; set; }
        string AuthorizingOfficial { get; set; }
        string OrganizationType { get; set; }
        int? OrganizationId { get; set; }
        string StateCode { get; set; }
        string Other { get; set; }
        string OrganizationName { get; set; }
    }
}
