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
using static SurgeonPortal.Library.ProfessionalStanding.UserAppointmentFactory;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class UserAppointment : YtgBusinessBase<UserAppointment>, IUserAppointment
    {
        private readonly IUserAppointmentDal _userAppointmentDal;

        public UserAppointment(
            IIdentityProvider identityProvider,
            IUserAppointmentDal userAppointmentDal)
            : base(identityProvider)
        {
            _userAppointmentDal = userAppointmentDal;
        }

        [Key] 
        [DisplayName(nameof(ApptId))]
		public decimal ApptId
		{
			get { return GetProperty(ApptIdProperty); }
			set { SetProperty(ApptIdProperty, value); }
		}
		public static readonly PropertyInfo<decimal> ApptIdProperty = RegisterProperty<decimal>(c => c.ApptId);

        [DisplayName(nameof(UserId))]
		public int? UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> UserIdProperty = RegisterProperty<int?>(c => c.UserId);

        [DisplayName(nameof(PracticeTypeId))]
		public int? PracticeTypeId
		{
			get { return GetProperty(PracticeTypeIdProperty); }
			set { SetProperty(PracticeTypeIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> PracticeTypeIdProperty = RegisterProperty<int?>(c => c.PracticeTypeId);

        [DisplayName(nameof(PracticeType))]
		public string PracticeType
		{
			get { return GetProperty(PracticeTypeProperty); }
			set { SetProperty(PracticeTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> PracticeTypeProperty = RegisterProperty<string>(c => c.PracticeType);

        [DisplayName(nameof(AppointmentTypeId))]
		public int? AppointmentTypeId
		{
			get { return GetProperty(AppointmentTypeIdProperty); }
			set { SetProperty(AppointmentTypeIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> AppointmentTypeIdProperty = RegisterProperty<int?>(c => c.AppointmentTypeId);

        [DisplayName(nameof(PrimaryAppointment))]
		public bool? PrimaryAppointment
		{
			get { return GetProperty(PrimaryAppointmentProperty); }
			set { SetProperty(PrimaryAppointmentProperty, value); }
		}
		public static readonly PropertyInfo<bool?> PrimaryAppointmentProperty = RegisterProperty<bool?>(c => c.PrimaryAppointment);

        [DisplayName(nameof(AppointmentType))]
		public string AppointmentType
		{
			get { return GetProperty(AppointmentTypeProperty); }
			set { SetProperty(AppointmentTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> AppointmentTypeProperty = RegisterProperty<string>(c => c.AppointmentType);

        [DisplayName(nameof(OrganizationTypeId))]
		public int? OrganizationTypeId
		{
			get { return GetProperty(OrganizationTypeIdProperty); }
			set { SetProperty(OrganizationTypeIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> OrganizationTypeIdProperty = RegisterProperty<int?>(c => c.OrganizationTypeId);

        [DisplayName(nameof(AuthorizingOfficial))]
		public string AuthorizingOfficial
		{
			get { return GetProperty(AuthorizingOfficialProperty); }
			set { SetProperty(AuthorizingOfficialProperty, value); }
		}
		public static readonly PropertyInfo<string> AuthorizingOfficialProperty = RegisterProperty<string>(c => c.AuthorizingOfficial);

        [DisplayName(nameof(OrganizationType))]
		public string OrganizationType
		{
			get { return GetProperty(OrganizationTypeProperty); }
			set { SetProperty(OrganizationTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> OrganizationTypeProperty = RegisterProperty<string>(c => c.OrganizationType);

        [DisplayName(nameof(OrganizationId))]
		public int? OrganizationId
		{
			get { return GetProperty(OrganizationIdProperty); }
			set { SetProperty(OrganizationIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> OrganizationIdProperty = RegisterProperty<int?>(c => c.OrganizationId);

        [DisplayName(nameof(StateCode))]
		public string StateCode
		{
			get { return GetProperty(StateCodeProperty); }
			set { SetProperty(StateCodeProperty, value); }
		}
		public static readonly PropertyInfo<string> StateCodeProperty = RegisterProperty<string>(c => c.StateCode);

        [DisplayName(nameof(Other))]
		public string Other
		{
			get { return GetProperty(OtherProperty); }
			set { SetProperty(OtherProperty, value); }
		}
		public static readonly PropertyInfo<string> OtherProperty = RegisterProperty<string>(c => c.Other);

        [DisplayName(nameof(OrganizationName))]
		public string OrganizationName
		{
			get { return GetProperty(OrganizationNameProperty); }
			set { SetProperty(OrganizationNameProperty, value); }
		}
		public static readonly PropertyInfo<string> OrganizationNameProperty = RegisterProperty<string>(c => c.OrganizationName);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(UserAppointment),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
            Csla.Rules.BusinessRules.AddRule(typeof(UserAppointment),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
            Csla.Rules.BusinessRules.AddRule(typeof(UserAppointment),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
            Csla.Rules.BusinessRules.AddRule(typeof(UserAppointment),
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
        
                await _userAppointmentDal.DeleteAsync(ToDto());
        
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
                var dto = await _userAppointmentDal.GetByIdAsync(criteria.ApptId);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("UserAppointment not found based on criteria");
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
                var dto = await _userAppointmentDal.InsertAsync(ToDto());
        
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
                var dto = await _userAppointmentDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(UserAppointmentDto dto)
		{
            base.FetchData(dto);
            
			this.ApptId = dto.ApptId;
			this.UserId = dto.UserId;
			this.PracticeTypeId = dto.PracticeTypeId;
			this.PracticeType = dto.PracticeType;
			this.AppointmentTypeId = dto.AppointmentTypeId;
			this.PrimaryAppointment = dto.PrimaryAppointment;
			this.AppointmentType = dto.AppointmentType;
			this.OrganizationTypeId = dto.OrganizationTypeId;
			this.AuthorizingOfficial = dto.AuthorizingOfficial;
			this.OrganizationType = dto.OrganizationType;
			this.OrganizationId = dto.OrganizationId;
			this.StateCode = dto.StateCode;
			this.Other = dto.Other;
			this.OrganizationName = dto.OrganizationName;
		}

		internal UserAppointmentDto ToDto()
		{
			var dto = new UserAppointmentDto();
			return ToDto(dto);
		}
		internal UserAppointmentDto ToDto(UserAppointmentDto dto)
		{
            base.ToDto(dto);
            
			dto.ApptId = this.ApptId;
			dto.UserId = this.UserId;
			dto.PracticeTypeId = this.PracticeTypeId;
			dto.PracticeType = this.PracticeType;
			dto.AppointmentTypeId = this.AppointmentTypeId;
			dto.PrimaryAppointment = this.PrimaryAppointment;
			dto.AppointmentType = this.AppointmentType;
			dto.OrganizationTypeId = this.OrganizationTypeId;
			dto.AuthorizingOfficial = this.AuthorizingOfficial;
			dto.OrganizationType = this.OrganizationType;
			dto.OrganizationId = this.OrganizationId;
			dto.StateCode = this.StateCode;
			dto.Other = this.Other;
			dto.OrganizationName = this.OrganizationName;

			return dto;
		}


    }
}
