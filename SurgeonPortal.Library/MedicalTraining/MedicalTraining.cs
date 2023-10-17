using Csla;
using Csla.Rules.CommonRules;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.MedicalTraining.MedicalTrainingFactory;

namespace SurgeonPortal.Library.MedicalTraining
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class MedicalTraining : YtgBusinessBase<MedicalTraining>, IMedicalTraining
    {
        private readonly IMedicalTrainingDal _medicalTrainingDal;

        public MedicalTraining(
            IIdentityProvider identityProvider,
            IMedicalTrainingDal medicalTrainingDal)
            : base(identityProvider)
        {
            _medicalTrainingDal = medicalTrainingDal;
        }

        [Key] 
        [DisplayName(nameof(Id))]
		public int Id
		{
			get { return GetProperty(IdProperty); }
			set { SetProperty(IdProperty, value); }
		}
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [Key] 
        [DisplayName(nameof(UserId))]
		public int UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DisplayName(nameof(GraduateProfileId))]
		public int GraduateProfileId
		{
			get { return GetProperty(GraduateProfileIdProperty); }
			set { SetProperty(GraduateProfileIdProperty, value); }
		}
		public static readonly PropertyInfo<int> GraduateProfileIdProperty = RegisterProperty<int>(c => c.GraduateProfileId);

        [DisplayName(nameof(GraduateProfileDescription))]
		public string GraduateProfileDescription
		{
			get { return GetProperty(GraduateProfileDescriptionProperty); }
			set { SetProperty(GraduateProfileDescriptionProperty, value); }
		}
		public static readonly PropertyInfo<string> GraduateProfileDescriptionProperty = RegisterProperty<string>(c => c.GraduateProfileDescription);

        [DisplayName(nameof(MedicalSchoolName))]
		public string MedicalSchoolName
		{
			get { return GetProperty(MedicalSchoolNameProperty); }
			set { SetProperty(MedicalSchoolNameProperty, value); }
		}
		public static readonly PropertyInfo<string> MedicalSchoolNameProperty = RegisterProperty<string>(c => c.MedicalSchoolName);

        [DisplayName(nameof(MedicalSchoolCity))]
		public string MedicalSchoolCity
		{
			get { return GetProperty(MedicalSchoolCityProperty); }
			set { SetProperty(MedicalSchoolCityProperty, value); }
		}
		public static readonly PropertyInfo<string> MedicalSchoolCityProperty = RegisterProperty<string>(c => c.MedicalSchoolCity);

        [DisplayName(nameof(MedicalSchoolStateId))]
		public string MedicalSchoolStateId
		{
			get { return GetProperty(MedicalSchoolStateIdProperty); }
			set { SetProperty(MedicalSchoolStateIdProperty, value); }
		}
		public static readonly PropertyInfo<string> MedicalSchoolStateIdProperty = RegisterProperty<string>(c => c.MedicalSchoolStateId);

        [DisplayName(nameof(MedicalSchoolStateName))]
		public string MedicalSchoolStateName
		{
			get { return GetProperty(MedicalSchoolStateNameProperty); }
			set { SetProperty(MedicalSchoolStateNameProperty, value); }
		}
		public static readonly PropertyInfo<string> MedicalSchoolStateNameProperty = RegisterProperty<string>(c => c.MedicalSchoolStateName);

        [DisplayName(nameof(MedicalSchoolCountryId))]
		public string MedicalSchoolCountryId
		{
			get { return GetProperty(MedicalSchoolCountryIdProperty); }
			set { SetProperty(MedicalSchoolCountryIdProperty, value); }
		}
		public static readonly PropertyInfo<string> MedicalSchoolCountryIdProperty = RegisterProperty<string>(c => c.MedicalSchoolCountryId);

        [DisplayName(nameof(MedicalSchoolCountryName))]
		public string MedicalSchoolCountryName
		{
			get { return GetProperty(MedicalSchoolCountryNameProperty); }
			set { SetProperty(MedicalSchoolCountryNameProperty, value); }
		}
		public static readonly PropertyInfo<string> MedicalSchoolCountryNameProperty = RegisterProperty<string>(c => c.MedicalSchoolCountryName);

        [DisplayName(nameof(DegreeId))]
		public int DegreeId
		{
			get { return GetProperty(DegreeIdProperty); }
			set { SetProperty(DegreeIdProperty, value); }
		}
		public static readonly PropertyInfo<int> DegreeIdProperty = RegisterProperty<int>(c => c.DegreeId);

        [DisplayName(nameof(DegreeName))]
		public string DegreeName
		{
			get { return GetProperty(DegreeNameProperty); }
			set { SetProperty(DegreeNameProperty, value); }
		}
		public static readonly PropertyInfo<string> DegreeNameProperty = RegisterProperty<string>(c => c.DegreeName);

        [DisplayName(nameof(MedicalSchoolCompletionYear))]
		public string MedicalSchoolCompletionYear
		{
			get { return GetProperty(MedicalSchoolCompletionYearProperty); }
			set { SetProperty(MedicalSchoolCompletionYearProperty, value); }
		}
		public static readonly PropertyInfo<string> MedicalSchoolCompletionYearProperty = RegisterProperty<string>(c => c.MedicalSchoolCompletionYear);

        [DisplayName(nameof(ResidencyProgramName))]
		public string ResidencyProgramName
		{
			get { return GetProperty(ResidencyProgramNameProperty); }
			set { SetProperty(ResidencyProgramNameProperty, value); }
		}
		public static readonly PropertyInfo<string> ResidencyProgramNameProperty = RegisterProperty<string>(c => c.ResidencyProgramName);

        [DisplayName(nameof(ResidencyCompletionYear))]
		public string ResidencyCompletionYear
		{
			get { return GetProperty(ResidencyCompletionYearProperty); }
			set { SetProperty(ResidencyCompletionYearProperty, value); }
		}
		public static readonly PropertyInfo<string> ResidencyCompletionYearProperty = RegisterProperty<string>(c => c.ResidencyCompletionYear);

        [DisplayName(nameof(ResidencyProgramOther))]
		public string ResidencyProgramOther
		{
			get { return GetProperty(ResidencyProgramOtherProperty); }
			set { SetProperty(ResidencyProgramOtherProperty, value); }
		}
		public static readonly PropertyInfo<string> ResidencyProgramOtherProperty = RegisterProperty<string>(c => c.ResidencyProgramOther);



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
            BusinessRules.AddRule(new Required(GraduateProfileIdProperty, "GraduateProfileId is required"));
            BusinessRules.AddRule(new Required(MedicalSchoolNameProperty, "MedicalSchoolName is required"));
            BusinessRules.AddRule(new MaxLength(MedicalSchoolNameProperty, 30, @"The MedicalSchoolName cannot be more than 30 characters"));
            BusinessRules.AddRule(new Required(MedicalSchoolCityProperty, "MedicalSchoolCity is required"));
            BusinessRules.AddRule(new MaxLength(MedicalSchoolCityProperty, 30, @"The MedicalSchoolCity cannot be more than 30 characters"));
            BusinessRules.AddRule(new Required(MedicalSchoolCountryIdProperty, "MedicalSchoolCountryId is required"));
            BusinessRules.AddRule(new Required(DegreeIdProperty, "DegreeId is required"));
            BusinessRules.AddRule(new Required(MedicalSchoolCompletionYearProperty, "MedicalSchoolCompletionYear is required"));
            BusinessRules.AddRule(new RegExMatch(MedicalSchoolCompletionYearProperty, @"^\d{4}$", @"The medical school completion year doesn't match the required pattern (YYYY)"));
            BusinessRules.AddRule(new Required(ResidencyProgramNameProperty, "ResidencyProgramName is required"));
            BusinessRules.AddRule(new Required(ResidencyCompletionYearProperty, "ResidencyCompletionYear is required"));
            BusinessRules.AddRule(new RegExMatch(ResidencyCompletionYearProperty, @"^\d{4}$", @"The residency completion year doesn't match the required pattern (YYYY)"));
            BusinessRules.AddRule(new Required(ResidencyProgramOtherProperty, "ResidencyProgramOther is required"));
        }


        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _medicalTrainingDal.GetByUserIdAsync(_identity.GetUserId<int>());
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("MedicalTraining not found based on criteria");
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
                var dto = await _medicalTrainingDal.InsertAsync(ToDto());
        
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
                var dto = await _medicalTrainingDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(MedicalTrainingDto dto)
		{
            base.FetchData(dto);
            
			this.Id = dto.Id;
			this.UserId = dto.UserId;
			this.GraduateProfileId = dto.GraduateProfileId;
			this.GraduateProfileDescription = dto.GraduateProfileDescription;
			this.MedicalSchoolName = dto.MedicalSchoolName;
			this.MedicalSchoolCity = dto.MedicalSchoolCity;
			this.MedicalSchoolStateId = dto.MedicalSchoolStateId;
			this.MedicalSchoolStateName = dto.MedicalSchoolStateName;
			this.MedicalSchoolCountryId = dto.MedicalSchoolCountryId;
			this.MedicalSchoolCountryName = dto.MedicalSchoolCountryName;
			this.DegreeId = dto.DegreeId;
			this.DegreeName = dto.DegreeName;
			this.MedicalSchoolCompletionYear = dto.MedicalSchoolCompletionYear;
			this.ResidencyProgramName = dto.ResidencyProgramName;
			this.ResidencyCompletionYear = dto.ResidencyCompletionYear;
			this.ResidencyProgramOther = dto.ResidencyProgramOther;
		}

		internal MedicalTrainingDto ToDto()
		{
			var dto = new MedicalTrainingDto();
			return ToDto(dto);
		}
		internal MedicalTrainingDto ToDto(MedicalTrainingDto dto)
		{
            base.ToDto(dto);
            
			dto.Id = this.Id;
			dto.UserId = this.UserId;
			dto.GraduateProfileId = this.GraduateProfileId;
			dto.GraduateProfileDescription = this.GraduateProfileDescription;
			dto.MedicalSchoolName = this.MedicalSchoolName;
			dto.MedicalSchoolCity = this.MedicalSchoolCity;
			dto.MedicalSchoolStateId = this.MedicalSchoolStateId;
			dto.MedicalSchoolStateName = this.MedicalSchoolStateName;
			dto.MedicalSchoolCountryId = this.MedicalSchoolCountryId;
			dto.MedicalSchoolCountryName = this.MedicalSchoolCountryName;
			dto.DegreeId = this.DegreeId;
			dto.DegreeName = this.DegreeName;
			dto.MedicalSchoolCompletionYear = this.MedicalSchoolCompletionYear;
			dto.ResidencyProgramName = this.ResidencyProgramName;
			dto.ResidencyCompletionYear = this.ResidencyCompletionYear;
			dto.ResidencyProgramOther = this.ResidencyProgramOther;

			return dto;
		}


    }
}
