using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using System;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;


namespace SurgeonPortal.Library.Users
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class VerifyForgotGuidCommand : YtgCommandBase<VerifyForgotGuidCommand, int>, IVerifyForgotGuidCommand
    {
        private readonly IVerifyForgotGuidCommandDal _verifyForgotGuidCommandDal;


        public VerifyForgotGuidCommand(
            IIdentityProvider identityProvider,
            IVerifyForgotGuidCommandDal verifyForgotGuidCommandDal)
            : base(identityProvider)
        {
            _verifyForgotGuidCommandDal = verifyForgotGuidCommandDal;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        public Guid ResetGUID
		{
			get { return ReadProperty(ResetGUIDProperty); }
			internal set { LoadProperty(ResetGUIDProperty, value); }
		}
		public static readonly PropertyInfo<Guid> ResetGUIDProperty = RegisterProperty<Guid>(c => c.ResetGUID);

        public bool Result
		{
			get { return ReadProperty(ResultProperty); }
			internal set { LoadProperty(ResultProperty, value); }
		}
		public static readonly PropertyInfo<bool> ResultProperty = RegisterProperty<bool>(c => c.Result);


        [Execute]
        protected async Task ExecuteCommand()
        {
                var dto = await _verifyForgotGuidCommandDal.VerifyForgotPasswordGuidAsync(ResetGUID);
            
            			this.ResetGUID = dto.ResetGUID;
        			this.Result = dto.Result;
        }


        
    }
}
