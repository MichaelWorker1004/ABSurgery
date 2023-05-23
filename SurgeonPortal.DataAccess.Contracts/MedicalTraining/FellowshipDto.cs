using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public class FellowshipDto : YtgBusinessBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProgramName { get; set; }
        public string CompletionYear { get; set; }
        public string ProgramOther { get; set; }
    }
}
