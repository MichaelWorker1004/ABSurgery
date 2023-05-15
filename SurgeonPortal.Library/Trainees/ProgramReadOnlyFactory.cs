using Csla;
using SurgeonPortal.Library.Contracts.Trainees;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Trainees
{
    public class ProgramReadOnlyFactory : IProgramReadOnlyFactory
    {
        public async Task<IProgramReadOnly> GetByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new FactoryInvalidCriteriaException("userId is a required field.");
            }
            
            return await DataPortal.FetchAsync<ProgramReadOnly>(
                new GetByUserIdCriteria(userId));
            
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
