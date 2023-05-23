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
    public class DegreeReadOnly : ReadOnlyBase<DegreeReadOnly>, IDegreeReadOnly
    {
        [DataMember]
		[DisplayName(nameof(ItemDisplay))]
        public string ItemDisplay => ReadProperty(ItemDisplayProperty);
		public static readonly PropertyInfo<string> ItemDisplayProperty = RegisterProperty<string>(c => c.ItemDisplay);

        [DataMember]
		[DisplayName(nameof(ItemValue))]
        public int ItemValue => ReadProperty(ItemValueProperty);
		public static readonly PropertyInfo<int> ItemValueProperty = RegisterProperty<int>(c => c.ItemValue);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(DegreeReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(DegreeReadOnlyDto dto)
		{
            LoadProperty(ItemDisplayProperty, dto.ItemDisplay);
            LoadProperty(ItemValueProperty, dto.ItemValue);
		} 
        
    }
}
