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
    public class CaseDetailReadOnly : ReadOnlyBase<CaseDetailReadOnly>, ICaseDetailReadOnly
    {
        [DataMember]
		[DisplayName(nameof(CaseNumber))]
        public string CaseNumber => ReadProperty(CaseNumberProperty);
		public static readonly PropertyInfo<string> CaseNumberProperty = RegisterProperty<string>(c => c.CaseNumber);

        [DataMember]
		[DisplayName(nameof(CaseTitle))]
        public string CaseTitle => ReadProperty(CaseTitleProperty);
		public static readonly PropertyInfo<string> CaseTitleProperty = RegisterProperty<string>(c => c.CaseTitle);

        [DataMember]
		[DisplayName(nameof(CaseContentId))]
        public int CaseContentId => ReadProperty(CaseContentIdProperty);
		public static readonly PropertyInfo<int> CaseContentIdProperty = RegisterProperty<int>(c => c.CaseContentId);

        [DataMember]
		[DisplayName(nameof(Heading))]
        public string Heading => ReadProperty(HeadingProperty);
		public static readonly PropertyInfo<string> HeadingProperty = RegisterProperty<string>(c => c.Heading);

        [DataMember]
		[DisplayName(nameof(Content))]
        public string Content => ReadProperty(ContentProperty);
		public static readonly PropertyInfo<string> ContentProperty = RegisterProperty<string>(c => c.Content);

        [DataMember]
		[DisplayName(nameof(Comments))]
        public string Comments => ReadProperty(CommentsProperty);
		public static readonly PropertyInfo<string> CommentsProperty = RegisterProperty<string>(c => c.Comments);

        [DataMember]
		[DisplayName(nameof(CaseCommentId))]
        public int? CaseCommentId => ReadProperty(CaseCommentIdProperty);
		public static readonly PropertyInfo<int?> CaseCommentIdProperty = RegisterProperty<int?>(c => c.CaseCommentId);

        [DataMember]
		[DisplayName(nameof(SectionNumber))]
        public int? SectionNumber => ReadProperty(SectionNumberProperty);
		public static readonly PropertyInfo<int?> SectionNumberProperty = RegisterProperty<int?>(c => c.SectionNumber);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(CaseDetailReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(CaseDetailReadOnlyDto dto)
		{
            LoadProperty(CaseNumberProperty, dto.CaseNumber);
            LoadProperty(CaseTitleProperty, dto.CaseTitle);
            LoadProperty(CaseContentIdProperty, dto.CaseContentId);
            LoadProperty(HeadingProperty, dto.Heading);
            LoadProperty(ContentProperty, dto.Content);
            LoadProperty(CommentsProperty, dto.Comments);
            LoadProperty(CaseCommentIdProperty, dto.CaseCommentId);
            LoadProperty(SectionNumberProperty, dto.SectionNumber);
		} 
        
    }
}
