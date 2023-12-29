using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using System;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using SurgeonPortal.Library.Email;
using SurgeonPortal.Library.Contracts.Email;
using SurgeonPortal.Shared.Users;
using Microsoft.Extensions.Options;


namespace SurgeonPortal.Library.Users
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class CreateForgotPasswordCommand : YtgCommandBase<CreateForgotPasswordCommand, int>, ICreateForgotPasswordCommand
    {
        private readonly ICreateForgotPasswordCommandDal _createForgotPasswordCommandDal;
        private readonly IEmailFactory _emailFactory;
        private readonly UsersConfiguration _usersConfiguration;

        public CreateForgotPasswordCommand(
            IIdentityProvider identityProvider,
            ICreateForgotPasswordCommandDal createForgotPasswordCommandDal,
            IEmailFactory emailFactory,
            IOptions<UsersConfiguration> usersConfiguration)
            : base(identityProvider)
        {
            _createForgotPasswordCommandDal = createForgotPasswordCommandDal;
            _emailFactory = emailFactory;
            _usersConfiguration = usersConfiguration.Value;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        public string UserName
		{
			get { return ReadProperty(UserNameProperty); }
			internal set { LoadProperty(UserNameProperty, value); }
		}
		public static readonly PropertyInfo<string> UserNameProperty = RegisterProperty<string>(c => c.UserName);

        public Guid? ResetGUID
		{
			get { return ReadProperty(ResetGUIDProperty); }
			internal set { LoadProperty(ResetGUIDProperty, value); }
		}
		public static readonly PropertyInfo<Guid?> ResetGUIDProperty = RegisterProperty<Guid?>(c => c.ResetGUID);

        public string EmailAddress
		{
			get { return ReadProperty(EmailAddressProperty); }
			internal set { LoadProperty(EmailAddressProperty, value); }
		}
		public static readonly PropertyInfo<string> EmailAddressProperty = RegisterProperty<string>(c => c.EmailAddress);

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
            var dto = await _createForgotPasswordCommandDal.SendForgotPasswordEmailAsync(UserName);

            this.ResetGUID = dto.ResetGUID;
            this.EmailAddress = dto.EmailAddress;
            this.FirstName = dto.FirstName;
            this.LastName = dto.LastName;

            if(ResetGUID != null)
            {
                await SendEmail();
            }
        }

        private async Task SendEmail()
        {
            var email = _emailFactory.Create();

            var url = $"{_usersConfiguration.ForgotPasswordUrl}{ResetGUID}";

            email.To = EmailAddress;
            email.TemplateId = _usersConfiguration.ForgotPasswordTempalteId;
            email.TemplateData = new
            {
                first_name = FirstName,
                last_name = LastName,
                url
            };

            await email.SendAsync();
        }

        
    }
}
