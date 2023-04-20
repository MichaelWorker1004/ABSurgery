using Csla;
using SurgeonPortal.Library.Contracts.Surgeons;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Surgeons
{
    public class CertificationReadOnlyListFactory : ICertificationReadOnlyListFactory
    {
        public async Task<ICertificationReadOnlyList> GetByAbsIdAsync(string absId)
        {
            if (string.IsNullOrEmpty(absId) == true)
            {
                throw new FactoryInvalidCriteriaException("absId is a required field.");
            }
            
            return await DataPortal.FetchAsync<CertificationReadOnlyList>(
                new GetByAbsIdCriteria(absId));
            
        }

            [Serializable]
            internal class GetByAbsIdCriteria
            {
                public string AbsId { get; set; }
            
                public GetByAbsIdCriteria(string absId)
             {
                    AbsId = absId;
              }
            }
            


    }
}
