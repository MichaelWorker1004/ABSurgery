using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IPdReferenceLetter : IYtgBusinessBase
    {
        decimal Id { get; set; }
        int? UserId { get; set; }
        string Hosp { get; set; }
        string Official { get; set; }
        string Title { get; set; }
        string Email { get; set; }
        DateTime? LetterSent { get; set; }
        int Status { get; set; }
        int ExamId { get; set; }
        string IdCode { get; set; }
        string FullName { get; set; }
    }
}
