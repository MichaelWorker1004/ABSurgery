using Csla;
using Csla.Core;
using Csla.Rules;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using Ytg.Framework.Rules;
using Ytg.Framework.Rules.Dates;
using static SurgeonPortal.Library.GraduateMedicalEducation.RotationFactory;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class Rotation : YtgBusinessBase<Rotation>, IRotation
    {
        private readonly IRotationDal _rotationDal;
		readonly IOverlapConflictCommandFactory _overlapConflictCommandFactory;
        public Rotation(IOverlapConflictCommandFactory overlapConflictCommandFactory,
            IIdentityProvider identityProvider,
            IRotationDal rotationDal)
            : base(identityProvider)
        {
            _rotationDal = rotationDal;
			_overlapConflictCommandFactory = overlapConflictCommandFactory;
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

        [DisplayName(nameof(StartDate))]
		public DateTime StartDate
		{
			get { return GetProperty(StartDateProperty); }
			set
            {
                SetProperty(StartDateProperty, value);
                BusinessRules.CheckObjectRules();
            }
		}
		public static readonly PropertyInfo<DateTime> StartDateProperty = RegisterProperty<DateTime>(c => c.StartDate);

        [DisplayName(nameof(EndDate))]
		public DateTime EndDate
		{
			get { return GetProperty(EndDateProperty); }
			set 
			{ 
				SetProperty(EndDateProperty, value);
                BusinessRules.CheckObjectRules();
            }
		}
		public static readonly PropertyInfo<DateTime> EndDateProperty = RegisterProperty<DateTime>(c => c.EndDate);

        [DisplayName(nameof(ClinicalLevelId))]
		public int ClinicalLevelId
		{
			get { return GetProperty(ClinicalLevelIdProperty); }
			set { SetProperty(ClinicalLevelIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ClinicalLevelIdProperty = RegisterProperty<int>(c => c.ClinicalLevelId);

        [DisplayName(nameof(ClinicalLevel))]
		public string ClinicalLevel
		{
			get { return GetProperty(ClinicalLevelProperty); }
			set { SetProperty(ClinicalLevelProperty, value); }
		}
		public static readonly PropertyInfo<string> ClinicalLevelProperty = RegisterProperty<string>(c => c.ClinicalLevel);

        [DisplayName(nameof(ClinicalActivityId))]
		public int ClinicalActivityId
		{
			get { return GetProperty(ClinicalActivityIdProperty); }
			set { SetProperty(ClinicalActivityIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ClinicalActivityIdProperty = RegisterProperty<int>(c => c.ClinicalActivityId);

        [DisplayName(nameof(ProgramName))]
		public string ProgramName
		{
			get { return GetProperty(ProgramNameProperty); }
			set { SetProperty(ProgramNameProperty, value); }
		}
		public static readonly PropertyInfo<string> ProgramNameProperty = RegisterProperty<string>(c => c.ProgramName);

        [DisplayName(nameof(NonSurgicalActivity))]
		public string NonSurgicalActivity
		{
			get { return GetProperty(NonSurgicalActivityProperty); }
			set { SetProperty(NonSurgicalActivityProperty, value); }
		}
		public static readonly PropertyInfo<string> NonSurgicalActivityProperty = RegisterProperty<string>(c => c.NonSurgicalActivity);

        [DisplayName(nameof(AlternateInstitutionName))]
		public string AlternateInstitutionName
		{
			get { return GetProperty(AlternateInstitutionNameProperty); }
			set { SetProperty(AlternateInstitutionNameProperty, value); }
		}
		public static readonly PropertyInfo<string> AlternateInstitutionNameProperty = RegisterProperty<string>(c => c.AlternateInstitutionName);

        [DisplayName(nameof(IsInternationalRotation))]
		public bool IsInternationalRotation
		{
			get { return GetProperty(IsInternationalRotationProperty); }
			set { SetProperty(IsInternationalRotationProperty, value); }
		}
		public static readonly PropertyInfo<bool> IsInternationalRotationProperty = RegisterProperty<bool>(c => c.IsInternationalRotation);

        [DisplayName(nameof(IsEssential))]
		public bool IsEssential
		{
			get { return GetProperty(IsEssentialProperty); }
			set { SetProperty(IsEssentialProperty, value); }
		}
		public static readonly PropertyInfo<bool> IsEssentialProperty = RegisterProperty<bool>(c => c.IsEssential);

        [DisplayName(nameof(IsCredit))]
		public bool IsCredit
		{
			get { return GetProperty(IsCreditProperty); }
			set { SetProperty(IsCreditProperty, value); }
		}
		public static readonly PropertyInfo<bool> IsCreditProperty = RegisterProperty<bool>(c => c.IsCredit);

        [DisplayName(nameof(Other))]
		public string Other
		{
			get { return GetProperty(OtherProperty); }
			set { SetProperty(OtherProperty, value); }
		}
		public static readonly PropertyInfo<string> OtherProperty = RegisterProperty<string>(c => c.Other);

        [DisplayName(nameof(FourMonthRotationExplain))]
		public string FourMonthRotationExplain
		{
			get { return GetProperty(FourMonthRotationExplainProperty); }
			set { SetProperty(FourMonthRotationExplainProperty, value); }
		}
		public static readonly PropertyInfo<string> FourMonthRotationExplainProperty = RegisterProperty<string>(c => c.FourMonthRotationExplain);

        [DisplayName(nameof(NonPrimaryExplain))]
		public string NonPrimaryExplain
		{
			get { return GetProperty(NonPrimaryExplainProperty); }
			set { SetProperty(NonPrimaryExplainProperty, value); }
		}
		public static readonly PropertyInfo<string> NonPrimaryExplainProperty = RegisterProperty<string>(c => c.NonPrimaryExplain);

        [DisplayName(nameof(NonClinicalExplain))]
		public string NonClinicalExplain
		{
			get { return GetProperty(NonClinicalExplainProperty); }
			set { SetProperty(NonClinicalExplainProperty, value); }
		}
		public static readonly PropertyInfo<string> NonClinicalExplainProperty = RegisterProperty<string>(c => c.NonClinicalExplain);

        [DisplayName(nameof(ClinicalActivity))]
		public string ClinicalActivity
		{
			get { return GetProperty(ClinicalActivityProperty); }
			set { SetProperty(ClinicalActivityProperty, value); }
		}
		public static readonly PropertyInfo<string> ClinicalActivityProperty = RegisterProperty<string>(c => c.ClinicalActivity);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(Rotation),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim));
            Csla.Rules.BusinessRules.AddRule(typeof(Rotation),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim));
            Csla.Rules.BusinessRules.AddRule(typeof(Rotation),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim));
            Csla.Rules.BusinessRules.AddRule(typeof(Rotation),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim));
        }
        protected override void AddInjectedBusinessRules()
        {
            BusinessRules.AddRule(new OverlapConflictRule(_overlapConflictCommandFactory, StartDateProperty, EndDateProperty, UserIdProperty, 5));
        }
        /// <summary>
        /// This method is used to add business rules to the Csla 
        /// business rule engine
        /// </summary>
        protected override void AddBusinessRules()
        {
            // Only process priority 5 and higher if all 4 and lower completed first
            BusinessRules.ProcessThroughPriority = 4;

            BusinessRules.AddRule(new DateGreaterThanRule(EndDateProperty, StartDateProperty));
            BusinessRules.AddRule(new DateLessThanRule(StartDateProperty, EndDateProperty));
			BusinessRules.AddRule(new MaxDurationBetweenDatesRule(StartDateProperty, EndDateProperty, 364, 1));
			BusinessRules.AddRule(new MinDurationBetweenDatesRule(StartDateProperty, EndDateProperty, 2, 1));
			BusinessRules.AddRule(new ExplainRequiredWhenRule(OtherProperty, 4));
			BusinessRules.AddRule(new FourMonthRotationExplainRequiredWhen(FourMonthRotationExplainProperty, 4));
			BusinessRules.AddRule(new NonPrimaryExplainRequiredWhen(NonPrimaryExplainProperty, 4));
			BusinessRules.AddRule(new NonClinicalExplainRequiredWhen(NonClinicalExplainProperty, 4));
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
        
                await _rotationDal.DeleteAsync(ToDto());
        
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
                var dto = await _rotationDal.GetByIdAsync(criteria.Id);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("Rotation not found based on criteria");
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
                var dto = await _rotationDal.InsertAsync(ToDto());
        
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
                var dto = await _rotationDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(RotationDto dto)
		{
            base.FetchData(dto);
            
			this.Id = dto.Id;
			this.UserId = dto.UserId;
			this.StartDate = dto.StartDate;
			this.EndDate = dto.EndDate;
			this.ClinicalLevelId = dto.ClinicalLevelId;
			this.ClinicalLevel = dto.ClinicalLevel;
			this.ClinicalActivityId = dto.ClinicalActivityId;
			this.ProgramName = dto.ProgramName;
			this.NonSurgicalActivity = dto.NonSurgicalActivity;
			this.AlternateInstitutionName = dto.AlternateInstitutionName;
			this.IsInternationalRotation = dto.IsInternationalRotation;
			this.IsEssential = dto.IsEssential;
			this.IsCredit = dto.IsCredit;
			this.Other = dto.Other;
			this.FourMonthRotationExplain = dto.FourMonthRotationExplain;
			this.NonPrimaryExplain = dto.NonPrimaryExplain;
			this.NonClinicalExplain = dto.NonClinicalExplain;
			this.ClinicalActivity = dto.ClinicalActivity;
		}

		internal RotationDto ToDto()
		{
			var dto = new RotationDto();
			return ToDto(dto);
		}
		internal RotationDto ToDto(RotationDto dto)
		{
            base.ToDto(dto);
            
			dto.Id = this.Id;
			dto.UserId = this.UserId;
			dto.StartDate = this.StartDate;
			dto.EndDate = this.EndDate;
			dto.ClinicalLevelId = this.ClinicalLevelId;
			dto.ClinicalLevel = this.ClinicalLevel;
			dto.ClinicalActivityId = this.ClinicalActivityId;
			dto.ProgramName = this.ProgramName;
			dto.NonSurgicalActivity = this.NonSurgicalActivity;
			dto.AlternateInstitutionName = this.AlternateInstitutionName;
			dto.IsInternationalRotation = this.IsInternationalRotation;
			dto.IsEssential = this.IsEssential;
			dto.IsCredit = this.IsCredit;
			dto.Other = this.Other;
			dto.FourMonthRotationExplain = this.FourMonthRotationExplain;
			dto.NonPrimaryExplain = this.NonPrimaryExplain;
			dto.NonClinicalExplain = this.NonClinicalExplain;
			dto.ClinicalActivity = this.ClinicalActivity;

			return dto;
		}

		private class FourMonthRotationExplainRequiredWhen : BusinessRule
		{
			public FourMonthRotationExplainRequiredWhen(IPropertyInfo primaryProperty,
				int priority) : base(primaryProperty)
			{
				Priority = priority;
				InputProperties = new List<IPropertyInfo> { primaryProperty };
			}

			protected override void Execute(IRuleContext context)
			{
				var explain = (string)context.InputPropertyValues[PrimaryProperty];
				
				var target = context.Target as Rotation;
				var startDate = target.StartDate;
				var endDate = target.EndDate;
				var clinicalLevelId = target.ClinicalLevelId;

				if(clinicalLevelId == (int)ClinicalLevels.ClinicalLevel4Chief || clinicalLevelId == (int)ClinicalLevels.ClinicalLevel5Chief)
				{
					var duration = endDate - startDate;
					if(duration.Days + 1 > 120 && string.IsNullOrEmpty(explain))
					{
						context.AddErrorResult(PrimaryProperty, "FourMonthRotationExplain is required when the duration is over 4 months and clinical level is 4 Chief or 5 Chief");
					}
				}
			}
		}

		private class NonClinicalExplainRequiredWhen : BusinessRule
		{
			public NonClinicalExplainRequiredWhen(IPropertyInfo primaryProperty,
				int priority) : base(primaryProperty)
			{
				Priority = priority;
				InputProperties = new List<IPropertyInfo> { primaryProperty };
			}

			protected override void Execute(IRuleContext context)
			{
				var explain = (string)context.InputPropertyValues[PrimaryProperty];

				var target = context.Target as Rotation;
				var clinicalLevelId = target.ClinicalLevelId;
				var clinicalActivityId = target.ClinicalActivityId;

				if(clinicalLevelId == (int)ClinicalLevels.ClinicalLevel4 || clinicalLevelId == (int)ClinicalLevels.ClinicalLevel5)
				{
					if(clinicalActivityId == (int)ClinicalActivities.NonClinicalResearch || clinicalActivityId == (int)ClinicalActivities.ClinicalNonSurgical)
					{
						if(string.IsNullOrEmpty(explain))
						{
							context.AddErrorResult(PrimaryProperty, "NonClinicalExplain is required when the clinical level is 4 or 5 and the clinical activity is Non-Clinical Research or Clinical (Non-Surgical)");
						}
					}
				}
			}
		}

		private class NonPrimaryExplainRequiredWhen : BusinessRule
		{
			public NonPrimaryExplainRequiredWhen(IPropertyInfo primaryProperty,
				int priority) : base(primaryProperty)
			{
				Priority = priority;
				InputProperties = new List<IPropertyInfo> { primaryProperty };
			}

			protected override void Execute(IRuleContext context)
			{
				var explain = (string)context.InputPropertyValues[PrimaryProperty];

				var target = context.Target as Rotation;
				var isEssential = target.IsEssential;
				var clinicalLevelId = target.ClinicalLevelId;

				if(string.IsNullOrEmpty(explain) && !isEssential && 
					(clinicalLevelId == (int)ClinicalLevels.ClinicalLevel4Chief || clinicalLevelId == (int)ClinicalLevels.ClinicalLevel5Chief))
				{
					context.AddErrorResult(PrimaryProperty, "NonPrimaryExplain when isEssential is false and clinical level is 4 Chief or 5 Chief");
				}
			}
		}
    }
}
