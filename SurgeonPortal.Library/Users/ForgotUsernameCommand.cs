using System;
using System.Threading.Tasks;
using Csla;
using Microsoft.Extensions.Options;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Email;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Shared.Users;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;


namespace SurgeonPortal.Library.Users
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class ForgotUsernameCommand : YtgCommandBase<ForgotUsernameCommand, int>, IForgotUsernameCommand
    {
        private readonly IForgotUsernameCommandDal _forgotUsernameCommandDal;
        private readonly IEmailFactory _emailFactory;
        private readonly UsersConfiguration _usersConfiguration;

        public ForgotUsernameCommand(
            IIdentityProvider identityProvider,
            IForgotUsernameCommandDal forgotUsernameCommandDal,
            IEmailFactory emailFactory,
            IOptions<UsersConfiguration> usersConfiguration)
            : base(identityProvider)
        {
            _forgotUsernameCommandDal = forgotUsernameCommandDal;
            _emailFactory = emailFactory;
            _usersConfiguration = usersConfiguration.Value;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        public string Email
		{
			get { return ReadProperty(EmailProperty); }
			internal set { LoadProperty(EmailProperty, value); }
		}
		public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);

        public string UserName
		{
			get { return ReadProperty(UserNameProperty); }
			internal set { LoadProperty(UserNameProperty, value); }
		}
		public static readonly PropertyInfo<string> UserNameProperty = RegisterProperty<string>(c => c.UserName);

        public string FirstName
		{
			get { return ReadProperty(FirstNameProperty); }
			internal set { LoadProperty(FirstNameProperty, value); }
		}
		public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);

        public string LastName
		{
			get { return ReadProperty(LastNameProperty); }
			internal set { LoadProperty(LastNameProperty, value); }
		}
		public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);


        [Execute]
        protected async Task ExecuteCommand()
        {
            var dto = await _forgotUsernameCommandDal.SendForgotUsernameEmailAsync(Email);

            this.UserName = dto?.UserName;
            this.FirstName = dto?.FirstName;
            this.LastName = dto?.LastName;

            if (!string.IsNullOrWhiteSpace(UserName))
            {
                await SendEmail();
            }
        }

        private async Task SendEmail()
        {
            var email = _emailFactory.Create();

            email.To = Email;
            email.TemplateId = _usersConfiguration.ForgotUsernameTemplateId;
            email.TemplateData = new
            {
                first_name = FirstName,
                last_name = LastName,
                username = UserName
            };

            await email.SendAsync();
        }


        
    }
}
