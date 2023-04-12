using Csla;
using SurgeonPortal.Library.Contracts.Surgeons;
using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Surgeons
{
    public class CertificationReadOnlyFactory : ICertificationReadOnlyFactory
    {
        public async Task<ICertificationReadOnly> GetByAbsIdAsync(string absId)
        {
            return await DataPortal.FetchAsync<CertificationReadOnly>(
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
