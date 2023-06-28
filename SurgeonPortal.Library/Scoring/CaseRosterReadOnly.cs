using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class CaseRosterReadOnly : ReadOnlyBase<CaseRosterReadOnly>, ICaseRosterReadOnly
    {
        [DataMember]
		[DisplayName(nameof(CaseNumber))]
        public string CaseNumber => ReadProperty(CaseNumberProperty);
		public static readonly PropertyInfo<string> CaseNumberProperty = RegisterProperty<string>(c => c.CaseNumber);

        [DataMember]
		[DisplayName(nameof(Description))]
        public string Description => ReadProperty(DescriptionProperty);
		public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);

        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(CaseRosterReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(CaseRosterReadOnlyDto dto)
		{
            LoadProperty(CaseNumberProperty, dto.CaseNumber);
            LoadProperty(DescriptionProperty, dto.Description);
            LoadProperty(IdProperty, dto.Id);
		} 
        
    }
}
