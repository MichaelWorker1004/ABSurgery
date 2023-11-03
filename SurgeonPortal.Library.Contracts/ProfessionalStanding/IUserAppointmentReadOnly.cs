using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IUserAppointmentReadOnly : IYtgReadOnlyBase<int>
    {
        decimal ApptId { get; }
        int? UserId { get; }
        int? PracticeTypeId { get; }
        string PracticeType { get; }
        int? AppointmentTypeId { get; }
        string AppointmentType { get; }
        int? OrganizationTypeId { get; }
        string AuthorizingOfficial { get; }
        string OrganizationType { get; }
        int? OrganizationId { get; }
        string StateCode { get; }
        string Other { get; }
        string OrganizationName { get; }
    }
}
