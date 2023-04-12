using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Surgeons
{
    public class CertificationReadOnlyDto : YtgBusinessBaseDto
    {
        public string Speciality { get; set; }
        public string CertificateId { get; set; }
        public string InitialCertificationDate { get; set; }
        public string EndDateDisplay { get; set; }
    }
}
