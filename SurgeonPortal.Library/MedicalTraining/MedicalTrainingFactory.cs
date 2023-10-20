using Csla;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.MedicalTraining
{
    public class MedicalTrainingFactory : IMedicalTrainingFactory
    {
        public async Task<IMedicalTraining> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<MedicalTraining>();
            
        }

        public IMedicalTraining Create()
        {
            return DataPortal.Create<MedicalTraining>();
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
