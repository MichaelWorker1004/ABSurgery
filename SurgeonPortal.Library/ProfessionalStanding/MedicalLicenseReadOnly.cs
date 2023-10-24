using Csla;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class MedicalLicenseReadOnly : YtgReadOnlyBase<MedicalLicenseReadOnly, int>, IMedicalLicenseReadOnly
    {


        public MedicalLicenseReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(LicenseId))]
        public decimal LicenseId => ReadProperty(LicenseIdProperty);
		public static readonly PropertyInfo<decimal> LicenseIdProperty = RegisterProperty<decimal>(c => c.LicenseId);

        [DataMember]
		[DisplayName(nameof(UserId))]
        public int? UserId => ReadProperty(UserIdProperty);
		public static readonly PropertyInfo<int?> UserIdProperty = RegisterProperty<int?>(c => c.UserId);

        [DataMember]
		[DisplayName(nameof(IssuingStateId))]
        public string IssuingStateId => ReadProperty(IssuingStateIdProperty);
		public static readonly PropertyInfo<string> IssuingStateIdProperty = RegisterProperty<string>(c => c.IssuingStateId);

        [DataMember]
		[DisplayName(nameof(IssuingState))]
        public string IssuingState => ReadProperty(IssuingStateProperty);
		public static readonly PropertyInfo<string> IssuingStateProperty = RegisterProperty<string>(c => c.IssuingState);

        [DataMember]
		[DisplayName(nameof(LicenseNumber))]
        public string LicenseNumber => ReadProperty(LicenseNumberProperty);
		public static readonly PropertyInfo<string> LicenseNumberProperty = RegisterProperty<string>(c => c.LicenseNumber);

        [DataMember]
		[DisplayName(nameof(LicenseTypeId))]
        public int? LicenseTypeId => ReadProperty(LicenseTypeIdProperty);
		public static readonly PropertyInfo<int?> LicenseTypeIdProperty = RegisterProperty<int?>(c => c.LicenseTypeId);

        [DataMember]
		[DisplayName(nameof(LicenseType))]
        public string LicenseType => ReadProperty(LicenseTypeProperty);
		public static readonly PropertyInfo<string> LicenseTypeProperty = RegisterProperty<string>(c => c.LicenseType);

        [DataMember]
		[DisplayName(nameof(IssueDate))]
        public DateTime? IssueDate => ReadProperty(IssueDateProperty);
		public static readonly PropertyInfo<DateTime?> IssueDateProperty = RegisterProperty<DateTime?>(c => c.IssueDate);

        [DataMember]
		[DisplayName(nameof(ExpireDate))]
        public DateTime? ExpireDate => ReadProperty(ExpireDateProperty);
		public static readonly PropertyInfo<DateTime?> ExpireDateProperty = RegisterProperty<DateTime?>(c => c.ExpireDate);

        [DataMember]
		[DisplayName(nameof(ReportingOrganization))]
        public string ReportingOrganization => ReadProperty(ReportingOrganizationProperty);
		public static readonly PropertyInfo<string> ReportingOrganizationProperty = RegisterProperty<string>(c => c.ReportingOrganization);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(MedicalLicenseReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(MedicalLicenseReadOnlyDto dto)
		{
            LoadProperty(LicenseIdProperty, dto.LicenseId);
            LoadProperty(UserIdProperty, dto.UserId);
            LoadProperty(IssuingStateIdProperty, dto.IssuingStateId);
            LoadProperty(IssuingStateProperty, dto.IssuingState);
            LoadProperty(LicenseNumberProperty, dto.LicenseNumber);
            LoadProperty(LicenseTypeIdProperty, dto.LicenseTypeId);
            LoadProperty(LicenseTypeProperty, dto.LicenseType);
            LoadProperty(IssueDateProperty, dto.IssueDate);
            LoadProperty(ExpireDateProperty, dto.ExpireDate);
            LoadProperty(ReportingOrganizationProperty, dto.ReportingOrganization);
		} 
        
    }
}
