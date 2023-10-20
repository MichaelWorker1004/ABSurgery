using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;


namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class ExamSessionSkipCommand : CommandBase<ExamSessionSkipCommand>, IExamSessionSkipCommand
    {
        private readonly IExamSessionSkipCommandDal _examSessionSkipCommandDal;


        public ExamSessionSkipCommand(IExamSessionSkipCommandDal examSessionSkipCommandDal)
        {
            _examSessionSkipCommandDal = examSessionSkipCommandDal;
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

        public int ExaminerUserId
		{
			get { return ReadProperty(ExaminerUserIdProperty); }
			internal set { LoadProperty(ExaminerUserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExaminerUserIdProperty = RegisterProperty<int>(c => c.ExaminerUserId);


        [Execute]
        protected new async Task ExecuteCommand()
        {
                await _examSessionSkipCommandDal.SkipExamSessionAsync(
                    ExamScheduleId,
                    _identity.GetUserId<int>());
            }


        
    }
}
