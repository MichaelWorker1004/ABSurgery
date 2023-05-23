using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class DegreeReadOnlyListFactory : IDegreeReadOnlyListFactory
    {
        public async Task<IDegreeReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<DegreeReadOnlyList>();
            
        }

            


    }
}
