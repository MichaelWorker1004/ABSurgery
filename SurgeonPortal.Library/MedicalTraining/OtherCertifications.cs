using Csla;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.MedicalTraining.OtherCertificationsFactory;

namespace SurgeonPortal.Library.MedicalTraining
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class OtherCertifications : YtgBusinessBase<OtherCertifications>, IOtherCertifications
    {
        private readonly IOtherCertificationsDal _otherCertificationsDal;

        public OtherCertifications(
            IIdentityProvider identityProvider,
            IOtherCertificationsDal otherCertificationsDal)
            : base(identityProvider)
        {
            _otherCertificationsDal = otherCertificationsDal;
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

        [DisplayName(nameof(CertificateTypeId))]
		public int CertificateTypeId
		{
			get { return GetProperty(CertificateTypeIdProperty); }
			set { SetProperty(CertificateTypeIdProperty, value); }
		}
		public static readonly PropertyInfo<int> CertificateTypeIdProperty = RegisterProperty<int>(c => c.CertificateTypeId);

        [DisplayName(nameof(CertificateTypeName))]
		public string CertificateTypeName
		{
			get { return GetProperty(CertificateTypeNameProperty); }
			set { SetProperty(CertificateTypeNameProperty, value); }
		}
		public static readonly PropertyInfo<string> CertificateTypeNameProperty = RegisterProperty<string>(c => c.CertificateTypeName);

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
        
                await _otherCertificationsDal.DeleteAsync(ToDto());
        
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
                var dto = await _otherCertificationsDal.GetByIdAsync(criteria.Id);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("OtherCertifications not found based on criteria");
                }
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
                var dto = await _otherCertificationsDal.InsertAsync(ToDto());
        
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
                var dto = await _otherCertificationsDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(OtherCertificationsDto dto)
		{
            base.FetchData(dto);
            
			this.Id = dto.Id;
			this.UserId = dto.UserId;
			this.CertificateTypeId = dto.CertificateTypeId;
			this.CertificateTypeName = dto.CertificateTypeName;
			this.IssueDate = dto.IssueDate;
			this.CertificateNumber = dto.CertificateNumber;
		}

		internal OtherCertificationsDto ToDto()
		{
			var dto = new OtherCertificationsDto();
			return ToDto(dto);
		}
		internal OtherCertificationsDto ToDto(OtherCertificationsDto dto)
		{
            base.ToDto(dto);
            
			dto.Id = this.Id;
			dto.UserId = this.UserId;
			dto.CertificateTypeId = this.CertificateTypeId;
			dto.CertificateTypeName = this.CertificateTypeName;
			dto.IssueDate = this.IssueDate;
			dto.CertificateNumber = this.CertificateNumber;

			return dto;
		}


    }
}
