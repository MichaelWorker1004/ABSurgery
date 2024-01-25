namespace SurgeonPortal.DataAccess.Contracts.Surgeons
{
    public class CertificationReadOnlyDto
    {
        public string Speciality { get; set; }
        public string CertificateId { get; set; }
        public string InitialCertificationDate { get; set; }
        public string EndDateDisplay { get; set; }
        public int IsClinicallyInactive { get; set; }
        public string Status { get; set; }
    }
}
