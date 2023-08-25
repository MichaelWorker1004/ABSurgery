using Csla;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.ProfessionalStanding.MedicalLicenseFactory;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class MedicalLicense : YtgBusinessBase<MedicalLicense>, IMedicalLicense
    {
        private readonly IMedicalLicenseDal _medicalLicenseDal;

        public MedicalLicense(
            IIdentityProvider identityProvider,
            IMedicalLicenseDal medicalLicenseDal)
            : base(identityProvider)
        {
            _medicalLicenseDal = medicalLicenseDal;
        }

        [Key] 
        [DisplayName(nameof(LicenseId))]
		public decimal LicenseId
		{
			get { return GetProperty(LicenseIdProperty); }
			set { SetProperty(LicenseIdProperty, value); }
		}
		public static readonly PropertyInfo<decimal> LicenseIdProperty = RegisterProperty<decimal>(c => c.LicenseId);

        [DisplayName(nameof(UserId))]
		public int? UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> UserIdProperty = RegisterProperty<int?>(c => c.UserId);

        [DisplayName(nameof(IssuingStateId))]
		public string IssuingStateId
		{
			get { return GetProperty(IssuingStateIdProperty); }
			set { SetProperty(IssuingStateIdProperty, value); }
		}
		public static readonly PropertyInfo<string> IssuingStateIdProperty = RegisterProperty<string>(c => c.IssuingStateId);

        [DisplayName(nameof(IssuingState))]
		public string IssuingState
		{
			get { return GetProperty(IssuingStateProperty); }
			set { SetProperty(IssuingStateProperty, value); }
		}
		public static readonly PropertyInfo<string> IssuingStateProperty = RegisterProperty<string>(c => c.IssuingState);

        [DisplayName(nameof(LicenseNumber))]
		public string LicenseNumber
		{
			get { return GetProperty(LicenseNumberProperty); }
			set { SetProperty(LicenseNumberProperty, value); }
		}
		public static readonly PropertyInfo<string> LicenseNumberProperty = RegisterProperty<string>(c => c.LicenseNumber);

        [DisplayName(nameof(LicenseTypeId))]
		public int? LicenseTypeId
		{
			get { return GetProperty(LicenseTypeIdProperty); }
			set { SetProperty(LicenseTypeIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> LicenseTypeIdProperty = RegisterProperty<int?>(c => c.LicenseTypeId);

        [DisplayName(nameof(LicenseType))]
		public string LicenseType
		{
			get { return GetProperty(LicenseTypeProperty); }
			set { SetProperty(LicenseTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> LicenseTypeProperty = RegisterProperty<string>(c => c.LicenseType);

        [DisplayName(nameof(IssueDate))]
		public DateTime? IssueDate
		{
			get { return GetProperty(IssueDateProperty); }
			set { SetProperty(IssueDateProperty, value); }
		}
		public static readonly PropertyInfo<DateTime?> IssueDateProperty = RegisterProperty<DateTime?>(c => c.IssueDate);

        [DisplayName(nameof(ExpireDate))]
		public DateTime? ExpireDate
		{
			get { return GetProperty(ExpireDateProperty); }
			set { SetProperty(ExpireDateProperty, value); }
		}
		public static readonly PropertyInfo<DateTime?> ExpireDateProperty = RegisterProperty<DateTime?>(c => c.ExpireDate);

        [DisplayName(nameof(ReportingOrganization))]
		public string ReportingOrganization
		{
			get { return GetProperty(ReportingOrganizationProperty); }
			set { SetProperty(ReportingOrganizationProperty, value); }
		}
		public static readonly PropertyInfo<string> ReportingOrganizationProperty = RegisterProperty<string>(c => c.ReportingOrganization);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(MedicalLicense),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(MedicalLicense),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(MedicalLicense),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(MedicalLicense),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

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
        
                await _medicalLicenseDal.DeleteAsync(ToDto());
        
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
                var dto = await _medicalLicenseDal.GetByIdAsync(criteria.LicenseId);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("MedicalLicense not found based on criteria");
                }
                FetchData(dto);
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
                var dto = await _medicalLicenseDal.InsertAsync(ToDto());
        
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
                var dto = await _medicalLicenseDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(MedicalLicenseDto dto)
		{
            base.FetchData(dto);
            
			this.LicenseId = dto.LicenseId;
			this.UserId = dto.UserId;
			this.IssuingStateId = dto.IssuingStateId;
			this.IssuingState = dto.IssuingState;
			this.LicenseNumber = dto.LicenseNumber;
			this.LicenseTypeId = dto.LicenseTypeId;
			this.LicenseType = dto.LicenseType;
			this.IssueDate = dto.IssueDate;
			this.ExpireDate = dto.ExpireDate;
			this.ReportingOrganization = dto.ReportingOrganization;
		}

		internal MedicalLicenseDto ToDto()
		{
			var dto = new MedicalLicenseDto();
			return ToDto(dto);
		}
		internal MedicalLicenseDto ToDto(MedicalLicenseDto dto)
		{
            base.ToDto(dto);
            
			dto.LicenseId = this.LicenseId;
			dto.UserId = this.UserId;
			dto.IssuingStateId = this.IssuingStateId;
			dto.IssuingState = this.IssuingState;
			dto.LicenseNumber = this.LicenseNumber;
			dto.LicenseTypeId = this.LicenseTypeId;
			dto.LicenseType = this.LicenseType;
			dto.IssueDate = this.IssueDate;
			dto.ExpireDate = this.ExpireDate;
			dto.ReportingOrganization = this.ReportingOrganization;

			return dto;
		}


    }
}
