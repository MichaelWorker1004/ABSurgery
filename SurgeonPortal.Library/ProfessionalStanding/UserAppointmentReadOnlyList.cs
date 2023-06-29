using Csla;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.ProfessionalStanding.UserAppointmentReadOnlyListFactory;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class UserAppointmentReadOnlyList : YtgReadOnlyListBase<IUserAppointmentReadOnlyList, IUserAppointmentReadOnly>, IUserAppointmentReadOnlyList
    {
        private readonly IUserAppointmentReadOnlyDal _userAppointmentReadOnlyDal;

        public UserAppointmentReadOnlyList(IUserAppointmentReadOnlyDal userAppointmentReadOnlyDal)
        {
            _userAppointmentReadOnlyDal = userAppointmentReadOnlyDal;
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
            var dtos = await _userAppointmentReadOnlyDal.GetByUserIdAsync();
        			
            FetchChildren(dtos);
        }
    }
}