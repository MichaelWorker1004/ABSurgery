using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IReferenceLetter : IYtgBusinessBase
    {
        int? UserId { get; set; }
        string Official { get; set; }
        string RoleName { get; set; }
        string AltRoleName { get; set; }
        int? RoleId { get; set; }
        int? AltRoleId { get; set; }
        string Explain { get; set; }
        string Title { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Hosp { get; set; }
        string City { get; set; }
        string State { get; set; }
        string FullName { get; set; }
        public int SecOrder { get; set; }
        public string IdCode { get; set; }
    }
}
