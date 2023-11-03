using Csla;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    public class MedicalLicenseReadOnlyListFactory : IMedicalLicenseReadOnlyListFactory
    {
        public async Task<IMedicalLicenseReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<MedicalLicenseReadOnlyList>();
            
        }

            [Serializable]
            internal class GetByUserIdCriteria
            {
            
                public GetByUserIdCriteria()
             {
              }
            }
            


    }
}
