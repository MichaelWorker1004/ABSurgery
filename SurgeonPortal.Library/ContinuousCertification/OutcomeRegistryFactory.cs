using Csla;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.ContinuousCertification
{
    public class OutcomeRegistryFactory : IOutcomeRegistryFactory
    {
        public async Task<IOutcomeRegistry> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<OutcomeRegistry>();
            
        }

        public IOutcomeRegistry Create()
        {
            return DataPortal.Create<OutcomeRegistry>();
        }


        
            


    }
}
