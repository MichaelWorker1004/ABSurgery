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
    public class UpdateQeAttestationsCommand : YtgCommandBase<UpdateQeAttestationsCommand, int>, IUpdateQeAttestationsCommand
    {
        private readonly IUpdateQeAttestationsCommandDal _updateQeAttestationsCommandDal;


        public UpdateQeAttestationsCommand(
            IIdentityProvider identityProvider,
            IUpdateQeAttestationsCommandDal updateQeAttestationsCommandDal)
            : base(identityProvider)
        {
            _updateQeAttestationsCommandDal = updateQeAttestationsCommandDal;
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

        public int ExamId
		{
			get { return ReadProperty(ExamIdProperty); }
			internal set { LoadProperty(ExamIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExamIdProperty = RegisterProperty<int>(c => c.ExamId);


        [Execute]
        protected async Task ExecuteCommand()
        {
                await _updateQeAttestationsCommandDal.UpdateQeAttestationsAsync(
                    _identity.GetUserId<int>(),
                    ExamId);
            }


        
    }
}
