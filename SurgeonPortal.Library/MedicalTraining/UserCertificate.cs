using Csla;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.Documents;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Library.Documents;
using SurgeonPortal.Shared;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.MedicalTraining.UserCertificateFactory;

namespace SurgeonPortal.Library.MedicalTraining
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class UserCertificate : YtgBusinessBase<UserCertificate>, IUserCertificate
    {
        private readonly IUserCertificateDal _userCertificateDal;
        private readonly IDocumentFactory _documentFactory;

        public UserCertificate(
            IIdentityProvider identityProvider,
            IUserCertificateDal userCertificateDal,
            IDocumentFactory documentFactory)
            : base(identityProvider)
        {
            _userCertificateDal = userCertificateDal;
            _documentFactory = documentFactory;
        }

        [Key] 
        [DisplayName(nameof(CertificateId))]
		public int CertificateId
		{
			get { return GetProperty(CertificateIdProperty); }
			set { SetProperty(CertificateIdProperty, value); }
		}
		public static readonly PropertyInfo<int> CertificateIdProperty = RegisterProperty<int>(c => c.CertificateId);

        [DisplayName(nameof(UserId))]
		public int UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DisplayName(nameof(DocumentId))]
		public int DocumentId
		{
			get { return GetProperty(DocumentIdProperty); }
			set { SetProperty(DocumentIdProperty, value); }
		}
		public static readonly PropertyInfo<int> DocumentIdProperty = RegisterProperty<int>(c => c.DocumentId);

        [DisplayName(nameof(CertificateTypeId))]
		public int CertificateTypeId
		{
			get { return GetProperty(CertificateTypeIdProperty); }
			set { SetProperty(CertificateTypeIdProperty, value); }
		}
		public static readonly PropertyInfo<int> CertificateTypeIdProperty = RegisterProperty<int>(c => c.CertificateTypeId);

        [DisplayName(nameof(CertificateType))]
		public string CertificateType
		{
			get { return GetProperty(CertificateTypeProperty); }
			set { SetProperty(CertificateTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> CertificateTypeProperty = RegisterProperty<string>(c => c.CertificateType);

        [DisplayName(nameof(IssueDate))]
		public DateTime IssueDate
		{
			get { return GetProperty(IssueDateProperty); }
			set { SetProperty(IssueDateProperty, value); }
		}
		public static readonly PropertyInfo<DateTime> IssueDateProperty = RegisterProperty<DateTime>(c => c.IssueDate);

        [DisplayName(nameof(CertificateNumber))]
		public string CertificateNumber
		{
			get { return GetProperty(CertificateNumberProperty); }
			set { SetProperty(CertificateNumberProperty, value); }
		}
		public static readonly PropertyInfo<string> CertificateNumberProperty = RegisterProperty<string>(c => c.CertificateNumber);

        public static readonly PropertyInfo<Document> DocumentProperty = RegisterProperty<Document>(c => c.Document);
        [DataMember]
        [DisplayName(nameof(Document))]
        public IDocument Document
        {
            get => GetProperty(DocumentProperty);
            private set => SetProperty(DocumentProperty, value);
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            

            

            

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
        
                await _userCertificateDal.DeleteAsync(ToDto());
        
                if (Document != null)
                {
                    Document.Delete();
                    var document = await Document.SaveAsync();
                    LoadProperty(DocumentProperty, document);
                }

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
                var dto = await _userCertificateDal.GetByIdAsync(criteria.CertificateId);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("UserCertificate not found based on criteria");
                }

                LoadProperty(DocumentProperty, await _documentFactory.GetByIdAsync(dto.DocumentId));

                FetchData(dto);
            }
        }

        [Create]
        private void Create()
        {
            LoadProperty(UserIdProperty, _identity.GetUserId<int>());
            LoadProperty(CreatedByUserIdProperty, _identity.GetUserId<int>());
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

                var dto = await _userCertificateDal.InsertAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(UserCertificateDto dto)
		{
            base.FetchData(dto);
            
			this.CertificateId = dto.CertificateId;
			this.UserId = dto.UserId;
			this.DocumentId = dto.DocumentId;
			this.CertificateTypeId = dto.CertificateTypeId;
			this.CertificateType = dto.CertificateType;
			this.IssueDate = dto.IssueDate;
			this.CertificateNumber = dto.CertificateNumber;
		}

		internal UserCertificateDto ToDto()
		{
			var dto = new UserCertificateDto();
			return ToDto(dto);
		}
		internal UserCertificateDto ToDto(UserCertificateDto dto)
		{
            base.ToDto(dto);
            
			dto.CertificateId = this.CertificateId;
			dto.UserId = this.UserId;
			dto.DocumentId = this.DocumentId;
			dto.CertificateTypeId = this.CertificateTypeId;
			dto.CertificateType = this.CertificateType;
			dto.IssueDate = this.IssueDate;
			dto.CertificateNumber = this.CertificateNumber;

			return dto;
		}

        public void LoadDocument(Stream file)
        {
            var document = _documentFactory.Create();
            document.UserId = IdentityHelper.UserId;
            document.DocumentTypeId = (int)DocumentTypes.Certificate;
            document.DocumentName = $"Certificate-{Enum.GetName(typeof(CertificateTypes), CertificateTypeId)}-{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}";
            document.InternalViewOnly = false;
            document.UploadedBy = IdentityHelper.UserName;
            document.File = file;
            LoadProperty(DocumentProperty, document);
        }
    }
}
