using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Documents;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Library.Documents;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Examinations.AccommodationFactory;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class Accommodation : YtgBusinessBase<Accommodation>, IAccommodation
    {
        private readonly IAccommodationDal _accommodationDal;
        private readonly IDocumentFactory _documentFactory;

        public Accommodation(
            IIdentityProvider identityProvider,
            IAccommodationDal accommodationDal,
            IDocumentFactory documentFactory)
            : base(identityProvider)
        {
            _accommodationDal = accommodationDal;
            _documentFactory = documentFactory;
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

        [DisplayName(nameof(AccommodationID))]
		public int AccommodationID
		{
			get { return GetProperty(AccommodationIDProperty); }
			set { SetProperty(AccommodationIDProperty, value); }
		}
		public static readonly PropertyInfo<int> AccommodationIDProperty = RegisterProperty<int>(c => c.AccommodationID);

        [DisplayName(nameof(AccommodationName))]
		public string AccommodationName
		{
			get { return GetProperty(AccommodationNameProperty); }
			set { SetProperty(AccommodationNameProperty, value); }
		}
		public static readonly PropertyInfo<string> AccommodationNameProperty = RegisterProperty<string>(c => c.AccommodationName);

        [DisplayName(nameof(DocumentId))]
		public int? DocumentId
		{
			get { return GetProperty(DocumentIdProperty); }
			set { SetProperty(DocumentIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> DocumentIdProperty = RegisterProperty<int?>(c => c.DocumentId);

        [DisplayName(nameof(DocumentName))]
		public string DocumentName
		{
			get { return GetProperty(DocumentNameProperty); }
			set { SetProperty(DocumentNameProperty, value); }
		}
		public static readonly PropertyInfo<string> DocumentNameProperty = RegisterProperty<string>(c => c.DocumentName);

        [DisplayName(nameof(ExamID))]
		public int? ExamID
		{
			get { return GetProperty(ExamIDProperty); }
			set { SetProperty(ExamIDProperty, value); }
		}
		public static readonly PropertyInfo<int?> ExamIDProperty = RegisterProperty<int?>(c => c.ExamID);

        [DataMember]
        [DisplayName(nameof(Document))]
        public IDocument Document
        {
            get => GetProperty(DocumentProperty);
            private set => SetProperty(DocumentProperty, value);
        }
        public static readonly PropertyInfo<Document> DocumentProperty = RegisterProperty<Document>(c => c.Document);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        // [ObjectAuthorizationRules]
        // public static void AddObjectAuthorizationRules()
        // {
        //     Csla.Rules.BusinessRules.AddRule(typeof(Accommodation),
        //         new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, 
        //             SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        //     Csla.Rules.BusinessRules.AddRule(typeof(Accommodation),
        //         new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
        //             SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        //     Csla.Rules.BusinessRules.AddRule(typeof(Accommodation),
        //         new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, 
        //             SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        //     Csla.Rules.BusinessRules.AddRule(typeof(Accommodation),
        //         new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, 
        //             SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        // }


        [RunLocal]
        [DeleteSelf]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task DeleteSelf()
        {
            using (BypassPropertyChecks)
            {
                base.DataPortal_DeleteSelf();
        
                await _accommodationDal.DeleteAsync(ToDto());
        
                MarkIdle();
            }
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByExamId(GetByExamIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _accommodationDal.GetByExamIdAsync(
                    _identity.GetUserId<int>(),
                    criteria.ExamId);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("Accommodation not found based on criteria");
                }

                if (dto.DocumentId.HasValue)
                {
                    LoadProperty(DocumentProperty, await _documentFactory.GetByIdAsync(dto.DocumentId.Value));
                }

                FetchData(dto);
            }
        }

        [Create]
        private void Create()
        {
            base.DataPortal_Create();
            LoadProperty(UserIdProperty, _identity.GetUserId<int>());
            LoadProperty(CreatedByUserIdProperty, _identity.GetUserId<int>());
            LoadProperty(LastUpdatedByUserIdProperty, _identity.GetUserId<int>());
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
                if(Document != null)
                {
                    var newDocument = await Document.SaveAsync();
                    LoadProperty(DocumentProperty, newDocument);
                    LoadProperty(DocumentIdProperty, Document.Id);
                }

                var dto = await _accommodationDal.InsertAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }

        [RunLocal]
        [Update]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task Update()
        {
            base.DataPortal_Update();
        
            using (BypassPropertyChecks)
            {
				if (Document != null)
				{
					var newDocument = await Document.SaveAsync();
					LoadProperty(DocumentProperty, newDocument);
					LoadProperty(DocumentIdProperty, Document.Id);
				}

				var dto = await _accommodationDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(AccommodationDto dto)
		{
            base.FetchData(dto);
            
			this.Id = dto.Id;
			this.UserId = dto.UserId;
			this.AccommodationID = dto.AccommodationID;
			this.AccommodationName = dto.AccommodationName;
			this.DocumentId = dto.DocumentId;
			this.DocumentName = dto.DocumentName;
			this.ExamID = dto.ExamID;
		}

		internal AccommodationDto ToDto()
		{
			var dto = new AccommodationDto();
			return ToDto(dto);
		}
		internal AccommodationDto ToDto(AccommodationDto dto)
		{
            base.ToDto(dto);
            
			dto.Id = this.Id;
			dto.UserId = this.UserId;
			dto.AccommodationID = this.AccommodationID;
			dto.AccommodationName = this.AccommodationName;
			dto.DocumentId = this.DocumentId;
			dto.DocumentName = this.DocumentName;
			dto.ExamID = this.ExamID;

			return dto;
		}

        public void LoadDocument(Stream file)
        {
            var document = _documentFactory.Create();
            document.DocumentTypeId = (int)DocumentTypes.Accomodation;
            document.DocumentName = $"Accommodation-{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}";
            document.InternalViewOnly = false;
            document.UploadedBy = _identity.GetUserName();
            document.File = file;
            LoadProperty(DocumentProperty, document);

		}
    }
}
