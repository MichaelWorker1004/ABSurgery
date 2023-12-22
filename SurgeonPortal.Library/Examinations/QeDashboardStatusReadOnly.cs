using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class QeDashboardStatusReadOnly : YtgReadOnlyBase<QeDashboardStatusReadOnly, int>, IQeDashboardStatusReadOnly
    {


        public QeDashboardStatusReadOnly(IIdentityProvider identityProvider)
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

        [DataMember]
		[DisplayName(nameof(Disabled))]
        public int Disabled => ReadProperty(DisabledProperty);
		public static readonly PropertyInfo<int> DisabledProperty = RegisterProperty<int>(c => c.Disabled);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(QeDashboardStatusReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(QeDashboardStatusReadOnlyDto dto)
		{
            LoadProperty(StatusTypeProperty, dto.StatusType);
            LoadProperty(StatusProperty, dto.Status);
            LoadProperty(DisabledProperty, dto.Disabled);
		} 
        
    }
}
