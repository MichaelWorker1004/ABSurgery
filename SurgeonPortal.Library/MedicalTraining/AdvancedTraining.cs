using Csla;
using Csla.Rules.CommonRules;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using SurgeonPortal.Library.Users;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using Ytg.Framework.Rules;
using Ytg.Framework.Rules.Dates;
using static SurgeonPortal.Library.MedicalTraining.AdvancedTrainingFactory;

namespace SurgeonPortal.Library.MedicalTraining
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class AdvancedTraining : YtgBusinessBase<AdvancedTraining>, IAdvancedTraining
    {
        private readonly IAdvancedTrainingDal _advancedTrainingDal;

        public AdvancedTraining(
            IIdentityProvider identityProvider,
            IAdvancedTrainingDal advancedTrainingDal)
            : base(identityProvider)
        {
            _advancedTrainingDal = advancedTrainingDal;
        }

        [Key] 
        [DisplayName(nameof(Id))]
		public int Id
		{
			get { return GetProperty(IdProperty); }
			 private set { SetProperty(IdProperty, value); }
		}
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DisplayName(nameof(UserId))]
		public int UserId
		{
			get { return GetProperty(UserIdProperty); }
			 private set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DisplayName(nameof(TrainingTypeId))]
		public int? TrainingTypeId
		{
			get { return GetProperty(TrainingTypeIdProperty); }
			set { SetProperty(TrainingTypeIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> TrainingTypeIdProperty = RegisterProperty<int?>(c => c.TrainingTypeId);

        [DisplayName(nameof(TrainingType))]
		public string TrainingType
		{
			get { return GetProperty(TrainingTypeProperty); }
			 private set { SetProperty(TrainingTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> TrainingTypeProperty = RegisterProperty<string>(c => c.TrainingType);

        [DisplayName("Program Instituition")]
		public int? ProgramId
		{
			get { return GetProperty(ProgramIdProperty); }
			set { SetProperty(ProgramIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> ProgramIdProperty = RegisterProperty<int?>(c => c.ProgramId);

        [DisplayName(nameof(InstitutionName))]
		public string InstitutionName
		{
			get { return GetProperty(InstitutionNameProperty); }
			 private set { SetProperty(InstitutionNameProperty, value); }
		}
		public static readonly PropertyInfo<string> InstitutionNameProperty = RegisterProperty<string>(c => c.InstitutionName);

        [DisplayName(nameof(City))]
		public string City
		{
			get { return GetProperty(CityProperty); }
			 private set { SetProperty(CityProperty, value); }
		}
		public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(c => c.City);

        [DisplayName(nameof(State))]
		public string State
		{
			get { return GetProperty(StateProperty); }
			 private set { SetProperty(StateProperty, value); }
		}
		public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(c => c.State);

        [DisplayName("Other Institution")]
		public string Other
		{
			get { return GetProperty(OtherProperty); }
			set { SetProperty(OtherProperty, value); }
		}
		public static readonly PropertyInfo<string> OtherProperty = RegisterProperty<string>(c => c.Other);

        [DisplayName(nameof(StartDate))]
		public DateTime? StartDate
		{
			get { return GetProperty(StartDateProperty); }
			set { SetProperty(StartDateProperty, value); }
		}
		public static readonly PropertyInfo<DateTime?> StartDateProperty = RegisterProperty<DateTime?>(c => c.StartDate);

        [DisplayName(nameof(EndDate))]
		public DateTime? EndDate
		{
			get { return GetProperty(EndDateProperty); }
			set { SetProperty(EndDateProperty, value); }
		}
		public static readonly PropertyInfo<DateTime?> EndDateProperty = RegisterProperty<DateTime?>(c => c.EndDate);



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

            BusinessRules.AddRule(new Required(TrainingTypeIdProperty, "TrainingTypeId is required"));
            BusinessRules.AddRule(new Required(StartDateProperty, "StartDate is required"));
            BusinessRules.AddRule(new Required(EndDateProperty, "EndDate is required"));
			BusinessRules.AddRule(new DateGreaterThanRule(EndDateProperty, StartDateProperty));
            BusinessRules.AddRule(new DateLessThanRule(StartDateProperty, EndDateProperty));
            BusinessRules.AddRule(new EitherOrRequiredRule(ProgramIdProperty, OtherProperty, 1));
        }


        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByTrainingId(GetByTrainingIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _advancedTrainingDal.GetByTrainingIdAsync(criteria.Id);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("AdvancedTraining not found based on criteria");
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
                var dto = await _advancedTrainingDal.InsertAsync(ToDto());
        
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
                var dto = await _advancedTrainingDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(AdvancedTrainingDto dto)
		{
            base.FetchData(dto);
            
			this.Id = dto.Id;
			this.UserId = dto.UserId;
			this.TrainingTypeId = dto.TrainingTypeId;
			this.TrainingType = dto.TrainingType;
			this.ProgramId = dto.ProgramId;
			this.InstitutionName = dto.InstitutionName;
			this.City = dto.City;
			this.State = dto.State;
			this.Other = dto.Other;
			this.StartDate = dto.StartDate;
			this.EndDate = dto.EndDate;
		}

		internal AdvancedTrainingDto ToDto()
		{
			var dto = new AdvancedTrainingDto();
			return ToDto(dto);
		}
		internal AdvancedTrainingDto ToDto(AdvancedTrainingDto dto)
		{
            base.ToDto(dto);
            
			dto.Id = this.Id;
			dto.UserId = this.UserId;
			dto.TrainingTypeId = this.TrainingTypeId;
			dto.TrainingType = this.TrainingType;
			dto.ProgramId = this.ProgramId;
			dto.InstitutionName = this.InstitutionName;
			dto.City = this.City;
			dto.State = this.State;
			dto.Other = this.Other;
			dto.StartDate = this.StartDate;
			dto.EndDate = this.EndDate;

			return dto;
		}


    }
}
