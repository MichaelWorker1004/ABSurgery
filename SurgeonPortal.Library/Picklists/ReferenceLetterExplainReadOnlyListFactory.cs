using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ReferenceLetterExplainReadOnlyListFactory : IReferenceLetterExplainReadOnlyListFactory
    {
        public async Task<IReferenceLetterExplainReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<ReferenceLetterExplainReadOnlyList>();
            
        }

            


    }
}
