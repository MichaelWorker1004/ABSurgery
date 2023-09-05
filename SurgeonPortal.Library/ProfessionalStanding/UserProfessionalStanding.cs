using Csla;
using Csla.Core;
using Csla.Rules;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.ProfessionalStanding.UserProfessionalStandingFactory;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class UserProfessionalStanding : YtgBusinessBase<UserProfessionalStanding>, IUserProfessionalStanding
    {
        private readonly IUserProfessionalStandingDal _userProfessionalStandingDal;

        public UserProfessionalStanding(
            IIdentityProvider identityProvider,
            IUserProfessionalStandingDal userProfessionalStandingDal)
            : base(identityProvider)
        {
            _userProfessionalStandingDal = userProfessionalStandingDal;
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

        [DisplayName(nameof(PrimaryPracticeId))]
		public int? PrimaryPracticeId
		{
			get { return GetProperty(PrimaryPracticeIdProperty); }
			set { SetProperty(PrimaryPracticeIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> PrimaryPracticeIdProperty = RegisterProperty<int?>(c => c.PrimaryPracticeId);

        [DisplayName(nameof(PrimaryPractice))]
		public string PrimaryPractice
		{
			get { return GetProperty(PrimaryPracticeProperty); }
			set { SetProperty(PrimaryPracticeProperty, value); }
		}
		public static readonly PropertyInfo<string> PrimaryPracticeProperty = RegisterProperty<string>(c => c.PrimaryPractice);

        [DisplayName(nameof(OrganizationTypeId))]
		public int? OrganizationTypeId
		{
			get { return GetProperty(OrganizationTypeIdProperty); }
			set { SetProperty(OrganizationTypeIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> OrganizationTypeIdProperty = RegisterProperty<int?>(c => c.OrganizationTypeId);

        [DisplayName(nameof(OrganizationType))]
		public string OrganizationType
		{
			get { return GetProperty(OrganizationTypeProperty); }
			set { SetProperty(OrganizationTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> OrganizationTypeProperty = RegisterProperty<string>(c => c.OrganizationType);

        [DisplayName(nameof(ExplanationOfNonPrivileges))]
		public string ExplanationOfNonPrivileges
		{
			get { return GetProperty(ExplanationOfNonPrivilegesProperty); }
			set { SetProperty(ExplanationOfNonPrivilegesProperty, value); }
		}
		public static readonly PropertyInfo<string> ExplanationOfNonPrivilegesProperty = RegisterProperty<string>(c => c.ExplanationOfNonPrivileges);

        [DisplayName(nameof(ExplanationOfNonClinicalActivities))]
		public string ExplanationOfNonClinicalActivities
		{
			get { return GetProperty(ExplanationOfNonClinicalActivitiesProperty); }
			set { SetProperty(ExplanationOfNonClinicalActivitiesProperty, value); }
		}
		public static readonly PropertyInfo<string> ExplanationOfNonClinicalActivitiesProperty = RegisterProperty<string>(c => c.ExplanationOfNonClinicalActivities);

        [DisplayName(nameof(ClinicallyActive))]
		public int ClinicallyActive
		{
			get { return GetProperty(ClinicallyActiveProperty); }
			set { SetProperty(ClinicallyActiveProperty, value); }
		}
		public static readonly PropertyInfo<int> ClinicallyActiveProperty = RegisterProperty<int>(c => c.ClinicallyActive);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(UserProfessionalStanding),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(UserProfessionalStanding),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(UserProfessionalStanding),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

        }




        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _userProfessionalStandingDal.GetByUserIdAsync();
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("UserProfessionalStanding not found based on criteria");
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
                var dto = await _userProfessionalStandingDal.InsertAsync(ToDto());
        
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
                var dto = await _userProfessionalStandingDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(UserProfessionalStandingDto dto)
		{
            base.FetchData(dto);
            
			this.Id = dto.Id;
			this.UserId = dto.UserId;
			this.PrimaryPracticeId = dto.PrimaryPracticeId;
			this.PrimaryPractice = dto.PrimaryPractice;
			this.OrganizationTypeId = dto.OrganizationTypeId;
			this.OrganizationType = dto.OrganizationType;
			this.ExplanationOfNonPrivileges = dto.ExplanationOfNonPrivileges;
			this.ExplanationOfNonClinicalActivities = dto.ExplanationOfNonClinicalActivities;
			this.ClinicallyActive = dto.ClinicallyActive;
		}

		internal UserProfessionalStandingDto ToDto()
		{
			var dto = new UserProfessionalStandingDto();
			return ToDto(dto);
		}
		internal UserProfessionalStandingDto ToDto(UserProfessionalStandingDto dto)
		{
            base.ToDto(dto);
            
			dto.Id = this.Id;
			dto.UserId = this.UserId;
			dto.PrimaryPracticeId = this.PrimaryPracticeId;
			dto.PrimaryPractice = this.PrimaryPractice;
			dto.OrganizationTypeId = this.OrganizationTypeId;
			dto.OrganizationType = this.OrganizationType;
			dto.ExplanationOfNonPrivileges = this.ExplanationOfNonPrivileges;
			dto.ExplanationOfNonClinicalActivities = this.ExplanationOfNonClinicalActivities;
			dto.ClinicallyActive = this.ClinicallyActive;

			return dto;
		}

        private class ClinicallyActiveRequiresRule : BusinessRule
        {
            private IGetClinicallyActiveCommandFactory _getClinicallyActiveCommandFactory;

            public ClinicallyActiveRequiresRule(IPropertyInfo primaryProperty, int priority)
                : base(primaryProperty)
            {
                _getClinicallyActiveCommandFactory = new GetClinicallyActiveCommandFactory();
                Priority = priority;
                InputProperties = new List<IPropertyInfo> { primaryProperty };
            }

			protected override void Execute(IRuleContext context)
			{
                var command = _getClinicallyActiveCommandFactory.GetClinicallyActiveByUserId();
                
                var target = context.Target as UserProfessionalStanding;

                if (command.ClinicallyActive.HasValue && command.ClinicallyActive.Value)
                {
                    if (!target.PrimaryPracticeId.HasValue)
                    {
                        context.AddErrorResult(PrimaryProperty, "Primary practice is required when clinically active is true");
                    }

                    if (!target.OrganizationTypeId.HasValue)
                    {
                        context.AddErrorResult(PrimaryProperty, "Organization type is required when clinically active is true");
                    }
                }
                else
                {
                    if(string.IsNullOrEmpty(target.ExplanationOfNonPrivileges))
                    {
                        context.AddErrorResult(PrimaryProperty, "Explanation of non-privaleges is required when clinically active is false");
                    }

                    if(string.IsNullOrEmpty(target.ExplanationOfNonClinicalActivities))
                    {
						context.AddErrorResult(PrimaryProperty, "Explanation of non-clinical activities is required when clinically active is false");
					}
                }
			}
		}
    }
}
