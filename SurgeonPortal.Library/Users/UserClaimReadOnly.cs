using Csla;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Users
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class UserClaimReadOnly : YtgReadOnlyBase<UserClaimReadOnly, int>, IUserClaimReadOnly
    {


        public UserClaimReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(ClaimName))]
        public string ClaimName => ReadProperty(ClaimNameProperty);
		public static readonly PropertyInfo<string> ClaimNameProperty = RegisterProperty<string>(c => c.ClaimName);

        [DataMember]
		[DisplayName(nameof(ApplicationId))]
        public int ApplicationId => ReadProperty(ApplicationIdProperty);
		public static readonly PropertyInfo<int> ApplicationIdProperty = RegisterProperty<int>(c => c.ApplicationId);

        [DataMember]
		[DisplayName(nameof(ApplicationName))]
        public string ApplicationName => ReadProperty(ApplicationNameProperty);
		public static readonly PropertyInfo<string> ApplicationNameProperty = RegisterProperty<string>(c => c.ApplicationName);

        [DataMember]
		[DisplayName(nameof(UserClaimId))]
        public int UserClaimId => ReadProperty(UserClaimIdProperty);
		public static readonly PropertyInfo<int> UserClaimIdProperty = RegisterProperty<int>(c => c.UserClaimId);

        [DataMember]
		[DisplayName(nameof(ApplicationClaimId))]
        public int ApplicationClaimId => ReadProperty(ApplicationClaimIdProperty);
		public static readonly PropertyInfo<int> ApplicationClaimIdProperty = RegisterProperty<int>(c => c.ApplicationClaimId);

        [DataMember]
		[DisplayName(nameof(UserId))]
        public int UserId => ReadProperty(UserIdProperty);
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(UserClaimReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(UserClaimReadOnlyDto dto)
		{
            LoadProperty(ClaimNameProperty, dto.ClaimName);
            LoadProperty(ApplicationIdProperty, dto.ApplicationId);
            LoadProperty(ApplicationNameProperty, dto.ApplicationName);
            LoadProperty(UserClaimIdProperty, dto.UserClaimId);
            LoadProperty(ApplicationClaimIdProperty, dto.ApplicationClaimId);
            LoadProperty(UserIdProperty, dto.UserId);
		} 
        
    }
}
