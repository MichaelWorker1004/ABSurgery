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
    public class DashboardRosterReadOnly : YtgReadOnlyBase<DashboardRosterReadOnly, int>, IDashboardRosterReadOnly
    {


        public DashboardRosterReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(FirstName))]
        public string FirstName => ReadProperty(FirstNameProperty);
		public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);

        [DataMember]
		[DisplayName(nameof(MiddleName))]
        public string MiddleName => ReadProperty(MiddleNameProperty);
		public static readonly PropertyInfo<string> MiddleNameProperty = RegisterProperty<string>(c => c.MiddleName);

        [DataMember]
		[DisplayName(nameof(LastName))]
        public string LastName => ReadProperty(LastNameProperty);
		public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);

        [DataMember]
		[DisplayName(nameof(SessionNumber))]
        public int? SessionNumber => ReadProperty(SessionNumberProperty);
		public static readonly PropertyInfo<int?> SessionNumberProperty = RegisterProperty<int?>(c => c.SessionNumber);

        [DataMember]
		[DisplayName(nameof(StartTime))]
        public TimeSpan StartTime => ReadProperty(StartTimeProperty);
		public static readonly PropertyInfo<TimeSpan> StartTimeProperty = RegisterProperty<TimeSpan>(c => c.StartTime);

        [DataMember]
		[DisplayName(nameof(EndTime))]
        public TimeSpan EndTime => ReadProperty(EndTimeProperty);
		public static readonly PropertyInfo<TimeSpan> EndTimeProperty = RegisterProperty<TimeSpan>(c => c.EndTime);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(DashboardRosterReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(DashboardRosterReadOnlyDto dto)
		{
            LoadProperty(FirstNameProperty, dto.FirstName);
            LoadProperty(MiddleNameProperty, dto.MiddleName);
            LoadProperty(LastNameProperty, dto.LastName);
            LoadProperty(SessionNumberProperty, dto.SessionNumber);
            LoadProperty(StartTimeProperty, dto.StartTime);
            LoadProperty(EndTimeProperty, dto.EndTime);
		} 
        
    }
}
