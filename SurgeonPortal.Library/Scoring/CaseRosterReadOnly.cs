using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class CaseRosterReadOnly : YtgReadOnlyBase<CaseRosterReadOnly, int>, ICaseRosterReadOnly
    {


        public CaseRosterReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(CaseNumber))]
        public string CaseNumber => ReadProperty(CaseNumberProperty);
		public static readonly PropertyInfo<string> CaseNumberProperty = RegisterProperty<string>(c => c.CaseNumber);

        [DataMember]
		[DisplayName(nameof(Description))]
        public string Description => ReadProperty(DescriptionProperty);
		public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);

        [DataMember]
		[DisplayName(nameof(Title))]
        public string Title => ReadProperty(TitleProperty);
		public static readonly PropertyInfo<string> TitleProperty = RegisterProperty<string>(c => c.Title);

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
            LoadProperty(TitleProperty, dto.Title);
            LoadProperty(IdProperty, dto.Id);
		} 
        
    }
}
