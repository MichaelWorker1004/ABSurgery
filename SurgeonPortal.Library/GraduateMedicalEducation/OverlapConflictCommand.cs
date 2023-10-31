using System;
using System.Threading.Tasks;
using Csla;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;


namespace SurgeonPortal.Library.GraduateMedicalEducation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
    public class OverlapConflictCommand : YtgCommandBase<OverlapConflictCommand, int>, IOverlapConflictCommand
    {
        private readonly IOverlapConflictCommandDal _overlapConflictCommandDal;


        public OverlapConflictCommand(
            IIdentityProvider identityProvider,
            IOverlapConflictCommandDal overlapConflictCommandDal)
            : base(identityProvider)
        {
            _overlapConflictCommandDal = overlapConflictCommandDal;
        }

        public static bool CanExecuteCommand()
        {
            // customize to check user role, if applicable
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        public int UserId
		{
			get { return ReadProperty(UserIdProperty); }
			internal set { LoadProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        public DateTime StartDate
		{
			get { return ReadProperty(StartDateProperty); }
			internal set { LoadProperty(StartDateProperty, value); }
		}
		public static readonly PropertyInfo<DateTime> StartDateProperty = RegisterProperty<DateTime>(c => c.StartDate);

        public DateTime EndDate
		{
			get { return ReadProperty(EndDateProperty); }
			internal set { LoadProperty(EndDateProperty, value); }
		}
		public static readonly PropertyInfo<DateTime> EndDateProperty = RegisterProperty<DateTime>(c => c.EndDate);

        public bool OverlapConflict
		{
			get { return ReadProperty(OverlapConflictProperty); }
			internal set { LoadProperty(OverlapConflictProperty, value); }
		}
		public static readonly PropertyInfo<bool> OverlapConflictProperty = RegisterProperty<bool>(c => c.OverlapConflict);

        public int? RotationId
		{
			get { return ReadProperty(RotationIdProperty); }
			internal set { LoadProperty(RotationIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> RotationIdProperty = RegisterProperty<int?>(c => c.RotationId);


        [Execute]
        protected void ExecuteCommand()
        {
            var dto = _overlapConflictCommandDal.CheckOverlapConflicts(
                UserId,
                StartDate,
                EndDate,
                RotationId);
            
            this.UserId = dto.UserId;
        	this.StartDate = dto.StartDate;
        	this.EndDate = dto.EndDate;
        	this.OverlapConflict = dto.OverlapConflict;
        	this.RotationId = dto.RotationId;
        }


        
    }
}
