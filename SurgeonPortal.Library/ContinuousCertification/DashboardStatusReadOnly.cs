using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.ContinuousCertification
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class DashboardStatusReadOnly : YtgReadOnlyBase<DashboardStatusReadOnly, int>, IDashboardStatusReadOnly
    {


        public DashboardStatusReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(StatusType))]
        public string StatusType => ReadProperty(StatusTypeProperty);
		public static readonly PropertyInfo<string> StatusTypeProperty = RegisterProperty<string>(c => c.StatusType);

        [DataMember]
		[DisplayName(nameof(Status))]
        public int? Status => ReadProperty(StatusProperty);
		public static readonly PropertyInfo<int?> StatusProperty = RegisterProperty<int?>(c => c.Status);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(DashboardStatusReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(DashboardStatusReadOnlyDto dto)
		{
            LoadProperty(StatusTypeProperty, dto.StatusType);
            LoadProperty(StatusProperty, dto.Status);
		} 
        
    }
}
