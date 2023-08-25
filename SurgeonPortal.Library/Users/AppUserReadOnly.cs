using Csla;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;
using static SurgeonPortal.Library.Users.AppUserReadOnlyFactory;

namespace SurgeonPortal.Library.Users
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class AppUserReadOnly : ReadOnlyBase<AppUserReadOnly>, IAppUserReadOnly
    {
        private readonly IAppUserReadOnlyDal _appUserReadOnlyDal;


        public AppUserReadOnly(IAppUserReadOnlyDal appUserReadOnlyDal)
        {
            _appUserReadOnlyDal = appUserReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(UserId))]
        public int UserId => ReadProperty(UserIdProperty);
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DataMember]
		[DisplayName(nameof(FullName))]
        public string FullName => ReadProperty(FullNameProperty);
		public static readonly PropertyInfo<string> FullNameProperty = RegisterProperty<string>(c => c.FullName);

        [DataMember]
		[DisplayName(nameof(UserName))]
        public string UserName => ReadProperty(UserNameProperty);
		public static readonly PropertyInfo<string> UserNameProperty = RegisterProperty<string>(c => c.UserName);

        [DataMember]
		[DisplayName(nameof(EmailAddress))]
        public string EmailAddress => ReadProperty(EmailAddressProperty);
		public static readonly PropertyInfo<string> EmailAddressProperty = RegisterProperty<string>(c => c.EmailAddress);

        [DataMember]
		[DisplayName(nameof(LastLoginDateUtc))]
        public DateTime? LastLoginDateUtc => ReadProperty(LastLoginDateUtcProperty);
		public static readonly PropertyInfo<DateTime?> LastLoginDateUtcProperty = RegisterProperty<DateTime?>(c => c.LastLoginDateUtc);

        public static readonly PropertyInfo<UserClaimReadOnlyList> ClaimsProperty =
            RegisterProperty<UserClaimReadOnlyList>(c => c.Claims);
        public IUserClaimReadOnlyList Claims
        {
            get => GetProperty(ClaimsProperty);
            private set => LoadProperty(ClaimsProperty, value);
        }

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
        private async Task GetByCredentials(GetByCredentialsCriteria criteria)
        
        {
            var dto = await _appUserReadOnlyDal.GetByCredentialsAsync(
                criteria.UserName,
                criteria.Password);
            
            if (dto == null)
            {
                throw new AuthenticationFailedException("User login failed");
            }
            
            FetchData(dto);
        }
        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByToken(GetByTokenCriteria criteria)
        
        {
            var dto = await _appUserReadOnlyDal.GetByTokenAsync(criteria.Token);
            
            if (dto == null)
            {
                throw new AuthenticationFailedException("AppUserReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(AppUserReadOnlyDto dto)
		{
            LoadProperty(UserIdProperty, dto.UserId);
            LoadProperty(FullNameProperty, dto.FullName);
            LoadProperty(UserNameProperty, dto.UserName);
            LoadProperty(EmailAddressProperty, dto.EmailAddress);
            LoadProperty(LastLoginDateUtcProperty, dto.LastLoginDateUtc);

            LoadProperty(ClaimsProperty, DataPortal.FetchChild<UserClaimReadOnlyList>(dto.Claims));
		} 
        
    }
}
