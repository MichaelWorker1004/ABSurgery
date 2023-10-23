using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.Dev;
using SurgeonPortal.Library.Contracts.Dev;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;


namespace SurgeonPortal.Library.Dev
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class ResetScoresCommand : YtgCommandBase<ResetScoresCommand, int>, IResetScoresCommand
    {
        private readonly IResetScoresCommandDal _resetScoresCommandDal;


        public ResetScoresCommand(
            IIdentityProvider identityProvider,
            IResetScoresCommandDal resetScoresCommandDal)
            : base(identityProvider)
        {
            _resetScoresCommandDal = resetScoresCommandDal;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        public int ExaminerUserId
		{
			get { return ReadProperty(ExaminerUserIdProperty); }
			internal set { LoadProperty(ExaminerUserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExaminerUserIdProperty = RegisterProperty<int>(c => c.ExaminerUserId);


        [Execute]
        protected new async Task ExecuteCommand()
        {
                await _resetScoresCommandDal.ResetExamScoresAsync(_identity.GetUserId<int>());
            }


        
    }
}
