using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Surgeons
{
    public interface ICertificationReadOnly : IYtgBusinessBase
    {
        string Speciality { get; set; }
        string CertificateId { get; set; }
        string InitialCertificationDate { get; set; }
        string EndDateDisplay { get; set; }
    }
}
