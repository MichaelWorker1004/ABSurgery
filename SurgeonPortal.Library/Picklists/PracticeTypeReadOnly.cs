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
    public class PracticeTypeReadOnly : ReadOnlyBase<PracticeTypeReadOnly>, IPracticeTypeReadOnly
    {
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(Name))]
        public string Name => ReadProperty(NameProperty);
		public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(PracticeTypeReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(PracticeTypeReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(NameProperty, dto.Name);
		} 
        
    }
}
