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
    public class OtherCertificationsReadOnly : ReadOnlyBase<OtherCertificationsReadOnly>, IOtherCertificationsReadOnly
    {
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(UserId))]
        public int UserId => ReadProperty(UserIdProperty);
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DataMember]
		[DisplayName(nameof(CertificateTypeId))]
        public int CertificateTypeId => ReadProperty(CertificateTypeIdProperty);
		public static readonly PropertyInfo<int> CertificateTypeIdProperty = RegisterProperty<int>(c => c.CertificateTypeId);

        [DataMember]
		[DisplayName(nameof(CertificateTypeName))]
        public string CertificateTypeName => ReadProperty(CertificateTypeNameProperty);
		public static readonly PropertyInfo<string> CertificateTypeNameProperty = RegisterProperty<string>(c => c.CertificateTypeName);

        [DataMember]
		[DisplayName(nameof(IssueDate))]
        public DateTime IssueDate => ReadProperty(IssueDateProperty);
		public static readonly PropertyInfo<DateTime> IssueDateProperty = RegisterProperty<DateTime>(c => c.IssueDate);

        [DataMember]
		[DisplayName(nameof(CertificateNumber))]
        public string CertificateNumber => ReadProperty(CertificateNumberProperty);
		public static readonly PropertyInfo<string> CertificateNumberProperty = RegisterProperty<string>(c => c.CertificateNumber);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(OtherCertificationsReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(OtherCertificationsReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(UserIdProperty, dto.UserId);
            LoadProperty(CertificateTypeIdProperty, dto.CertificateTypeId);
            LoadProperty(CertificateTypeNameProperty, dto.CertificateTypeName);
            LoadProperty(IssueDateProperty, dto.IssueDate);
            LoadProperty(CertificateNumberProperty, dto.CertificateNumber);
		} 
        
    }
}
