using Csla;
using Csla.Rules.CommonRules;
using Microsoft.Extensions.Options;
using SurgeonPortal.DataAccess.Contracts.Documents;
using SurgeonPortal.DataAccess.Contracts.Storage;
using SurgeonPortal.Library.Contracts.Documents;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Exceptions;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Documents.DocumentFactory;

namespace SurgeonPortal.Library.Documents
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class Document : YtgBusinessBase<Document>, IDocument
    {
        private readonly IDocumentDal _documentDal;
        private readonly IStorageDal _storageDal;

        public Document(
            IIdentityProvider identityProvider,
            IDocumentDal documentDal,
            IStorageDal storageDal)
            : base(identityProvider)
        {
            _documentDal = documentDal;
            _storageDal = storageDal;
        }

        [Key] 
        [DisplayName(nameof(Id))]
		public int Id
		{
			get { return GetProperty(IdProperty); }
			set { SetProperty(IdProperty, value); }
		}
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DisplayName(nameof(UserId))]
		public int UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DisplayName(nameof(StreamId))]
		public Guid StreamId
		{
			get { return GetProperty(StreamIdProperty); }
            private set { SetProperty(StreamIdProperty, value); }
		}
		public static readonly PropertyInfo<Guid> StreamIdProperty = RegisterProperty<Guid>(c => c.StreamId);

        [DisplayName(nameof(DocumentTypeId))]
		public int DocumentTypeId
		{
			get { return GetProperty(DocumentTypeIdProperty); }
			set { SetProperty(DocumentTypeIdProperty, value); }
		}
		public static readonly PropertyInfo<int> DocumentTypeIdProperty = RegisterProperty<int>(c => c.DocumentTypeId);

        [DisplayName(nameof(DocumentName))]
		public string DocumentName
		{
			get { return GetProperty(DocumentNameProperty); }
			set { SetProperty(DocumentNameProperty, value); }
		}
		public static readonly PropertyInfo<string> DocumentNameProperty = RegisterProperty<string>(c => c.DocumentName);

        [DisplayName(nameof(DocumentType))]
		public string DocumentType
		{
			get { return GetProperty(DocumentTypeProperty); }
			set { SetProperty(DocumentTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> DocumentTypeProperty = RegisterProperty<string>(c => c.DocumentType);

        [DisplayName(nameof(InternalViewOnly))]
		public bool InternalViewOnly
		{
			get { return GetProperty(InternalViewOnlyProperty); }
			set { SetProperty(InternalViewOnlyProperty, value); }
		}
		public static readonly PropertyInfo<bool> InternalViewOnlyProperty = RegisterProperty<bool>(c => c.InternalViewOnly);

        [DisplayName(nameof(UploadedBy))]
		public string UploadedBy
		{
			get { return GetProperty(UploadedByProperty); }
			set { SetProperty(UploadedByProperty, value); }
		}
		public static readonly PropertyInfo<string> UploadedByProperty = RegisterProperty<string>(c => c.UploadedBy);

        [DisplayName(nameof(UploadedDateUtc))]
		public DateTime UploadedDateUtc
		{
			get { return GetProperty(UploadedDateUtcProperty); }
			set { SetProperty(UploadedDateUtcProperty, value); }
		}
		public static readonly PropertyInfo<DateTime> UploadedDateUtcProperty = RegisterProperty<DateTime>(c => c.UploadedDateUtc);

        private string FileName
        {
            get
            {
                var fileName = $"{StreamId.ToString()}.pdf";
                return fileName;
            }
        }

        public static readonly PropertyInfo<Stream> FileProperty = RegisterProperty<Stream>(c => c.File);
        [DataMember]
        [DisplayName(nameof(File))]
        public Stream File
        {
            get => GetProperty(FileProperty);
            set => SetProperty(FileProperty, value);
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            

        }



        /// <summary>
        /// This method is used to add business rules to the Csla 
        /// business rule engine
        /// </summary>
        protected override void AddBusinessRules()
        {
            // Only process priority 5 and higher if all 4 and lower completed first
            BusinessRules.ProcessThroughPriority = 4;

            BusinessRules.AddRule(new Required(IdProperty, "Id is required"));
            BusinessRules.AddRule(new Required(UserIdProperty, "UserId is required"));
        }

        [Create]
        private void Create()
        {
            LoadProperty(StreamIdProperty, Guid.NewGuid());

        }

        [RunLocal]
        [DeleteSelf]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task DeleteSelf()
        {
            using (BypassPropertyChecks)
            {
                base.DataPortal_DeleteSelf();
        
                await _documentDal.DeleteAsync(ToDto());
        
                await _storageDal.DeleteAsync(FileName);

                MarkIdle();
            }
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetById(GetByIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _documentDal.GetByIdAsync(criteria.Id);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("Document not found based on criteria");
                }
                FetchData(dto);

                try
                {
                    File = await _storageDal.LoadAsync(FileName);
                }
                catch(Exception ex)
                {
                    throw new DataNotFoundException($"Unable to find the document named: {FileName}");
                }
            }
        }

        [RunLocal]
        [Insert]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task Insert()
        {
            base.DataPortal_Insert();
        
            using (BypassPropertyChecks)
            {
                var dto = await _documentDal.InsertAsync(ToDto());
        
                await _storageDal.SaveAsync(File, FileName);

                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(DocumentDto dto)
		{
            base.FetchData(dto);
            
			this.Id = dto.Id;
			this.UserId = dto.UserId;
			this.StreamId = dto.StreamId;
			this.DocumentTypeId = dto.DocumentTypeId;
			this.DocumentName = dto.DocumentName;
			this.DocumentType = dto.DocumentType;
			this.InternalViewOnly = dto.InternalViewOnly;
			this.UploadedBy = dto.UploadedBy;
			this.UploadedDateUtc = dto.UploadedDateUtc;
		}

		internal DocumentDto ToDto()
		{
			var dto = new DocumentDto();
			return ToDto(dto);
		}
		internal DocumentDto ToDto(DocumentDto dto)
		{
            base.ToDto(dto);
            
			dto.Id = this.Id;
			dto.UserId = this.UserId;
			dto.StreamId = this.StreamId;
			dto.DocumentTypeId = this.DocumentTypeId;
			dto.DocumentName = this.DocumentName;
			dto.DocumentType = this.DocumentType;
			dto.InternalViewOnly = this.InternalViewOnly;
			dto.UploadedBy = this.UploadedBy;
			dto.UploadedDateUtc = this.UploadedDateUtc;

			return dto;
		}


    }
}
