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
    public class JcahoOrganizationReadOnly : ReadOnlyBase<JcahoOrganizationReadOnly>, IJcahoOrganizationReadOnly
    {
        [DataMember]
		[DisplayName(nameof(OrganizationId))]
        public int? OrganizationId => ReadProperty(OrganizationIdProperty);
		public static readonly PropertyInfo<int?> OrganizationIdProperty = RegisterProperty<int?>(c => c.OrganizationId);

        [DataMember]
		[DisplayName(nameof(OrganizationName))]
        public string OrganizationName => ReadProperty(OrganizationNameProperty);
		public static readonly PropertyInfo<string> OrganizationNameProperty = RegisterProperty<string>(c => c.OrganizationName);


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
		} 
        
    }
}
