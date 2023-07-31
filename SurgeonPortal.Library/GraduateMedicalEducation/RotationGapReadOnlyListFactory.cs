using Csla;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
    public class RotationGapReadOnlyListFactory : IRotationGapReadOnlyListFactory
    {
        public async Task<IRotationGapReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<RotationGapReadOnlyList>();
            
        }

            


    }
}
