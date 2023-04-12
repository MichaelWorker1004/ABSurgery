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
    public class EthnicityReadOnly : ReadOnlyBase<EthnicityReadOnly>, IEthnicityReadOnly
    {
        [DataMember]
		[DisplayName(nameof(ItemValue))]
        public string ItemValue => ReadProperty(ItemValueProperty);
		public static readonly PropertyInfo<string> ItemValueProperty = RegisterProperty<string>(c => c.ItemValue);

        [DataMember]
		[DisplayName(nameof(ItemDescription))]
        public string ItemDescription => ReadProperty(ItemDescriptionProperty);
		public static readonly PropertyInfo<string> ItemDescriptionProperty = RegisterProperty<string>(c => c.ItemDescription);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(EthnicityReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(EthnicityReadOnlyDto dto)
		{
            LoadProperty(ItemValueProperty, dto.ItemValue);
            LoadProperty(ItemDescriptionProperty, dto.ItemDescription);
		} 
        
    }
}
