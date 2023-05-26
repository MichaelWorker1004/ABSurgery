using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class CertificateTypeReadOnlyListFactory : ICertificateTypeReadOnlyListFactory
    {
        public async Task<ICertificateTypeReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<CertificateTypeReadOnlyList>();
            
        }

            


    }
}
