using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.CE
{
	public class GetExamCasesScoredCommandDto : YtgBusinessBaseDto
	{
		public int ExamScheduleId { get; set; }

		public bool CasesScored { get; set; }
	}
}
