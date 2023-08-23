using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
	public class IsExamSessionLockedCommandDto : YtgBusinessBaseDto
	{
		public int ExamCaseId { get; set; }
		public bool? IsLocked { get; set; }
	}
}
