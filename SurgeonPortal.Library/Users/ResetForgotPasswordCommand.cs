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
    public class ResetForgotPasswordCommand : YtgCommandBase<ResetForgotPasswordCommand, int>, IResetForgotPasswordCommand
    {
        private readonly IResetForgotPasswordCommandDal _resetForgotPasswordCommandDal;


        public ResetForgotPasswordCommand(
            IIdentityProvider identityProvider,
            IResetForgotPasswordCommandDal resetForgotPasswordCommandDal)
            : base(identityProvider)
        {
            _resetForgotPasswordCommandDal = resetForgotPasswordCommandDal;
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

        public string NewPassword
		{
			get { return ReadProperty(NewPasswordProperty); }
			internal set { LoadProperty(NewPasswordProperty, value); }
		}
		public static readonly PropertyInfo<string> NewPasswordProperty = RegisterProperty<string>(c => c.NewPassword);

        public bool? Result
		{
			get { return ReadProperty(ResultProperty); }
			internal set { LoadProperty(ResultProperty, value); }
		}
		public static readonly PropertyInfo<bool?> ResultProperty = RegisterProperty<bool?>(c => c.Result);


        [Execute]
        protected async Task ExecuteCommand()
        {
                var dto = await _resetForgotPasswordCommandDal.ResetForgotPasswordAsync(
                    ResetGUID,
                    NewPassword);
            
            			this.ResetGUID = dto.ResetGUID;
        			this.NewPassword = dto.NewPassword;
        			this.Result = dto.Result;
        }


        
    }
}
