using Csla;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.MedicalTraining
{
    public class MedicalTrainingFactory : IMedicalTrainingFactory
    {
        public async Task<IMedicalTraining> GetByUserIdAsync(int userId)
        {
            
            return await DataPortal.FetchAsync<MedicalTraining>(
                new GetByUserIdCriteria(userId));
            
        }

        public IMedicalTraining Create()
        {
            return DataPortal.Create<MedicalTraining>();
        }


        
            [Serializable]
            internal class GetByUserIdCriteria
            {
                public int UserId { get; set; }
            
                public GetByUserIdCriteria(int userId)
             {
                    UserId = userId;
              }
            }
            


    }
}
