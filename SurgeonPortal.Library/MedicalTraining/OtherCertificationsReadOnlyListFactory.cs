using Csla;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.MedicalTraining
{
    public class OtherCertificationsReadOnlyListFactory : IOtherCertificationsReadOnlyListFactory
    {
        public async Task<IOtherCertificationsReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<OtherCertificationsReadOnlyList>();
            
        }

            [Serializable]
            internal class GetByUserIdCriteria
            {
                public int UserId { get; set; }
            
                public GetByUserIdCriteria(int userId)
             {
                    UserId = userId;
              }
            }
            


    }
}
