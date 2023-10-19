using Csla;
using SurgeonPortal.Library.Contracts.Surgeons;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Surgeons
{
    public class CertificationReadOnlyListFactory : ICertificationReadOnlyListFactory
    {
        public async Task<ICertificationReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<CertificationReadOnlyList>();
            
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
