using Csla;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Picklists
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ReferenceLetterExplainReadOnly : YtgReadOnlyBase<ReferenceLetterExplainReadOnly, int>, IReferenceLetterExplainReadOnly
    {


        public ReferenceLetterExplainReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(Explain))]
        public string Explain => ReadProperty(ExplainProperty);
		public static readonly PropertyInfo<string> ExplainProperty = RegisterProperty<string>(c => c.Explain);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(ReferenceLetterExplainReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(ReferenceLetterExplainReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(ExplainProperty, dto.Explain);
		} 
        
    }
}
