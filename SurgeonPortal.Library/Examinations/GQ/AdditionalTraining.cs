using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations.GQ;
using SurgeonPortal.Library.Contracts.Examinations.GQ;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Examinations.GQ.AdditionalTrainingFactory;

namespace SurgeonPortal.Library.Examinations.GQ
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class AdditionalTraining : YtgBusinessBase<AdditionalTraining>, IAdditionalTraining
    {
        private readonly IAdditionalTrainingDal _additionalTrainingDal;

        public AdditionalTraining(
            IIdentityProvider identityProvider,
            IAdditionalTrainingDal additionalTrainingDal)
            : base(identityProvider)
        {
            _additionalTrainingDal = additionalTrainingDal;
        }

        [Key] 
        [DisplayName(nameof(TrainingId))]
		public int TrainingId
		{
			get { return GetProperty(TrainingIdProperty); }
			set { SetProperty(TrainingIdProperty, value); }
		}
		public static readonly PropertyInfo<int> TrainingIdProperty = RegisterProperty<int>(c => c.TrainingId);

        [DisplayName(nameof(DateEnded))]
		public DateTime DateEnded
		{
			get { return GetProperty(DateEndedProperty); }
			set { SetProperty(DateEndedProperty, value); }
		}
		public static readonly PropertyInfo<DateTime> DateEndedProperty = RegisterProperty<DateTime>(c => c.DateEnded);

        [DisplayName(nameof(DateStarted))]
		public DateTime DateStarted
		{
			get { return GetProperty(DateStartedProperty); }
			set { SetProperty(DateStartedProperty, value); }
		}
		public static readonly PropertyInfo<DateTime> DateStartedProperty = RegisterProperty<DateTime>(c => c.DateStarted);

        [DisplayName(nameof(Other))]
		public string Other
		{
			get { return GetProperty(OtherProperty); }
			set { SetProperty(OtherProperty, value); }
		}
		public static readonly PropertyInfo<string> OtherProperty = RegisterProperty<string>(c => c.Other);

        [DisplayName(nameof(InstitutionId))]
		public int InstitutionId
		{
			get { return GetProperty(InstitutionIdProperty); }
			set { SetProperty(InstitutionIdProperty, value); }
		}
		public static readonly PropertyInfo<int> InstitutionIdProperty = RegisterProperty<int>(c => c.InstitutionId);

        [DisplayName(nameof(InstitutionName))]
		public string InstitutionName
		{
			get { return GetProperty(InstitutionNameProperty); }
			set { SetProperty(InstitutionNameProperty, value); }
		}
		public static readonly PropertyInfo<string> InstitutionNameProperty = RegisterProperty<string>(c => c.InstitutionName);

        [DisplayName(nameof(City))]
		public string City
		{
			get { return GetProperty(CityProperty); }
			set { SetProperty(CityProperty, value); }
		}
		public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(c => c.City);

        [DisplayName(nameof(StateId))]
		public string StateId
		{
			get { return GetProperty(StateIdProperty); }
			set { SetProperty(StateIdProperty, value); }
		}
		public static readonly PropertyInfo<string> StateIdProperty = RegisterProperty<string>(c => c.StateId);

        [DisplayName(nameof(State))]
		public string State
		{
			get { return GetProperty(StateProperty); }
			set { SetProperty(StateProperty, value); }
		}
		public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(c => c.State);

        [DisplayName(nameof(TypeOfTraining))]
		public string TypeOfTraining
		{
			get { return GetProperty(TypeOfTrainingProperty); }
			set { SetProperty(TypeOfTrainingProperty, value); }
		}
		public static readonly PropertyInfo<string> TypeOfTrainingProperty = RegisterProperty<string>(c => c.TypeOfTraining);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(AdditionalTraining),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(AdditionalTraining),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(AdditionalTraining),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

        }




        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByTrainingId(GetByTrainingIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _additionalTrainingDal.GetByTrainingIdAsync(criteria.TrainingId);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("AdditionalTraining not found based on criteria");
                }
                FetchData(dto);
            }
        }

        [Create]
        private void Create()
        {
            base.DataPortal_Create();
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
                var dto = await _additionalTrainingDal.InsertAsync(ToDto());
        
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
                var dto = await _additionalTrainingDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(AdditionalTrainingDto dto)
		{
            base.FetchData(dto);
            
			this.TrainingId = dto.TrainingId;
			this.DateEnded = dto.DateEnded;
			this.DateStarted = dto.DateStarted;
			this.Other = dto.Other;
			this.InstitutionId = dto.InstitutionId;
			this.InstitutionName = dto.InstitutionName;
			this.City = dto.City;
			this.StateId = dto.StateId;
			this.State = dto.State;
			this.TypeOfTraining = dto.TypeOfTraining;
		}

		internal AdditionalTrainingDto ToDto()
		{
			var dto = new AdditionalTrainingDto();
			return ToDto(dto);
		}
		internal AdditionalTrainingDto ToDto(AdditionalTrainingDto dto)
		{
            base.ToDto(dto);
            
			dto.TrainingId = this.TrainingId;
			dto.DateEnded = this.DateEnded;
			dto.DateStarted = this.DateStarted;
			dto.Other = this.Other;
			dto.InstitutionId = this.InstitutionId;
			dto.InstitutionName = this.InstitutionName;
			dto.City = this.City;
			dto.StateId = this.StateId;
			dto.State = this.State;
			dto.TypeOfTraining = this.TypeOfTraining;

			return dto;
		}


    }
}
