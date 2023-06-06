using Csla;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SurgeonPortal.Library.MedicalTraining
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class UserCertificateReadOnly : ReadOnlyBase<UserCertificateReadOnly>, IUserCertificateReadOnly
    {
        [DataMember]
		[DisplayName(nameof(CertificateId))]
        public int CertificateId => ReadProperty(CertificateIdProperty);
		public static readonly PropertyInfo<int> CertificateIdProperty = RegisterProperty<int>(c => c.CertificateId);

        [DataMember]
		[DisplayName(nameof(UserId))]
        public int UserId => ReadProperty(UserIdProperty);
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DataMember]
		[DisplayName(nameof(DocumentId))]
        public int DocumentId => ReadProperty(DocumentIdProperty);
		public static readonly PropertyInfo<int> DocumentIdProperty = RegisterProperty<int>(c => c.DocumentId);

        [DataMember]
		[DisplayName(nameof(CertificateTypeId))]
        public int CertificateTypeId => ReadProperty(CertificateTypeIdProperty);
		public static readonly PropertyInfo<int> CertificateTypeIdProperty = RegisterProperty<int>(c => c.CertificateTypeId);

        [DataMember]
		[DisplayName(nameof(CertificateType))]
        public string CertificateType => ReadProperty(CertificateTypeProperty);
		public static readonly PropertyInfo<string> CertificateTypeProperty = RegisterProperty<string>(c => c.CertificateType);

        [DataMember]
		[DisplayName(nameof(IssueDate))]
        public DateTime IssueDate => ReadProperty(IssueDateProperty);
		public static readonly PropertyInfo<DateTime> IssueDateProperty = RegisterProperty<DateTime>(c => c.IssueDate);

        [DataMember]
		[DisplayName(nameof(CertificateNumber))]
        public string CertificateNumber => ReadProperty(CertificateNumberProperty);
		public static readonly PropertyInfo<string> CertificateNumberProperty = RegisterProperty<string>(c => c.CertificateNumber);

        [DataMember]
		[DisplayName(nameof(DocumentName))]
        public string DocumentName => ReadProperty(DocumentNameProperty);
		public static readonly PropertyInfo<string> DocumentNameProperty = RegisterProperty<string>(c => c.DocumentName);

        [DataMember]
		[DisplayName(nameof(UploadDateUtc))]
        public DateTime UploadDateUtc => ReadProperty(UploadDateUtcProperty);
		public static readonly PropertyInfo<DateTime> UploadDateUtcProperty = RegisterProperty<DateTime>(c => c.UploadDateUtc);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(UserCertificateReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(UserCertificateReadOnlyDto dto)
		{
            LoadProperty(CertificateIdProperty, dto.CertificateId);
            LoadProperty(UserIdProperty, dto.UserId);
            LoadProperty(DocumentIdProperty, dto.DocumentId);
            LoadProperty(CertificateTypeIdProperty, dto.CertificateTypeId);
            LoadProperty(CertificateTypeProperty, dto.CertificateType);
            LoadProperty(IssueDateProperty, dto.IssueDate);
            LoadProperty(CertificateNumberProperty, dto.CertificateNumber);
            LoadProperty(DocumentNameProperty, dto.DocumentName);
            LoadProperty(UploadDateUtcProperty, dto.UploadDateUtc);
		} 
        
    }
}
