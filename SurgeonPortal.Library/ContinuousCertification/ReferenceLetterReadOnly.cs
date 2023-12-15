using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.ContinuousCertification
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ReferenceLetterReadOnly : YtgReadOnlyBase<ReferenceLetterReadOnly, int>, IReferenceLetterReadOnly
    {


        public ReferenceLetterReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(id))]
        public decimal id => ReadProperty(idProperty);
		public static readonly PropertyInfo<decimal> idProperty = RegisterProperty<decimal>(c => c.id);

        [DataMember]
		[DisplayName(nameof(UserId))]
        public int? UserId => ReadProperty(UserIdProperty);
		public static readonly PropertyInfo<int?> UserIdProperty = RegisterProperty<int?>(c => c.UserId);

        [DataMember]
		[DisplayName(nameof(Hosp))]
        public string Hosp => ReadProperty(HospProperty);
		public static readonly PropertyInfo<string> HospProperty = RegisterProperty<string>(c => c.Hosp);

        [DataMember]
		[DisplayName(nameof(Official))]
        public string Official => ReadProperty(OfficialProperty);
		public static readonly PropertyInfo<string> OfficialProperty = RegisterProperty<string>(c => c.Official);

        [DataMember]
		[DisplayName(nameof(Title))]
        public string Title => ReadProperty(TitleProperty);
		public static readonly PropertyInfo<string> TitleProperty = RegisterProperty<string>(c => c.Title);

        [DataMember]
		[DisplayName(nameof(RoleName))]
        public string RoleName => ReadProperty(RoleNameProperty);
		public static readonly PropertyInfo<string> RoleNameProperty = RegisterProperty<string>(c => c.RoleName);

        [DataMember]
		[DisplayName(nameof(AltRoleName))]
        public string AltRoleName => ReadProperty(AltRoleNameProperty);
		public static readonly PropertyInfo<string> AltRoleNameProperty = RegisterProperty<string>(c => c.AltRoleName);

        [DataMember]
		[DisplayName(nameof(RoleId))]
        public int? RoleId => ReadProperty(RoleIdProperty);
		public static readonly PropertyInfo<int?> RoleIdProperty = RegisterProperty<int?>(c => c.RoleId);

        [DataMember]
		[DisplayName(nameof(AltRoleId))]
        public int? AltRoleId => ReadProperty(AltRoleIdProperty);
		public static readonly PropertyInfo<int?> AltRoleIdProperty = RegisterProperty<int?>(c => c.AltRoleId);

        [DataMember]
		[DisplayName(nameof(Explain))]
        public string Explain => ReadProperty(ExplainProperty);
		public static readonly PropertyInfo<string> ExplainProperty = RegisterProperty<string>(c => c.Explain);

        [DataMember]
		[DisplayName(nameof(Email))]
        public string Email => ReadProperty(EmailProperty);
		public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);

        [DataMember]
		[DisplayName(nameof(Phone))]
        public string Phone => ReadProperty(PhoneProperty);
		public static readonly PropertyInfo<string> PhoneProperty = RegisterProperty<string>(c => c.Phone);

        [DataMember]
		[DisplayName(nameof(LetterSent))]
        public DateTime? LetterSent => ReadProperty(LetterSentProperty);
		public static readonly PropertyInfo<DateTime?> LetterSentProperty = RegisterProperty<DateTime?>(c => c.LetterSent);

        [DataMember]
		[DisplayName(nameof(Status))]
        public int Status => ReadProperty(StatusProperty);
		public static readonly PropertyInfo<int> StatusProperty = RegisterProperty<int>(c => c.Status);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(ReferenceLetterReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(ReferenceLetterReadOnlyDto dto)
		{
            LoadProperty(idProperty, dto.Id);
            LoadProperty(UserIdProperty, dto.UserId);
            LoadProperty(HospProperty, dto.Hosp);
            LoadProperty(OfficialProperty, dto.Official);
            LoadProperty(TitleProperty, dto.Title);
            LoadProperty(RoleNameProperty, dto.RoleName);
            LoadProperty(AltRoleNameProperty, dto.AltRoleName);
            LoadProperty(RoleIdProperty, dto.RoleId);
            LoadProperty(AltRoleIdProperty, dto.AltRoleId);
            LoadProperty(ExplainProperty, dto.Explain);
            LoadProperty(EmailProperty, dto.Email);
            LoadProperty(PhoneProperty, dto.Phone);
            LoadProperty(LetterSentProperty, dto.LetterSent);
            LoadProperty(StatusProperty, dto.Status);
		} 
        
    }
}
