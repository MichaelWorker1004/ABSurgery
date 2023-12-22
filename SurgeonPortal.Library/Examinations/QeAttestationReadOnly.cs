using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class QeAttestationReadOnly : YtgReadOnlyBase<QeAttestationReadOnly, int>, IQeAttestationReadOnly
    {


        public QeAttestationReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(AttestationText))]
        public string AttestationText => ReadProperty(AttestationTextProperty);
		public static readonly PropertyInfo<string> AttestationTextProperty = RegisterProperty<string>(c => c.AttestationText);

        [DataMember]
		[DisplayName(nameof(Name))]
        public string Name => ReadProperty(NameProperty);
		public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);

        [DataMember]
		[DisplayName(nameof(Checked))]
        public int Checked => ReadProperty(CheckedProperty);
		public static readonly PropertyInfo<int> CheckedProperty = RegisterProperty<int>(c => c.Checked);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(QeAttestationReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(QeAttestationReadOnlyDto dto)
		{
            LoadProperty(AttestationTextProperty, dto.AttestationText);
            LoadProperty(NameProperty, dto.Name);
            LoadProperty(CheckedProperty, dto.Checked);
		} 
        
    }
}
