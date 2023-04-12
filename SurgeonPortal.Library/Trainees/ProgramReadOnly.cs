using Csla;
using SurgeonPortal.DataAccess.Contracts.Trainees;
using SurgeonPortal.Library.Contracts.Trainees;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Trainees.ProgramReadOnlyFactory;

namespace SurgeonPortal.Library.Trainees
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class ProgramReadOnly : YtgBusinessBase<ProgramReadOnly>, IProgramReadOnly
    {
        private readonly IProgramReadOnlyDal _programReadOnlyDal;

        public ProgramReadOnly(
            IIdentityProvider identityProvider,
            IProgramReadOnlyDal programReadOnlyDal)
            : base(identityProvider)
        {
            _programReadOnlyDal = programReadOnlyDal;
        }

        [DisplayName(nameof(ProgramName))]
		public string ProgramName
		{
			get { return GetProperty(ProgramNameProperty); }
			set { SetProperty(ProgramNameProperty, value); }
		}
		public static readonly PropertyInfo<string> ProgramNameProperty = RegisterProperty<string>(c => c.ProgramName);

        [DisplayName(nameof(ProgramDirector))]
		public string ProgramDirector
		{
			get { return GetProperty(ProgramDirectorProperty); }
			set { SetProperty(ProgramDirectorProperty, value); }
		}
		public static readonly PropertyInfo<string> ProgramDirectorProperty = RegisterProperty<string>(c => c.ProgramDirector);

        [DisplayName(nameof(ProgramNumber))]
		public string ProgramNumber
		{
			get { return GetProperty(ProgramNumberProperty); }
			set { SetProperty(ProgramNumberProperty, value); }
		}
		public static readonly PropertyInfo<string> ProgramNumberProperty = RegisterProperty<string>(c => c.ProgramNumber);

        [DisplayName(nameof(Exam))]
		public string Exam
		{
			get { return GetProperty(ExamProperty); }
			set { SetProperty(ExamProperty, value); }
		}
		public static readonly PropertyInfo<string> ExamProperty = RegisterProperty<string>(c => c.Exam);

        [DisplayName(nameof(ClinicalLevel))]
		public string ClinicalLevel
		{
			get { return GetProperty(ClinicalLevelProperty); }
			set { SetProperty(ClinicalLevelProperty, value); }
		}
		public static readonly PropertyInfo<string> ClinicalLevelProperty = RegisterProperty<string>(c => c.ClinicalLevel);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            

        }




        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId(GetByUserIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _programReadOnlyDal.GetByUserIdAsync(criteria.UserId);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("ProgramReadOnly not found based on criteria");
                }
                FetchData(dto);
            }
        }



        private void FetchData(ProgramReadOnlyDto dto)
		{
            base.FetchData(dto);
            
			this.ProgramName = dto.ProgramName;
			this.ProgramDirector = dto.ProgramDirector;
			this.ProgramNumber = dto.ProgramNumber;
			this.Exam = dto.Exam;
			this.ClinicalLevel = dto.ClinicalLevel;
		}

		internal ProgramReadOnlyDto ToDto()
		{
			var dto = new ProgramReadOnlyDto();
			return ToDto(dto);
		}
		internal ProgramReadOnlyDto ToDto(ProgramReadOnlyDto dto)
		{
            base.ToDto(dto);
            
			dto.ProgramName = this.ProgramName;
			dto.ProgramDirector = this.ProgramDirector;
			dto.ProgramNumber = this.ProgramNumber;
			dto.Exam = this.Exam;
			dto.ClinicalLevel = this.ClinicalLevel;

			return dto;
		}


    }
}
