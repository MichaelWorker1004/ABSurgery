using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IFellowship : IYtgBusinessBase
    {
        int Id { get; set; }
        int UserId { get; set; }
        string ProgramName { get; set; }
        string CompletionYear { get; set; }
        string ProgramOther { get; set; }
    }
}
