using Csla;
using SurgeonPortal.Library.Contracts.Documents;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Documents
{
    public class DocumentFactory : IDocumentFactory
    {
        public async Task<IDocument> GetByIdAsync(
            int id,
            int userId)
        {
            if (id <= 0)
            {
                throw new FactoryInvalidCriteriaException("id is a required field.");
            }
            
            return await DataPortal.FetchAsync<Document>(
                new GetByIdCriteria(
                id,
                userId));
            
        }

        public IDocument Create()
        {
            return DataPortal.Create<Document>();
        }


        
            [Serializable]
            internal class GetByIdCriteria
            {
                public int Id { get; set; }
                public int UserId { get; set; }
            
                public GetByIdCriteria(
                int id,
                int userId)
             {
                    Id = id;
                    UserId = userId;
              }
            }
            


    }
}
