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
    public class ReferenceLetterTypeReadOnly : YtgReadOnlyBase<ReferenceLetterTypeReadOnly, int>, IReferenceLetterTypeReadOnly
    {


        public ReferenceLetterTypeReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(Role))]
        public string Role => ReadProperty(RoleProperty);
		public static readonly PropertyInfo<string> RoleProperty = RegisterProperty<string>(c => c.Role);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(ReferenceLetterTypeReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(ReferenceLetterTypeReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(RoleProperty, dto.Role);
		} 
        
    }
}
