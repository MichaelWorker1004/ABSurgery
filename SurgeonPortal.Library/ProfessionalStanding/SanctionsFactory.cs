using Csla;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    public class SanctionsFactory : ISanctionsFactory
    {
        public async Task<ISanctions> GetByUserIdAsync(int userId)
        {
            
            return await DataPortal.FetchAsync<Sanctions>(
                new GetByUserIdCriteria(userId));
            
        }

        public ISanctions Create()
        {
            return DataPortal.Create<Sanctions>();
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
