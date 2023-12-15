using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IReferenceLetterReadOnly : IYtgReadOnlyBase<int>
    {
        decimal id { get; }
        int? UserId { get; }
        string Hosp { get; }
        string Official { get; }
        string Title { get; }
        string RoleName { get; }
        string AltRoleName { get; }
        int? RoleId { get; }
        int? AltRoleId { get; }
        string Explain { get; }
        string Email { get; }
        string Phone { get; }
        DateTime? LetterSent { get; }
        int Status { get; }
    }
}
