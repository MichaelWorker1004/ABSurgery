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
    public class JcahoOrganizationReadOnly : YtgReadOnlyBase<JcahoOrganizationReadOnly, int>, IJcahoOrganizationReadOnly
    {


        public JcahoOrganizationReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(OrganizationId))]
        public int OrganizationId => ReadProperty(OrganizationIdProperty);
		public static readonly PropertyInfo<int> OrganizationIdProperty = RegisterProperty<int>(c => c.OrganizationId);

        [DataMember]
		[DisplayName(nameof(OrganizationName))]
        public string OrganizationName => ReadProperty(OrganizationNameProperty);
		public static readonly PropertyInfo<string> OrganizationNameProperty = RegisterProperty<string>(c => c.OrganizationName);

        [DataMember]
		[DisplayName(nameof(StateCode))]
        public string StateCode => ReadProperty(StateCodeProperty);
		public static readonly PropertyInfo<string> StateCodeProperty = RegisterProperty<string>(c => c.StateCode);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(JcahoOrganizationReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(JcahoOrganizationReadOnlyDto dto)
		{
            LoadProperty(OrganizationIdProperty, dto.OrganizationId);
            LoadProperty(OrganizationNameProperty, dto.OrganizationName);
            LoadProperty(StateCodeProperty, dto.StateCode);
		} 
        
    }
}
