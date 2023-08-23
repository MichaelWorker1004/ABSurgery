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
    public class ExamSessionReadOnly : ReadOnlyBase<ExamSessionReadOnly>, IExamSessionReadOnly
    {
        [DataMember]
		[DisplayName(nameof(ExamScheduleId))]
        public int ExamScheduleId => ReadProperty(ExamScheduleIdProperty);
		public static readonly PropertyInfo<int> ExamScheduleIdProperty = RegisterProperty<int>(c => c.ExamScheduleId);

        [DataMember]
		[DisplayName(nameof(FirstName))]
        public string FirstName => ReadProperty(FirstNameProperty);
		public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);

        [DataMember]
		[DisplayName(nameof(LastName))]
        public string LastName => ReadProperty(LastNameProperty);
		public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);

        [DataMember]
		[DisplayName(nameof(StartTime))]
        public string StartTime => ReadProperty(StartTimeProperty);
		public static readonly PropertyInfo<string> StartTimeProperty = RegisterProperty<string>(c => c.StartTime);

        [DataMember]
		[DisplayName(nameof(EndTime))]
        public string EndTime => ReadProperty(EndTimeProperty);
		public static readonly PropertyInfo<string> EndTimeProperty = RegisterProperty<string>(c => c.EndTime);

        [DataMember]
		[DisplayName(nameof(MeetingLink))]
        public string MeetingLink => ReadProperty(MeetingLinkProperty);
		public static readonly PropertyInfo<string> MeetingLinkProperty = RegisterProperty<string>(c => c.MeetingLink);

        [DataMember]
		[DisplayName(nameof(IsSubmitted))]
        public bool? IsSubmitted => ReadProperty(IsSubmittedProperty);
		public static readonly PropertyInfo<bool?> IsSubmittedProperty = RegisterProperty<bool?>(c => c.IsSubmitted);

        [DataMember]
		[DisplayName(nameof(IsCurrentSession))]
        public bool? IsCurrentSession => ReadProperty(IsCurrentSessionProperty);
		public static readonly PropertyInfo<bool?> IsCurrentSessionProperty = RegisterProperty<bool?>(c => c.IsCurrentSession);

        [DataMember]
		[DisplayName(nameof(SessionNumber))]
        public int? SessionNumber => ReadProperty(SessionNumberProperty);
		public static readonly PropertyInfo<int?> SessionNumberProperty = RegisterProperty<int?>(c => c.SessionNumber);

        [DataMember]
		[DisplayName(nameof(isLocked))]
        public bool? isLocked => ReadProperty(isLockedProperty);
		public static readonly PropertyInfo<bool?> isLockedProperty = RegisterProperty<bool?>(c => c.isLocked);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(ExamSessionReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(ExamSessionReadOnlyDto dto)
		{
            LoadProperty(ExamScheduleIdProperty, dto.ExamScheduleId);
            LoadProperty(FirstNameProperty, dto.FirstName);
            LoadProperty(LastNameProperty, dto.LastName);
            LoadProperty(StartTimeProperty, dto.StartTime);
            LoadProperty(EndTimeProperty, dto.EndTime);
            LoadProperty(MeetingLinkProperty, dto.MeetingLink);
            LoadProperty(IsSubmittedProperty, dto.IsSubmitted);
            LoadProperty(IsCurrentSessionProperty, dto.IsCurrentSession);
            LoadProperty(SessionNumberProperty, dto.SessionNumber);
            LoadProperty(isLockedProperty, dto.IsLocked);
		} 
        
    }
}
