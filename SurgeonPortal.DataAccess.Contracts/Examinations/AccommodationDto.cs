using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public class AccommodationDto : YtgBusinessBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccommodationID { get; set; }
        public string AccommodationName { get; set; }
        public int? DocumentId { get; set; }
        public string DocumentName { get; set; }
        public int? ExamID { get; set; }
    }
}
