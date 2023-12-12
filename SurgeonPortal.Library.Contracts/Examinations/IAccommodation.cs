using SurgeonPortal.Library.Contracts.Documents;
using System.IO;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IAccommodation : IYtgBusinessBase
    {
        int Id { get; set; }
        int UserId { get; set; }
        int AccommodationID { get; set; }
        string AccommodationName { get; set; }
        int? DocumentId { get; set; }
        string DocumentName { get; set; }
        int? ExamID { get; set; }
        IDocument Document { get; }
        void LoadDocument(Stream file);
    }
}
