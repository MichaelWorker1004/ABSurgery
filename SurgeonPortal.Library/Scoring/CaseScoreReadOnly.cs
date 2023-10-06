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
    public class CaseScoreReadOnly : ReadOnlyBase<CaseScoreReadOnly>, ICaseScoreReadOnly
    {
        [DataMember]
		[DisplayName(nameof(ExamScoringId))]
        public int? ExamScoringId => ReadProperty(ExamScoringIdProperty);
		public static readonly PropertyInfo<int?> ExamScoringIdProperty = RegisterProperty<int?>(c => c.ExamScoringId);

        [DataMember]
		[DisplayName(nameof(ExamCaseId))]
        public int ExamCaseId => ReadProperty(ExamCaseIdProperty);
		public static readonly PropertyInfo<int> ExamCaseIdProperty = RegisterProperty<int>(c => c.ExamCaseId);

        [DataMember]
		[DisplayName(nameof(CaseNumber))]
        public string CaseNumber => ReadProperty(CaseNumberProperty);
		public static readonly PropertyInfo<string> CaseNumberProperty = RegisterProperty<string>(c => c.CaseNumber);

        [DataMember]
		[DisplayName(nameof(ExaminerUserId))]
        public int? ExaminerUserId => ReadProperty(ExaminerUserIdProperty);
		public static readonly PropertyInfo<int?> ExaminerUserIdProperty = RegisterProperty<int?>(c => c.ExaminerUserId);

        [DataMember]
		[DisplayName(nameof(ExamineeUserId))]
        public int? ExamineeUserId => ReadProperty(ExamineeUserIdProperty);
		public static readonly PropertyInfo<int?> ExamineeUserIdProperty = RegisterProperty<int?>(c => c.ExamineeUserId);

        [DataMember]
		[DisplayName(nameof(ExamineeFirstName))]
        public string ExamineeFirstName => ReadProperty(ExamineeFirstNameProperty);
		public static readonly PropertyInfo<string> ExamineeFirstNameProperty = RegisterProperty<string>(c => c.ExamineeFirstName);

        [DataMember]
		[DisplayName(nameof(ExamineeMiddleName))]
        public string ExamineeMiddleName => ReadProperty(ExamineeMiddleNameProperty);
		public static readonly PropertyInfo<string> ExamineeMiddleNameProperty = RegisterProperty<string>(c => c.ExamineeMiddleName);

        [DataMember]
		[DisplayName(nameof(ExamineeLastName))]
        public string ExamineeLastName => ReadProperty(ExamineeLastNameProperty);
		public static readonly PropertyInfo<string> ExamineeLastNameProperty = RegisterProperty<string>(c => c.ExamineeLastName);

        [DataMember]
		[DisplayName(nameof(ExamineeSuffix))]
        public string ExamineeSuffix => ReadProperty(ExamineeSuffixProperty);
		public static readonly PropertyInfo<string> ExamineeSuffixProperty = RegisterProperty<string>(c => c.ExamineeSuffix);

        [DataMember]
		[DisplayName(nameof(ExamDate))]
        public DateTime? ExamDate => ReadProperty(ExamDateProperty);
		public static readonly PropertyInfo<DateTime?> ExamDateProperty = RegisterProperty<DateTime?>(c => c.ExamDate);

        [DataMember]
		[DisplayName(nameof(StartTime))]
        public TimeSpan StartTime => ReadProperty(StartTimeProperty);
		public static readonly PropertyInfo<TimeSpan> StartTimeProperty = RegisterProperty<TimeSpan>(c => c.StartTime);

        [DataMember]
		[DisplayName(nameof(EndTime))]
        public TimeSpan EndTime => ReadProperty(EndTimeProperty);
		public static readonly PropertyInfo<TimeSpan> EndTimeProperty = RegisterProperty<TimeSpan>(c => c.EndTime);

        [DataMember]
		[DisplayName(nameof(Score))]
        public int? Score => ReadProperty(ScoreProperty);
		public static readonly PropertyInfo<int?> ScoreProperty = RegisterProperty<int?>(c => c.Score);

        [DataMember]
		[DisplayName(nameof(CriticalFail))]
        public bool? CriticalFail => ReadProperty(CriticalFailProperty);
		public static readonly PropertyInfo<bool?> CriticalFailProperty = RegisterProperty<bool?>(c => c.CriticalFail);

        [DataMember]
		[DisplayName(nameof(Remarks))]
        public string Remarks => ReadProperty(RemarksProperty);
		public static readonly PropertyInfo<string> RemarksProperty = RegisterProperty<string>(c => c.Remarks);

        [DataMember]
		[DisplayName(nameof(isLocked))]
        public bool? isLocked => ReadProperty(isLockedProperty);
		public static readonly PropertyInfo<bool?> isLockedProperty = RegisterProperty<bool?>(c => c.isLocked);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(CaseScoreReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(CaseScoreReadOnlyDto dto)
		{
            LoadProperty(ExamScoringIdProperty, dto.ExamScoringId);
            LoadProperty(ExamCaseIdProperty, dto.ExamCaseId);
            LoadProperty(CaseNumberProperty, dto.CaseNumber);
            LoadProperty(ExaminerUserIdProperty, dto.ExaminerUserId);
            LoadProperty(ExamineeUserIdProperty, dto.ExamineeUserId);
            LoadProperty(ExamineeFirstNameProperty, dto.ExamineeFirstName);
            LoadProperty(ExamineeMiddleNameProperty, dto.ExamineeMiddleName);
            LoadProperty(ExamineeLastNameProperty, dto.ExamineeLastName);
            LoadProperty(ExamineeSuffixProperty, dto.ExamineeSuffix);
            LoadProperty(ExamDateProperty, dto.ExamDate);
            LoadProperty(StartTimeProperty, dto.StartTime);
            LoadProperty(EndTimeProperty, dto.EndTime);
            LoadProperty(ScoreProperty, dto.Score);
            LoadProperty(CriticalFailProperty, dto.CriticalFail);
            LoadProperty(RemarksProperty, dto.Remarks);
            LoadProperty(isLockedProperty, dto.IsLocked);
		} 
        
    }
}
