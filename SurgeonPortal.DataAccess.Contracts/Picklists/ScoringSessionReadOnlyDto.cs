namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public class ScoringSessionReadOnlyDto
    {
        public string ExamSchedule { get; set; }
        public int Session1Id { get; set; }
        public int? Session2Id { get; set; }
    }
}
