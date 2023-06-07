using Csla;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.MedicalTraining
{
    public class OtherCertificationsFactory : IOtherCertificationsFactory
    {
        public async Task<IOtherCertifications> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new FactoryInvalidCriteriaException("id is a required field.");
            }
            
            return await DataPortal.FetchAsync<OtherCertifications>(
                new GetByIdCriteria(id));
            
        }

        public IOtherCertifications Create()
        {
            return DataPortal.Create<OtherCertifications>();
        }


        
            [Serializable]
            internal class GetByIdCriteria
            {
                public int Id { get; set; }
            
                public GetByIdCriteria(int id)
             {
                    Id = id;
              }
            }
            


    }
}
