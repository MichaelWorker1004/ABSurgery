using Csla;
using SurgeonPortal.DataAccess.Contracts.Surgeons;
using SurgeonPortal.Library.Contracts.Surgeons;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Surgeons.CertificationReadOnlyFactory;

namespace SurgeonPortal.Library.Surgeons
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class CertificationReadOnly : YtgBusinessBase<CertificationReadOnly>, ICertificationReadOnly
    {
        private readonly ICertificationReadOnlyDal _certificationReadOnlyDal;

        public CertificationReadOnly(
            IIdentityProvider identityProvider,
            ICertificationReadOnlyDal certificationReadOnlyDal)
            : base(identityProvider)
        {
            _certificationReadOnlyDal = certificationReadOnlyDal;
        }

        [DisplayName(nameof(Speciality))]
		public string Speciality
		{
			get { return GetProperty(SpecialityProperty); }
			set { SetProperty(SpecialityProperty, value); }
		}
		public static readonly PropertyInfo<string> SpecialityProperty = RegisterProperty<string>(c => c.Speciality);

        [DisplayName(nameof(CertificateId))]
		public string CertificateId
		{
			get { return GetProperty(CertificateIdProperty); }
			set { SetProperty(CertificateIdProperty, value); }
		}
		public static readonly PropertyInfo<string> CertificateIdProperty = RegisterProperty<string>(c => c.CertificateId);

        [DisplayName(nameof(InitialCertificationDate))]
		public string InitialCertificationDate
		{
			get { return GetProperty(InitialCertificationDateProperty); }
			set { SetProperty(InitialCertificationDateProperty, value); }
		}
		public static readonly PropertyInfo<string> InitialCertificationDateProperty = RegisterProperty<string>(c => c.InitialCertificationDate);

        [DisplayName(nameof(EndDateDisplay))]
		public string EndDateDisplay
		{
			get { return GetProperty(EndDateDisplayProperty); }
			set { SetProperty(EndDateDisplayProperty, value); }
		}
		public static readonly PropertyInfo<string> EndDateDisplayProperty = RegisterProperty<string>(c => c.EndDateDisplay);



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
        private async Task GetByAbsId(GetByAbsIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _certificationReadOnlyDal.GetByAbsIdAsync(criteria.AbsId);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("CertificationReadOnly not found based on criteria");
                }
                FetchData(dto);
            }
        }



        private void FetchData(CertificationReadOnlyDto dto)
		{
            base.FetchData(dto);
            
			this.Speciality = dto.Speciality;
			this.CertificateId = dto.CertificateId;
			this.InitialCertificationDate = dto.InitialCertificationDate;
			this.EndDateDisplay = dto.EndDateDisplay;
		}

		internal CertificationReadOnlyDto ToDto()
		{
			var dto = new CertificationReadOnlyDto();
			return ToDto(dto);
		}
		internal CertificationReadOnlyDto ToDto(CertificationReadOnlyDto dto)
		{
            base.ToDto(dto);
            
			dto.Speciality = this.Speciality;
			dto.CertificateId = this.CertificateId;
			dto.InitialCertificationDate = this.InitialCertificationDate;
			dto.EndDateDisplay = this.EndDateDisplay;

			return dto;
		}


    }
}
