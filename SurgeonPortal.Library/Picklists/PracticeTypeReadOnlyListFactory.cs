using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class PracticeTypeReadOnlyListFactory : IPracticeTypeReadOnlyListFactory
    {
        public async Task<IPracticeTypeReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<PracticeTypeReadOnlyList>();
            
        }

            


    }
}
