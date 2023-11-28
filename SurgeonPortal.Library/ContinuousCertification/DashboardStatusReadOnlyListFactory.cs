using Csla;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ContinuousCertification
{
    public class DashboardStatusReadOnlyListFactory : IDashboardStatusReadOnlyListFactory
    {
        public async Task<IDashboardStatusReadOnlyList> GetAllByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<DashboardStatusReadOnlyList>();
            
        }

            [Serializable]
            internal class GetAllByUserIdCriteria
            {
            
                public GetAllByUserIdCriteria()
             {
              }
            }
            


    }
}
