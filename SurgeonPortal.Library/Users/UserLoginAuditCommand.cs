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
    public class UserLoginAuditCommand : CommandBase<UserLoginAuditCommand>, IUserLoginAuditCommand
    {
        private readonly IUserLoginAuditCommandDal _userLoginAuditCommandDal;


        public UserLoginAuditCommand(IUserLoginAuditCommandDal userLoginAuditCommandDal)
        {
            _userLoginAuditCommandDal = userLoginAuditCommandDal;
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

        public string EmailAddress
		{
			get { return ReadProperty(EmailAddressProperty); }
			internal set { LoadProperty(EmailAddressProperty, value); }
		}
		public static readonly PropertyInfo<string> EmailAddressProperty = RegisterProperty<string>(c => c.EmailAddress);

        public int ApplicationId
		{
			get { return ReadProperty(ApplicationIdProperty); }
			internal set { LoadProperty(ApplicationIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ApplicationIdProperty = RegisterProperty<int>(c => c.ApplicationId);

        public string LoginIpAddress
		{
			get { return ReadProperty(LoginIpAddressProperty); }
			internal set { LoadProperty(LoginIpAddressProperty, value); }
		}
		public static readonly PropertyInfo<string> LoginIpAddressProperty = RegisterProperty<string>(c => c.LoginIpAddress);

        public string LoginUserAgent
		{
			get { return ReadProperty(LoginUserAgentProperty); }
			internal set { LoadProperty(LoginUserAgentProperty, value); }
		}
		public static readonly PropertyInfo<string> LoginUserAgentProperty = RegisterProperty<string>(c => c.LoginUserAgent);

        public bool LoginSuccess
		{
			get { return ReadProperty(LoginSuccessProperty); }
			internal set { LoadProperty(LoginSuccessProperty, value); }
		}
		public static readonly PropertyInfo<bool> LoginSuccessProperty = RegisterProperty<bool>(c => c.LoginSuccess);

        public string LoginFailureReason
		{
			get { return ReadProperty(LoginFailureReasonProperty); }
			internal set { LoadProperty(LoginFailureReasonProperty, value); }
		}
		public static readonly PropertyInfo<string> LoginFailureReasonProperty = RegisterProperty<string>(c => c.LoginFailureReason);


        [Execute]
        protected new async Task ExecuteCommand()
        {
                await _userLoginAuditCommandDal.AuditAsync(
                    UserId,
                    EmailAddress,
                    ApplicationId,
                    LoginIpAddress,
                    LoginUserAgent,
                    LoginSuccess,
                    LoginFailureReason);
            }


        
    }
}
