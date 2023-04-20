using Csla;
using SurgeonPortal.DataAccess.Contracts.Surgeons;
using SurgeonPortal.Library.Contracts.Surgeons;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SurgeonPortal.Library.Surgeons
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class CertificationReadOnly : ReadOnlyBase<CertificationReadOnly>, ICertificationReadOnly
    {
        [DataMember]
		[DisplayName(nameof(Speciality))]
        public string Speciality => ReadProperty(SpecialityProperty);
		public static readonly PropertyInfo<string> SpecialityProperty = RegisterProperty<string>(c => c.Speciality);

        [DataMember]
		[DisplayName(nameof(CertificateId))]
        public string CertificateId => ReadProperty(CertificateIdProperty);
		public static readonly PropertyInfo<string> CertificateIdProperty = RegisterProperty<string>(c => c.CertificateId);

        [DataMember]
		[DisplayName(nameof(InitialCertificationDate))]
        public string InitialCertificationDate => ReadProperty(InitialCertificationDateProperty);
		public static readonly PropertyInfo<string> InitialCertificationDateProperty = RegisterProperty<string>(c => c.InitialCertificationDate);

        [DataMember]
		[DisplayName(nameof(EndDateDisplay))]
        public string EndDateDisplay => ReadProperty(EndDateDisplayProperty);
		public static readonly PropertyInfo<string> EndDateDisplayProperty = RegisterProperty<string>(c => c.EndDateDisplay);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(CertificationReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(CertificationReadOnlyDto dto)
		{
            LoadProperty(SpecialityProperty, dto.Speciality);
            LoadProperty(CertificateIdProperty, dto.CertificateId);
            LoadProperty(InitialCertificationDateProperty, dto.InitialCertificationDate);
            LoadProperty(EndDateDisplayProperty, dto.EndDateDisplay);
		} 
        
    }
}
