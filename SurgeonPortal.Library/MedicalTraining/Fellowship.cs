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
using static SurgeonPortal.Library.MedicalTraining.FellowshipFactory;

namespace SurgeonPortal.Library.MedicalTraining
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class Fellowship : YtgBusinessBase<Fellowship>, IFellowship
    {
        private readonly IFellowshipDal _fellowshipDal;

        public Fellowship(
            IIdentityProvider identityProvider,
            IFellowshipDal fellowshipDal)
            : base(identityProvider)
        {
            _fellowshipDal = fellowshipDal;
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

        [DisplayName(nameof(ProgramName))]
		public string ProgramName
		{
			get { return GetProperty(ProgramNameProperty); }
			set { SetProperty(ProgramNameProperty, value); }
		}
		public static readonly PropertyInfo<string> ProgramNameProperty = RegisterProperty<string>(c => c.ProgramName);

        [DisplayName(nameof(CompletionYear))]
		public string CompletionYear
		{
			get { return GetProperty(CompletionYearProperty); }
			set { SetProperty(CompletionYearProperty, value); }
		}
		public static readonly PropertyInfo<string> CompletionYearProperty = RegisterProperty<string>(c => c.CompletionYear);

        [DisplayName(nameof(ProgramOther))]
		public string ProgramOther
		{
			get { return GetProperty(ProgramOtherProperty); }
			set { SetProperty(ProgramOtherProperty, value); }
		}
		public static readonly PropertyInfo<string> ProgramOtherProperty = RegisterProperty<string>(c => c.ProgramOther);



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

            BusinessRules.AddRule(new Required(ProgramNameProperty, "ProgramName is required"));
            BusinessRules.AddRule(new MaxLength(ProgramOtherProperty, 8000, @"The ProgramOther cannot be more than 8000 characters"));
            BusinessRules.AddRule(new Ytg.Framework.Rules.YearIsValidAndLessThanCurrentYearRule(CompletionYearProperty));
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
        
                await _fellowshipDal.DeleteAsync(ToDto());
        
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
                var dto = await _fellowshipDal.GetByIdAsync(criteria.Id);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("Fellowship not found based on criteria");
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
                var dto = await _fellowshipDal.InsertAsync(ToDto());
        
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
                var dto = await _fellowshipDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(FellowshipDto dto)
		{
            base.FetchData(dto);
            
			this.Id = dto.Id;
			this.UserId = dto.UserId;
			this.ProgramName = dto.ProgramName;
			this.CompletionYear = dto.CompletionYear;
			this.ProgramOther = dto.ProgramOther;
		}

		internal FellowshipDto ToDto()
		{
			var dto = new FellowshipDto();
			return ToDto(dto);
		}
		internal FellowshipDto ToDto(FellowshipDto dto)
		{
            base.ToDto(dto);
            
			dto.Id = this.Id;
			dto.UserId = this.UserId;
			dto.ProgramName = this.ProgramName;
			dto.CompletionYear = this.CompletionYear;
			dto.ProgramOther = this.ProgramOther;

			return dto;
		}


    }
}
