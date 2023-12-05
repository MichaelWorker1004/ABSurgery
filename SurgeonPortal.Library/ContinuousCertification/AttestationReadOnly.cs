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
    public class AttestationReadOnly : YtgReadOnlyBase<AttestationReadOnly, int>, IAttestationReadOnly
    {


        public AttestationReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(Label))]
        public string Label => ReadProperty(LabelProperty);
		public static readonly PropertyInfo<string> LabelProperty = RegisterProperty<string>(c => c.Label);

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
        private void Child_Fetch(AttestationReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(AttestationReadOnlyDto dto)
		{
            LoadProperty(LabelProperty, dto.Label);
            LoadProperty(NameProperty, dto.Name);
            LoadProperty(CheckedProperty, dto.Checked);
		} 
        
    }
}
