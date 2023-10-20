using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;


namespace SurgeonPortal.Library.ProfessionalStanding
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class GetClinicallyActiveCommand : CommandBase<GetClinicallyActiveCommand>, IGetClinicallyActiveCommand
    {
        private readonly IGetClinicallyActiveCommandDal _getClinicallyActiveCommandDal;


        public GetClinicallyActiveCommand(IGetClinicallyActiveCommandDal getClinicallyActiveCommandDal)
        {
            _getClinicallyActiveCommandDal = getClinicallyActiveCommandDal;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        public int? UserId
		{
			get { return ReadProperty(UserIdProperty); }
			internal set { LoadProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> UserIdProperty = RegisterProperty<int?>(c => c.UserId);

        public bool? ClinicallyActive
		{
			get { return ReadProperty(ClinicallyActiveProperty); }
			internal set { LoadProperty(ClinicallyActiveProperty, value); }
		}
		public static readonly PropertyInfo<bool?> ClinicallyActiveProperty = RegisterProperty<bool?>(c => c.ClinicallyActive);


        [Execute]
        protected void ExecuteCommand()
        {
                var dto = _getClinicallyActiveCommandDal.GetClinicallyActiveByUserId(_identity.GetUserId<int>());
            
            			this.UserId = dto.UserId;
        			this.ClinicallyActive = dto.ClinicallyActive;
        }


        
    }
}
