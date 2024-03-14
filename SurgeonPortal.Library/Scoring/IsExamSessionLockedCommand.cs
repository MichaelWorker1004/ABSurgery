using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;


namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class IsExamSessionLockedCommand : YtgCommandBase<IsExamSessionLockedCommand, int>, IIsExamSessionLockedCommand
    {
        private readonly IIsExamSessionLockedCommandDal _isExamSessionLockedCommandDal;


        public IsExamSessionLockedCommand(
            IIdentityProvider identityProvider,
            IIsExamSessionLockedCommandDal isExamSessionLockedCommandDal)
            : base(identityProvider)
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
            if (ExamCaseId != 0)
            {
                var dto = _isExamSessionLockedCommandDal.IsExamSessionLocked(ExamCaseId);

                if(dto == null)
                {
                    this.ExamCaseId = 0;
                    this.IsLocked = true; //set it to true to be on the safe side. still testing and debugging
                }
                else
                {
                    this.IsLocked = dto.IsLocked;
                }
            }
            else
            {
                this.ExamCaseId = 0;
                this.IsLocked = true; //set it to true to be on the safe side. still testing and debugging
            }
        }
        
    }
}
