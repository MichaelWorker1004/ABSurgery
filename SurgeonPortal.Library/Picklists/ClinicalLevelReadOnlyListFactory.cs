using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ClinicalLevelReadOnlyListFactory : IClinicalLevelReadOnlyListFactory
    {
        public async Task<IClinicalLevelReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<ClinicalLevelReadOnlyList>();
            
        }

            


    }
}
