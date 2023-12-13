using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ReferenceLetterTypeReadOnlyListFactory : IReferenceLetterTypeReadOnlyListFactory
    {
        public async Task<IReferenceLetterTypeReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<ReferenceLetterTypeReadOnlyList>();
            
        }

            


    }
}
