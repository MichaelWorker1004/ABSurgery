using Csla;
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

        public UserCredential(
            IIdentityProvider identityProvider,
            IUserCredentialDal userCredentialDal)
            : base(identityProvider)
        {
            _userCredentialDal = userCredentialDal;
        }

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




        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId(GetByUserIdCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                var dto = await _userCredentialDal.GetByUserIdAsync();
        
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
            
			dto.EmailAddress = this.EmailAddress;
			dto.Password = this.Password;

			return dto;
		}


    }
}
