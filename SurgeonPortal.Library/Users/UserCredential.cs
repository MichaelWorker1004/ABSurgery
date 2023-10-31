using Csla;
using Csla.Rules.CommonRules;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Users.UserCredentialFactory;

namespace SurgeonPortal.Library.Users
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class UserCredential : YtgBusinessBase<UserCredential>, IUserCredential
    {
        private readonly IUserCredentialDal _userCredentialDal;
        private readonly IPasswordValidationCommandFactory _passwordValidationCommandFactory;

        public UserCredential(
            IIdentityProvider identityProvider,
            IUserCredentialDal userCredentialDal,
            IPasswordValidationCommandFactory passwordValidationCommandFactory)
            : base(identityProvider)
        {
            _userCredentialDal = userCredentialDal;
            _passwordValidationCommandFactory = passwordValidationCommandFactory;
            InitializeInjectionDependentRules();
        }

        [Key] 
        [DisplayName(nameof(UserId))]
		public int UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DisplayName(nameof(EmailAddress))]
		public string EmailAddress
		{
			get { return GetProperty(EmailAddressProperty); }
			set { SetProperty(EmailAddressProperty, value); }
		}
		public static readonly PropertyInfo<string> EmailAddressProperty = RegisterProperty<string>(c => c.EmailAddress);

        [DisplayName(nameof(Password))]
		public string Password
		{
			get { return GetProperty(PasswordProperty); }
			set { SetProperty(PasswordProperty, value); }
		}
		public static readonly PropertyInfo<string> PasswordProperty = RegisterProperty<string>(c => c.Password);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            
            
        }

        /// <summary>
        /// This method is used to add business rules to the Csla 
        /// business rule engine
        /// </summary>
        protected override void AddBusinessRules()
        {
            // Only process priority 5 and higher if all 4 and lower completed first
            BusinessRules.ProcessThroughPriority = 4;

            BusinessRules.AddRule(new RegExMatch(EmailAddressProperty, @"^$|^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", @"The email address must be a valid format"));
            BusinessRules.AddRule(new RegExMatch(PasswordProperty, @"^$|^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", @"The password does not meet the minimum requirements.  Passwords must be a minimum length of 8 characters, at least one uppercase letter, one lowercase letter, one digit, and one special character"));
            BusinessRules.AddRule(new EitherOrRequiredRule(PasswordProperty, EmailAddressProperty, 1));
        }
        private void InitializeInjectionDependentRules()
        {
            BusinessRules.AddRule(new PasswordMatchesCurrentRule(_passwordValidationCommandFactory, PasswordProperty, UserIdProperty, 2));
        }


        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _userCredentialDal.GetByUserIdAsync(_identity.GetUserId<int>());
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("UserCredential not found based on criteria");
                }
                FetchData(dto);
            }
        }

        [RunLocal]
        [Update]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task Update()
        {
            base.DataPortal_Update();
        
            using (BypassPropertyChecks)
            {
                var dto = await _userCredentialDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(UserCredentialDto dto)
		{
            base.FetchData(dto);
            
			this.UserId = dto.UserId;
			this.EmailAddress = dto.EmailAddress;
			this.Password = dto.Password;
		}

		internal UserCredentialDto ToDto()
		{
			var dto = new UserCredentialDto();
			return ToDto(dto);
		}
		internal UserCredentialDto ToDto(UserCredentialDto dto)
		{
            base.ToDto(dto);
            
			dto.UserId = this.UserId;
			dto.EmailAddress = this.EmailAddress;
			dto.Password = this.Password;

			return dto;
		}


    }
}
