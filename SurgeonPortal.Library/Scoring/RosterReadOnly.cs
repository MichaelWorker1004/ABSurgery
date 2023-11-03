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
    public class RosterReadOnly : YtgReadOnlyBase<RosterReadOnly, int>, IRosterReadOnly
    {


        public RosterReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(ExamScheduleId))]
        public int ExamScheduleId => ReadProperty(ExamScheduleIdProperty);
		public static readonly PropertyInfo<int> ExamScheduleIdProperty = RegisterProperty<int>(c => c.ExamScheduleId);

        [DataMember]
		[DisplayName(nameof(DayNumber))]
        public int? DayNumber => ReadProperty(DayNumberProperty);
		public static readonly PropertyInfo<int?> DayNumberProperty = RegisterProperty<int?>(c => c.DayNumber);

        [DataMember]
		[DisplayName(nameof(SessionNumber))]
        public int? SessionNumber => ReadProperty(SessionNumberProperty);
		public static readonly PropertyInfo<int?> SessionNumberProperty = RegisterProperty<int?>(c => c.SessionNumber);

        [DataMember]
		[DisplayName(nameof(Roster))]
        public string Roster => ReadProperty(RosterProperty);
		public static readonly PropertyInfo<string> RosterProperty = RegisterProperty<string>(c => c.Roster);

        [DataMember]
		[DisplayName(nameof(DisplayName))]
        public string DisplayName => ReadProperty(DisplayNameProperty);
		public static readonly PropertyInfo<string> DisplayNameProperty = RegisterProperty<string>(c => c.DisplayName);

        [DataMember]
		[DisplayName(nameof(IsSubmitted))]
        public bool? IsSubmitted => ReadProperty(IsSubmittedProperty);
		public static readonly PropertyInfo<bool?> IsSubmittedProperty = RegisterProperty<bool?>(c => c.IsSubmitted);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(RosterReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(RosterReadOnlyDto dto)
		{
            LoadProperty(ExamScheduleIdProperty, dto.ExamScheduleId);
            LoadProperty(DayNumberProperty, dto.DayNumber);
            LoadProperty(SessionNumberProperty, dto.SessionNumber);
            LoadProperty(RosterProperty, dto.Roster);
            LoadProperty(DisplayNameProperty, dto.DisplayName);
            LoadProperty(IsSubmittedProperty, dto.IsSubmitted);
		} 
        
    }
}
