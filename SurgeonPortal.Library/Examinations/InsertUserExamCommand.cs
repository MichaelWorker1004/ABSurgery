using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;


namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class InsertUserExamCommand : YtgCommandBase<InsertUserExamCommand, int>, IInsertUserExamCommand
    {
        private readonly IInsertUserExamCommandDal _insertUserExamCommandDal;


        public InsertUserExamCommand(
            IIdentityProvider identityProvider,
            IInsertUserExamCommandDal insertUserExamCommandDal)
            : base(identityProvider)
        {
            _insertUserExamCommandDal = insertUserExamCommandDal;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        public int UserId
		{
			get { return ReadProperty(UserIdProperty); }
			internal set { LoadProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        public int ExamHeaderId
		{
			get { return ReadProperty(ExamHeaderIdProperty); }
			internal set { LoadProperty(ExamHeaderIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExamHeaderIdProperty = RegisterProperty<int>(c => c.ExamHeaderId);


        [Execute]
        protected async Task ExecuteCommand()
        {
                await _insertUserExamCommandDal.InsertUserExamAsync(
                    _identity.GetUserId<int>(),
                    ExamHeaderId);
            }


        
    }
}
