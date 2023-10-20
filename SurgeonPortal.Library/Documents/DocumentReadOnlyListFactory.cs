using Csla;
using SurgeonPortal.Library.Contracts.Documents;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Documents
{
    public class DocumentReadOnlyListFactory : IDocumentReadOnlyListFactory
    {
        public async Task<IDocumentReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<DocumentReadOnlyList>();
            
        }

            [Serializable]
            internal class GetByUserIdCriteria
            {
            
                public GetByUserIdCriteria()
             {
              }
            }
            


    }
}
