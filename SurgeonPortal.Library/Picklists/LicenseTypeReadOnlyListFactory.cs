using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class LicenseTypeReadOnlyListFactory : ILicenseTypeReadOnlyListFactory
    {
        public async Task<ILicenseTypeReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<LicenseTypeReadOnlyList>();
            
        }

            


    }
}
