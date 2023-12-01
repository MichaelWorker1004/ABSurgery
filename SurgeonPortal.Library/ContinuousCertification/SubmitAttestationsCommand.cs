using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using SurgeonPortal.Library.Contracts.Identity;


namespace SurgeonPortal.Library.ContinuousCertification
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class SubmitAttestationsCommand : YtgCommandBase<SubmitAttestationsCommand, int>, ISubmitAttestationsCommand
    {
        private readonly ISubmitAttestationsCommandDal _submitAttestationsCommandDal;


        public SubmitAttestationsCommand(
            IIdentityProvider identityProvider,
            ISubmitAttestationsCommandDal submitAttestationsCommandDal)
            : base(identityProvider)
        {
            _submitAttestationsCommandDal = submitAttestationsCommandDal;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return ApplicationContext.User.IsInRole(SurgeonPortalClaims.SurgeonClaim);
        }

        public int UserId
		{
			get { return ReadProperty(UserIdProperty); }
			internal set { LoadProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        public DateTime SigReceive
		{
			get { return ReadProperty(SigReceiveProperty); }
			internal set { LoadProperty(SigReceiveProperty, value); }
		}
		public static readonly PropertyInfo<DateTime> SigReceiveProperty = RegisterProperty<DateTime>(c => c.SigReceive);

        public DateTime CertnoticeReceive
		{
			get { return ReadProperty(CertnoticeReceiveProperty); }
			internal set { LoadProperty(CertnoticeReceiveProperty, value); }
		}
		public static readonly PropertyInfo<DateTime> CertnoticeReceiveProperty = RegisterProperty<DateTime>(c => c.CertnoticeReceive);


        [Execute]
        protected async Task ExecuteCommand()
        {
                await _submitAttestationsCommandDal.GetExamCasesScoredAsync(
                    _identity.GetUserId<int>(),
                    SigReceive,
                    CertnoticeReceive);
            }


        
    }
}
