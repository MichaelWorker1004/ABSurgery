using Csla;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
    public class RotationReadOnlyListFactory : IRotationReadOnlyListFactory
    {
        public async Task<IRotationReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<RotationReadOnlyList>();
            
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
