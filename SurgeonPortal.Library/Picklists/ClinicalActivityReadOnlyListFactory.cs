using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ClinicalActivityReadOnlyListFactory : IClinicalActivityReadOnlyListFactory
    {
        public async Task<IClinicalActivityReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<ClinicalActivityReadOnlyList>();
            
        }

            


    }
}
