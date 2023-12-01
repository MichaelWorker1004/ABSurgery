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
		[DisplayName(nameof(label))]
        public string label => ReadProperty(labelProperty);
		public static readonly PropertyInfo<string> labelProperty = RegisterProperty<string>(c => c.label);

        [DataMember]
		[DisplayName(nameof(name))]
        public string name => ReadProperty(nameProperty);
		public static readonly PropertyInfo<string> nameProperty = RegisterProperty<string>(c => c.name);

        [DataMember]
		[DisplayName(nameof(checked))]
        public int checked => ReadProperty(checkedProperty);
		public static readonly PropertyInfo<int> checkedProperty = RegisterProperty<int>(c => c.checked);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(AttestationReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(AttestationReadOnlyDto dto)
		{
            LoadProperty(labelProperty, dto.Label);
            LoadProperty(nameProperty, dto.Name);
            LoadProperty(checkedProperty, dto.Checked);
		} 
        
    }
}
