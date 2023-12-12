using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Picklists;
using SurgeonPortal.Models.Picklists;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Picklists
{
    [ApiVersion("1")]
    [ApiController]
    [Produces("application/json")]
	[Route("api/picklists")]
	public class PicklistsController : YtgControllerBase
	{
        private readonly IMapper _mapper;

        public PicklistsController(
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper)
            : base(webHostEnvironment)
        {
            _mapper = mapper;
        }

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AccommodationReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("accommodations")]
        public async Task<ActionResult<IEnumerable<AccommodationReadOnlyModel>>> GetAccommodationReadOnly_GetAllAsync(
            [FromServices] IAccommodationReadOnlyListFactory accommodationReadOnlyListFactory)
        {
            var items = await accommodationReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<AccommodationReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AccreditedProgramInstitutionReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("accredited-program-institutions")]
        public async Task<ActionResult<IEnumerable<AccreditedProgramInstitutionReadOnlyModel>>> GetAccreditedProgramInstitutionReadOnly_GetAllAsync(
            [FromServices] IAccreditedProgramInstitutionReadOnlyListFactory accreditedProgramInstitutionReadOnlyListFactory)
        {
            var items = await accreditedProgramInstitutionReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<AccreditedProgramInstitutionReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AppointmentTypeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("appointment-types")]
        public async Task<ActionResult<IEnumerable<AppointmentTypeReadOnlyModel>>> GetAppointmentTypeReadOnly_GetAllAsync(
            [FromServices] IAppointmentTypeReadOnlyListFactory appointmentTypeReadOnlyListFactory)
        {
            var items = await appointmentTypeReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<AppointmentTypeReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CertificateTypeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("certificate-types")]
        public async Task<ActionResult<IEnumerable<CertificateTypeReadOnlyModel>>> GetCertificateTypeReadOnly_GetAllAsync(
            [FromServices] ICertificateTypeReadOnlyListFactory certificateTypeReadOnlyListFactory)
        {
            var items = await certificateTypeReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<CertificateTypeReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClinicalActivityReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("clinical-activities")]
        public async Task<ActionResult<IEnumerable<ClinicalActivityReadOnlyModel>>> GetClinicalActivityReadOnly_GetAllAsync(
            [FromServices] IClinicalActivityReadOnlyListFactory clinicalActivityReadOnlyListFactory)
        {
            var items = await clinicalActivityReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<ClinicalActivityReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClinicalLevelReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("clinical-levels")]
        public async Task<ActionResult<IEnumerable<ClinicalLevelReadOnlyModel>>> GetClinicalLevelReadOnly_GetAllAsync(
            [FromServices] IClinicalLevelReadOnlyListFactory clinicalLevelReadOnlyListFactory)
        {
            var items = await clinicalLevelReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<ClinicalLevelReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CountryReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("countries")]
        public async Task<ActionResult<IEnumerable<CountryReadOnlyModel>>> GetCountryReadOnly_GetAllAsync(
            [FromServices] ICountryReadOnlyListFactory countryReadOnlyListFactory)
        {
            var items = await countryReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<CountryReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DegreeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("degrees")]
        public async Task<ActionResult<IEnumerable<DegreeReadOnlyModel>>> GetDegreeReadOnly_GetAllAsync(
            [FromServices] IDegreeReadOnlyListFactory degreeReadOnlyListFactory)
        {
            var items = await degreeReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<DegreeReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DocumentTypeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("document-types")]
        public async Task<ActionResult<IEnumerable<DocumentTypeReadOnlyModel>>> GetDocumentTypeReadOnly_GetAllAsync(
            [FromServices] IDocumentTypeReadOnlyListFactory documentTypeReadOnlyListFactory)
        {
            var items = await documentTypeReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<DocumentTypeReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EthnicityReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("ethnicities")]
        public async Task<ActionResult<IEnumerable<EthnicityReadOnlyModel>>> GetEthnicityReadOnly_GetAllAsync(
            [FromServices] IEthnicityReadOnlyListFactory ethnicityReadOnlyListFactory)
        {
            var items = await ethnicityReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<EthnicityReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamSpecialtyReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("exam-specialties")]
        public async Task<ActionResult<IEnumerable<ExamSpecialtyReadOnlyModel>>> GetExamSpecialtyReadOnly_GetAllAsync(
            [FromServices] IExamSpecialtyReadOnlyListFactory examSpecialtyReadOnlyListFactory)
        {
            var items = await examSpecialtyReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<ExamSpecialtyReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamStatusReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("exam-statuses")]
        public async Task<ActionResult<IEnumerable<ExamStatusReadOnlyModel>>> GetExamStatusReadOnly_GetAllAsync(
            [FromServices] IExamStatusReadOnlyListFactory examStatusReadOnlyListFactory)
        {
            var items = await examStatusReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<ExamStatusReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamTypeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("exam-types")]
        public async Task<ActionResult<IEnumerable<ExamTypeReadOnlyModel>>> GetExamTypeReadOnly_GetAllAsync(
            [FromServices] IExamTypeReadOnlyListFactory examTypeReadOnlyListFactory)
        {
            var items = await examTypeReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<ExamTypeReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FellowshipProgramReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("fellowship-programs")]
        public async Task<ActionResult<IEnumerable<FellowshipProgramReadOnlyModel>>> GetFellowshipProgramReadOnly_GetAllAsync(
            [FromServices] IFellowshipProgramReadOnlyListFactory fellowshipProgramReadOnlyListFactory,
            string fellowshipType)
        {
            var items = await fellowshipProgramReadOnlyListFactory.GetAllAsync(fellowshipType);
        
            return Ok(_mapper.Map<IEnumerable<FellowshipProgramReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FellowshipTypeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("fellowship-types")]
        public async Task<ActionResult<IEnumerable<FellowshipTypeReadOnlyModel>>> GetFellowshipTypeReadOnly_GetAsync(
            [FromServices] IFellowshipTypeReadOnlyListFactory fellowshipTypeReadOnlyListFactory)
        {
            var items = await fellowshipTypeReadOnlyListFactory.GetAsync();
        
            return Ok(_mapper.Map<IEnumerable<FellowshipTypeReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GenderReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("genders")]
        public async Task<ActionResult<IEnumerable<GenderReadOnlyModel>>> GetGenderReadOnly_GetAllAsync(
            [FromServices] IGenderReadOnlyListFactory genderReadOnlyListFactory)
        {
            var items = await genderReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<GenderReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GraduateProfileReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("graduate-profile")]
        public async Task<ActionResult<IEnumerable<GraduateProfileReadOnlyModel>>> GetGraduateProfileReadOnly_GetAllAsync(
            [FromServices] IGraduateProfileReadOnlyListFactory graduateProfileReadOnlyListFactory)
        {
            var items = await graduateProfileReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<GraduateProfileReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<JcahoOrganizationReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("jcaho-organizations")]
        public async Task<ActionResult<IEnumerable<JcahoOrganizationReadOnlyModel>>> GetJcahoOrganizationReadOnly_GetAllAsync(
            [FromServices] IJcahoOrganizationReadOnlyListFactory jcahoOrganizationReadOnlyListFactory)
        {
            var items = await jcahoOrganizationReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<JcahoOrganizationReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LanguageReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("languages")]
        public async Task<ActionResult<IEnumerable<LanguageReadOnlyModel>>> GetLanguageReadOnly_GetAllAsync(
            [FromServices] ILanguageReadOnlyListFactory languageReadOnlyListFactory)
        {
            var items = await languageReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<LanguageReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LicenseTypeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("license-types")]
        public async Task<ActionResult<IEnumerable<LicenseTypeReadOnlyModel>>> GetLicenseTypeReadOnly_GetAllAsync(
            [FromServices] ILicenseTypeReadOnlyListFactory licenseTypeReadOnlyListFactory)
        {
            var items = await licenseTypeReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<LicenseTypeReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrganizationTypeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("organization-types")]
        public async Task<ActionResult<IEnumerable<OrganizationTypeReadOnlyModel>>> GetOrganizationTypeReadOnly_GetAllAsync(
            [FromServices] IOrganizationTypeReadOnlyListFactory organizationTypeReadOnlyListFactory)
        {
            var items = await organizationTypeReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<OrganizationTypeReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PracticeTypeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("practice-types")]
        public async Task<ActionResult<IEnumerable<PracticeTypeReadOnlyModel>>> GetPracticeTypeReadOnly_GetAllAsync(
            [FromServices] IPracticeTypeReadOnlyListFactory practiceTypeReadOnlyListFactory)
        {
            var items = await practiceTypeReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<PracticeTypeReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PrimaryPracticeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("primary-practices")]
        public async Task<ActionResult<IEnumerable<PrimaryPracticeReadOnlyModel>>> GetPrimaryPracticeReadOnly_GetAllAsync(
            [FromServices] IPrimaryPracticeReadOnlyListFactory primaryPracticeReadOnlyListFactory)
        {
            var items = await primaryPracticeReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<PrimaryPracticeReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RaceReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("races")]
        public async Task<ActionResult<IEnumerable<RaceReadOnlyModel>>> GetRaceReadOnly_GetAllAsync(
            [FromServices] IRaceReadOnlyListFactory raceReadOnlyListFactory)
        {
            var items = await raceReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<RaceReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResidencyProgramReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("residency-programs")]
        public async Task<ActionResult<IEnumerable<ResidencyProgramReadOnlyModel>>> GetResidencyProgramReadOnly_GetAllAsync(
            [FromServices] IResidencyProgramReadOnlyListFactory residencyProgramReadOnlyListFactory)
        {
            var items = await residencyProgramReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<ResidencyProgramReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ScoringSessionReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("examiner-sessions")]
        public async Task<ActionResult<IEnumerable<ScoringSessionReadOnlyModel>>> GetScoringSessionReadOnly_GetByKeysAsync(
            [FromServices] IScoringSessionReadOnlyListFactory scoringSessionReadOnlyListFactory,
            DateTime currentDate)
        {
            var items = await scoringSessionReadOnlyListFactory.GetByKeysAsync(currentDate);
        
            return Ok(_mapper.Map<IEnumerable<ScoringSessionReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StateReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("states")]
        public async Task<ActionResult<IEnumerable<StateReadOnlyModel>>> GetStateReadOnly_GetByCountryAsync(
            [FromServices] IStateReadOnlyListFactory stateReadOnlyListFactory,
            string countryCode)
        {
            var items = await stateReadOnlyListFactory.GetByCountryAsync(countryCode);
        
            return Ok(_mapper.Map<IEnumerable<StateReadOnlyModel>>(items));
        } 

        ///<summary>
        /// YtgIm
        ///<summary>
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TrainingTypeReadOnlyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("training-types")]
        public async Task<ActionResult<IEnumerable<TrainingTypeReadOnlyModel>>> GetTrainingTypeReadOnly_GetAllAsync(
            [FromServices] ITrainingTypeReadOnlyListFactory trainingTypeReadOnlyListFactory)
        {
            var items = await trainingTypeReadOnlyListFactory.GetAllAsync();
        
            return Ok(_mapper.Map<IEnumerable<TrainingTypeReadOnlyModel>>(items));
        } 
    }
}

