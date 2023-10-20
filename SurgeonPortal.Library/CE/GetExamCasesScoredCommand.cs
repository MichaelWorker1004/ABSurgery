using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.CE;
using SurgeonPortal.Library.Contracts.CE;


namespace SurgeonPortal.Library.CE
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class GetExamCasesScoredCommand : CommandBase<GetExamCasesScoredCommand>, IGetExamCasesScoredCommand
    {
        private readonly IGetExamCasesScoredCommandDal _getExamCasesScoredCommandDal;


        public GetExamCasesScoredCommand(IGetExamCasesScoredCommandDal getExamCasesScoredCommandDal)
        {
            _getExamCasesScoredCommandDal = getExamCasesScoredCommandDal;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        public int ExamScheduleId
		{
			get { return ReadProperty(ExamScheduleIdProperty); }
			internal set { LoadProperty(ExamScheduleIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExamScheduleIdProperty = RegisterProperty<int>(c => c.ExamScheduleId);

        public bool CasesScored
		{
			get { return ReadProperty(CasesScoredProperty); }
			internal set { LoadProperty(CasesScoredProperty, value); }
		}
		public static readonly PropertyInfo<bool> CasesScoredProperty = RegisterProperty<bool>(c => c.CasesScored);


        [Execute]
        protected void ExecuteCommand()
        {
                var dto = _getExamCasesScoredCommandDal.GetExamCasesScored(
                    ExamScheduleId,
                    _identity.GetUserId<int>());
            
            			this.ExamScheduleId = dto.ExamScheduleId;
        			this.CasesScored = dto.CasesScored;
        }


        
    }
}
