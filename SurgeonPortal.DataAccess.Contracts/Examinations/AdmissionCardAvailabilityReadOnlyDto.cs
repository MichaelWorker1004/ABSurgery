namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public class AdmissionCardAvailabilityReadOnlyDto
    {
        public bool AdmCardAvailable { get; set; }
        public int? ExamId { get; set; }
    }
}
