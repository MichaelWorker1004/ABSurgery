using Csla;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.MedicalTraining.FellowshipReadOnlyListFactory;

namespace SurgeonPortal.Library.MedicalTraining
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class FellowshipReadOnlyList : YtgReadOnlyListBase<IFellowshipReadOnlyList, IFellowshipReadOnly>, IFellowshipReadOnlyList
    {
        private readonly IFellowshipReadOnlyDal _fellowshipReadOnlyDal;

        public FellowshipReadOnlyList(IFellowshipReadOnlyDal fellowshipReadOnlyDal)
        {
            _fellowshipReadOnlyDal = fellowshipReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            

        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            var dtos = await _fellowshipReadOnlyDal.GetByUserIdAsync(_identity.GetUserId<int>());
        			
            FetchChildren(dtos);
        }
    }
}
