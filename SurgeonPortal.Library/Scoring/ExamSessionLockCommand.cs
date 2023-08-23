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
    public class ExamSessionLockCommand : CommandBase<ExamSessionLockCommand>, IExamSessionLockCommand
    {
        private readonly IExamSessionLockCommandDal _examSessionLockCommandDal;


        public ExamSessionLockCommand(IExamSessionLockCommandDal examSessionLockCommandDal)
        {
            _examSessionLockCommandDal = examSessionLockCommandDal;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        public int ExamscheduleId
		{
			get { return ReadProperty(ExamscheduleIdProperty); }
			internal set { LoadProperty(ExamscheduleIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExamscheduleIdProperty = RegisterProperty<int>(c => c.ExamscheduleId);


        [Execute]
        protected new async Task ExecuteCommand()
        {
                await _examSessionLockCommandDal.LockExamSessionAsync(ExamscheduleId);
            }


        
    }
}
