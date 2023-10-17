using Csla;
using SurgeonPortal.Library.Contracts.ContinuingMedicalEducation;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ContinuingMedicalEducation
{
    public class CmeCreditReadOnlyListFactory : ICmeCreditReadOnlyListFactory
    {
        public async Task<ICmeCreditReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<CmeCreditReadOnlyList>();
            
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
