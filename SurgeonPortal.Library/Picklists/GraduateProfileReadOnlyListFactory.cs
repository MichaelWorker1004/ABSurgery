using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class GraduateProfileReadOnlyListFactory : IGraduateProfileReadOnlyListFactory
    {
        public async Task<IGraduateProfileReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<GraduateProfileReadOnlyList>();
            
        }

            


    }
}
