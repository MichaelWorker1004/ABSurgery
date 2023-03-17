using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;


namespace SurgeonPortal.Library.Users
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class PasswordValidationCommand : CommandBase<PasswordValidationCommand>, IPasswordValidationCommand
    {
        private readonly IPasswordValidationCommandDal _passwordValidationCommandDal;


        public PasswordValidationCommand(IPasswordValidationCommandDal passwordValidationCommandDal)
        {
            _passwordValidationCommandDal = passwordValidationCommandDal;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        public bool? PasswordsMatch
		{
			get { return ReadProperty(PasswordsMatchProperty); }
			internal set { LoadProperty(PasswordsMatchProperty, value); }
		}
		public static readonly PropertyInfo<bool?> PasswordsMatchProperty = RegisterProperty<bool?>(c => c.PasswordsMatch);

        public string Password
		{
			get { return ReadProperty(PasswordProperty); }
			internal set { LoadProperty(PasswordProperty, value); }
		}
		public static readonly PropertyInfo<string> PasswordProperty = RegisterProperty<string>(c => c.Password);

        public int UserId
		{
			get { return ReadProperty(UserIdProperty); }
			internal set { LoadProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);


        [Execute]
        protected new async Task ExecuteCommand()
        {
                await _passwordValidationCommandDal.ValidateAsync(
                    UserId,
                    Password);
            }


        
    }
}
