using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring.CE;
using SurgeonPortal.Library.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring.CE;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using static SurgeonPortal.Library.Scoring.CaseDetailReadOnlyListFactory;

namespace SurgeonPortal.Library.Scoring.CE
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class TitleReadOnly : ReadOnlyBase<TitleReadOnly>, ITitleReadOnly
    {
        [DataMember]
		[DisplayName(nameof(Title))]
        public string Title => ReadProperty(TitleProperty);
		public static readonly PropertyInfo<string> TitleProperty = RegisterProperty<string>(c => c.Title);

        [DataMember]
		[DisplayName(nameof(CaseHeaderId))]
        public int CaseHeaderId => ReadProperty(CaseHeaderIdProperty);
		public static readonly PropertyInfo<int> CaseHeaderIdProperty = RegisterProperty<int>(c => c.CaseHeaderId);

        [DataMember]
		[DisplayName(nameof(ExamCaseId))]
        public int? ExamCaseId => ReadProperty(ExamCaseIdProperty);
		public static readonly PropertyInfo<int?> ExamCaseIdProperty = RegisterProperty<int?>(c => c.ExamCaseId);

        public static readonly PropertyInfo<CaseDetailReadOnlyList> SectionsProperty =
            RegisterProperty<CaseDetailReadOnlyList>(c => c.Sections);
        public ICaseDetailReadOnlyList Sections
        {
            get => GetProperty(SectionsProperty);
            private set => LoadProperty(SectionsProperty, value);
        }
        
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

        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task Child_Fetch(TitleReadOnlyDto dto)
        {
            await FetchDataAsync(dto);
        }

        
		private async Task FetchDataAsync(TitleReadOnlyDto dto)
		{
            LoadProperty(TitleProperty, dto.Title);
            LoadProperty(CaseHeaderIdProperty, dto.CaseHeaderId);
            LoadProperty(ExamCaseIdProperty, dto.ExamCaseId);
            LoadProperty(SectionsProperty, await DataPortal.FetchAsync<CaseDetailReadOnlyList>(new GetByCaseHeaderIdCriteria(dto.CaseHeaderId)));
            LoadProperty(ScoreProperty, dto.Score);
            LoadProperty(CriticalFailProperty, dto.CriticalFail);
            LoadProperty(RemarksProperty, dto.Remarks);
		} 
        
    }
}
