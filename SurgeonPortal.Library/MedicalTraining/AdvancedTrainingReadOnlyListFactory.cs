using Csla;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.MedicalTraining
{
    public class AdvancedTrainingReadOnlyListFactory : IAdvancedTrainingReadOnlyListFactory
    {
        public async Task<IAdvancedTrainingReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<AdvancedTrainingReadOnlyList>();
            
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
