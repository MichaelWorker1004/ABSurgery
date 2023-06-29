using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class RosterReadOnly : ReadOnlyBase<RosterReadOnly>, IRosterReadOnly
    {
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
		[DisplayName(nameof(Score))]
        public string Score => ReadProperty(ScoreProperty);
		public static readonly PropertyInfo<string> ScoreProperty = RegisterProperty<string>(c => c.Score);

        [DataMember]
		[DisplayName(nameof(CriticalFail))]
        public string CriticalFail => ReadProperty(CriticalFailProperty);
		public static readonly PropertyInfo<string> CriticalFailProperty = RegisterProperty<string>(c => c.CriticalFail);

        [DataMember]
		[DisplayName(nameof(Submitted))]
        public string Submitted => ReadProperty(SubmittedProperty);
		public static readonly PropertyInfo<string> SubmittedProperty = RegisterProperty<string>(c => c.Submitted);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(RosterReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(RosterReadOnlyDto dto)
		{
            LoadProperty(DayNumberProperty, dto.DayNumber);
            LoadProperty(SessionNumberProperty, dto.SessionNumber);
            LoadProperty(RosterProperty, dto.Roster);
            LoadProperty(DisplayNameProperty, dto.DisplayName);
            LoadProperty(ScoreProperty, dto.Score);
            LoadProperty(CriticalFailProperty, dto.CriticalFail);
            LoadProperty(SubmittedProperty, dto.Submitted);
		} 
        
    }
}
