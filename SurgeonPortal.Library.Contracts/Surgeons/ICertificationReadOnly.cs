using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Surgeons
{
    public interface ICertificationReadOnly : IYtgReadOnlyBase<int>
    {
        string Speciality { get; }
        string CertificateId { get; }
        string InitialCertificationDate { get; }
        string EndDateDisplay { get; }
        int IsClinicallyInactive { get; }
        string Status { get; }
    }
}
