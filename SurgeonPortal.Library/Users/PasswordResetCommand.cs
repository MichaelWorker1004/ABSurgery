using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;


namespace SurgeonPortal.Library.Users
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class PasswordResetCommand : YtgCommandBase<PasswordResetCommand, int>, IPasswordResetCommand
    {
        private readonly IPasswordResetCommandDal _passwordResetCommandDal;


        public PasswordResetCommand(
            IIdentityProvider identityProvider,
            IPasswordResetCommandDal passwordResetCommandDal)
            : base(identityProvider)
        {
            _passwordResetCommandDal = passwordResetCommandDal;
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

        public string OldPassword
		{
			get { return ReadProperty(OldPasswordProperty); }
			internal set { LoadProperty(OldPasswordProperty, value); }
		}
		public static readonly PropertyInfo<string> OldPasswordProperty = RegisterProperty<string>(c => c.OldPassword);

        public string NewPassword
		{
			get { return ReadProperty(NewPasswordProperty); }
			internal set { LoadProperty(NewPasswordProperty, value); }
		}
		public static readonly PropertyInfo<string> NewPasswordProperty = RegisterProperty<string>(c => c.NewPassword);

        public bool? PasswordReset
		{
			get { return ReadProperty(PasswordResetProperty); }
			internal set { LoadProperty(PasswordResetProperty, value); }
		}
		public static readonly PropertyInfo<bool?> PasswordResetProperty = RegisterProperty<bool?>(c => c.PasswordReset);


        [Execute]
        protected async Task ExecuteCommand()
        {
                var dto = await _passwordResetCommandDal.ResetPasswordAsync(
                    _identity.GetUserId<int>(),
                    OldPassword,
                    NewPassword);
            
            			this.UserId = dto.UserId;
        			this.OldPassword = dto.OldPassword;
        			this.NewPassword = dto.NewPassword;
        			this.PasswordReset = dto.PasswordReset;
        }


        
    }
}
