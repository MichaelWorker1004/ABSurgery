using Csla;
using SurgeonPortal.DataAccess.Contracts.Documents;
using SurgeonPortal.Library.Contracts.Documents;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SurgeonPortal.Library.Documents
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class DocumentReadOnly : ReadOnlyBase<DocumentReadOnly>, IDocumentReadOnly
    {
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(UserId))]
        public int UserId => ReadProperty(UserIdProperty);
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DataMember]
		[DisplayName(nameof(StreamId))]
        public Guid StreamId => ReadProperty(StreamIdProperty);
		public static readonly PropertyInfo<Guid> StreamIdProperty = RegisterProperty<Guid>(c => c.StreamId);

        [DataMember]
		[DisplayName(nameof(DocumentTypeId))]
        public int DocumentTypeId => ReadProperty(DocumentTypeIdProperty);
		public static readonly PropertyInfo<int> DocumentTypeIdProperty = RegisterProperty<int>(c => c.DocumentTypeId);

        [DataMember]
		[DisplayName(nameof(DocumentName))]
        public string DocumentName => ReadProperty(DocumentNameProperty);
		public static readonly PropertyInfo<string> DocumentNameProperty = RegisterProperty<string>(c => c.DocumentName);

        [DataMember]
		[DisplayName(nameof(DocumentType))]
        public string DocumentType => ReadProperty(DocumentTypeProperty);
		public static readonly PropertyInfo<string> DocumentTypeProperty = RegisterProperty<string>(c => c.DocumentType);

        [DataMember]
		[DisplayName(nameof(InternalViewOnly))]
        public bool InternalViewOnly => ReadProperty(InternalViewOnlyProperty);
		public static readonly PropertyInfo<bool> InternalViewOnlyProperty = RegisterProperty<bool>(c => c.InternalViewOnly);

        [DataMember]
		[DisplayName(nameof(UploadedBy))]
        public string UploadedBy => ReadProperty(UploadedByProperty);
		public static readonly PropertyInfo<string> UploadedByProperty = RegisterProperty<string>(c => c.UploadedBy);

        [DataMember]
		[DisplayName(nameof(UploadedDateUtc))]
        public DateTime UploadedDateUtc => ReadProperty(UploadedDateUtcProperty);
		public static readonly PropertyInfo<DateTime> UploadedDateUtcProperty = RegisterProperty<DateTime>(c => c.UploadedDateUtc);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(DocumentReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(DocumentReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(UserIdProperty, dto.UserId);
            LoadProperty(StreamIdProperty, dto.StreamId);
            LoadProperty(DocumentTypeIdProperty, dto.DocumentTypeId);
            LoadProperty(DocumentNameProperty, dto.DocumentName);
            LoadProperty(DocumentTypeProperty, dto.DocumentType);
            LoadProperty(InternalViewOnlyProperty, dto.InternalViewOnly);
            LoadProperty(UploadedByProperty, dto.UploadedBy);
            LoadProperty(UploadedDateUtcProperty, dto.UploadedDateUtc);
		} 
        
    }
}
