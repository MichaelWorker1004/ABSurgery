using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ExamHistoryReadOnly : ReadOnlyBase<ExamHistoryReadOnly>, IExamHistoryReadOnly
    {
        [DataMember]
		[DisplayName(nameof(UserId))]
        public int? UserId => ReadProperty(UserIdProperty);
		public static readonly PropertyInfo<int?> UserIdProperty = RegisterProperty<int?>(c => c.UserId);

        [DataMember]
		[DisplayName(nameof(ExaminationId))]
        public decimal ExaminationId => ReadProperty(ExaminationIdProperty);
		public static readonly PropertyInfo<decimal> ExaminationIdProperty = RegisterProperty<decimal>(c => c.ExaminationId);

        [DataMember]
		[DisplayName(nameof(ExaminationName))]
        public string ExaminationName => ReadProperty(ExaminationNameProperty);
		public static readonly PropertyInfo<string> ExaminationNameProperty = RegisterProperty<string>(c => c.ExaminationName);

        [DataMember]
		[DisplayName(nameof(ExaminationDate))]
        public DateTime? ExaminationDate => ReadProperty(ExaminationDateProperty);
		public static readonly PropertyInfo<DateTime?> ExaminationDateProperty = RegisterProperty<DateTime?>(c => c.ExaminationDate);

        [DataMember]
		[DisplayName(nameof(DocumentId))]
        public int? DocumentId => ReadProperty(DocumentIdProperty);
		public static readonly PropertyInfo<int?> DocumentIdProperty = RegisterProperty<int?>(c => c.DocumentId);

        [DataMember]
		[DisplayName(nameof(status))]
        public string status => ReadProperty(statusProperty);
		public static readonly PropertyInfo<string> statusProperty = RegisterProperty<string>(c => c.status);

        [DataMember]
		[DisplayName(nameof(result))]
        public string result => ReadProperty(resultProperty);
		public static readonly PropertyInfo<string> resultProperty = RegisterProperty<string>(c => c.result);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(ExamHistoryReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(ExamHistoryReadOnlyDto dto)
		{
            LoadProperty(UserIdProperty, dto.UserId);
            LoadProperty(ExaminationIdProperty, dto.ExaminationId);
            LoadProperty(ExaminationNameProperty, dto.ExaminationName);
            LoadProperty(ExaminationDateProperty, dto.ExaminationDate);
            LoadProperty(DocumentIdProperty, dto.DocumentId);
            LoadProperty(statusProperty, dto.Status);
            LoadProperty(resultProperty, dto.Result);
		} 
        
    }
}
