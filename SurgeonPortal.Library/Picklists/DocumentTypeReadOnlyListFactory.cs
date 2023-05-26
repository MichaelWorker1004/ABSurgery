using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class DocumentTypeReadOnlyListFactory : IDocumentTypeReadOnlyListFactory
    {
        public async Task<IDocumentTypeReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<DocumentTypeReadOnlyList>();
            
        }

            


    }
}
