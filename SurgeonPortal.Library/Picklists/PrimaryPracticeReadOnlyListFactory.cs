using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class PrimaryPracticeReadOnlyListFactory : IPrimaryPracticeReadOnlyListFactory
    {
        public async Task<IPrimaryPracticeReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<PrimaryPracticeReadOnlyList>();
            
        }

            


    }
}
