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
    public class IsExamSessionLockedCommand : CommandBase<IsExamSessionLockedCommand>, IIsExamSessionLockedCommand
    {
        private readonly IIsExamSessionLockedCommandDal _isExamSessionLockedCommandDal;


        public IsExamSessionLockedCommand(IIsExamSessionLockedCommandDal isExamSessionLockedCommandDal)
        {
            _isExamSessionLockedCommandDal = isExamSessionLockedCommandDal;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        public int ExamCaseId
		{
			get { return ReadProperty(ExamCaseIdProperty); }
			internal set { LoadProperty(ExamCaseIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExamCaseIdProperty = RegisterProperty<int>(c => c.ExamCaseId);

        public bool? IsLocked
		{
			get { return ReadProperty(IsLockedProperty); }
			internal set { LoadProperty(IsLockedProperty, value); }
		}
		public static readonly PropertyInfo<bool?> IsLockedProperty = RegisterProperty<bool?>(c => c.IsLocked);


        [Execute]
        protected void ExecuteCommand()
        {
                var dto = _isExamSessionLockedCommandDal.IsExamSessionLocked(ExamCaseId);
            
            			this.ExamCaseId = dto.ExamCaseId;
        			this.IsLocked = dto.IsLocked;
        }


        
    }
}
