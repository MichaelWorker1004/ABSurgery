using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Surgeons
{
    public interface ICertificationReadOnly : IYtgReadOnlyBase
    {
        string Speciality { get; }
        string CertificateId { get; }
        string InitialCertificationDate { get; }
        string EndDateDisplay { get; }
        int IsClinicallyInactive { get; }
    }
}
