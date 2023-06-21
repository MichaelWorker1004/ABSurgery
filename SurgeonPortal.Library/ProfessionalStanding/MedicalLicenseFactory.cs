using Csla;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    public class MedicalLicenseFactory : IMedicalLicenseFactory
    {
        public async Task<IMedicalLicense> GetByIdAsync(int licenseId)
        {
            if (licenseId <= 0)
            {
                throw new FactoryInvalidCriteriaException("licenseId is a required field.");
            }
            
            return await DataPortal.FetchAsync<MedicalLicense>(
                new GetByIdCriteria(licenseId));
            
        }

        public IMedicalLicense Create()
        {
            return DataPortal.Create<MedicalLicense>();
        }


        
            [Serializable]
            internal class GetByIdCriteria
            {
                public int LicenseId { get; set; }
            
                public GetByIdCriteria(int licenseId)
             {
                    LicenseId = licenseId;
              }
            }
            


    }
}
