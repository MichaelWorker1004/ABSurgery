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
    public class FellowshipTypeReadOnly : YtgReadOnlyBase<FellowshipTypeReadOnly, int>, IFellowshipTypeReadOnly
    {


        public FellowshipTypeReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(FellowshipType))]
        public string FellowshipType => ReadProperty(FellowshipTypeProperty);
		public static readonly PropertyInfo<string> FellowshipTypeProperty = RegisterProperty<string>(c => c.FellowshipType);

        [DataMember]
		[DisplayName(nameof(FellowshipTypeName))]
        public string FellowshipTypeName => ReadProperty(FellowshipTypeNameProperty);
		public static readonly PropertyInfo<string> FellowshipTypeNameProperty = RegisterProperty<string>(c => c.FellowshipTypeName);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(FellowshipTypeReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(FellowshipTypeReadOnlyDto dto)
		{
            LoadProperty(FellowshipTypeProperty, dto.FellowshipType);
            LoadProperty(FellowshipTypeNameProperty, dto.FellowshipTypeName);
		} 
        
    }
}
