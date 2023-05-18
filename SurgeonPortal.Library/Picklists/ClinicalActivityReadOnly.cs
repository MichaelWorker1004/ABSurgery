using Csla;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SurgeonPortal.Library.Picklists
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ClinicalActivityReadOnly : ReadOnlyBase<ClinicalActivityReadOnly>, IClinicalActivityReadOnly
    {
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(Name))]
        public string Name => ReadProperty(NameProperty);
		public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);

        [DataMember]
		[DisplayName(nameof(IsCredit))]
        public bool IsCredit => ReadProperty(IsCreditProperty);
		public static readonly PropertyInfo<bool> IsCreditProperty = RegisterProperty<bool>(c => c.IsCredit);

        [DataMember]
		[DisplayName(nameof(IsEssential))]
        public bool IsEssential => ReadProperty(IsEssentialProperty);
		public static readonly PropertyInfo<bool> IsEssentialProperty = RegisterProperty<bool>(c => c.IsEssential);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(ClinicalActivityReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(ClinicalActivityReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(NameProperty, dto.Name);
            LoadProperty(IsCreditProperty, dto.IsCredit);
            LoadProperty(IsEssentialProperty, dto.IsEssential);
		} 
        
    }
}
