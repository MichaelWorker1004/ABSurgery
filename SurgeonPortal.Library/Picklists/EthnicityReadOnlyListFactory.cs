using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class EthnicityReadOnlyListFactory : IEthnicityReadOnlyListFactory
    {
        public async Task<IEthnicityReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<EthnicityReadOnlyList>();
            
        }

            


    }
}
