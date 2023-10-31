using Csla;
using Csla.Core;
using Csla.Rules;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Shared;
using System;
using System.Collections.Generic;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
	public class OverlapConflictRule : BusinessRule
	{
		private IOverlapConflictCommandFactory _overlapConflictCommandFactory;
        private IPropertyInfo _userIdProperty;
        private IPropertyInfo SecondaryProperty { get; set; }

		public OverlapConflictRule(IOverlapConflictCommandFactory overlapConflictCommandFactory,
			IPropertyInfo primaryProperty,
			IPropertyInfo secondaryProperty,
            IPropertyInfo userIdProperty,
            int priority)
			: base(primaryProperty)
		{
			_overlapConflictCommandFactory = overlapConflictCommandFactory;
            _userIdProperty = userIdProperty;
            InputProperties = new List<IPropertyInfo> { primaryProperty, secondaryProperty, userIdProperty };
			SecondaryProperty = secondaryProperty;
			Priority = priority;
			IsAsync = false;
		}

		protected override void Execute(IRuleContext context)
		{
			var startDate = context.InputPropertyValues[PrimaryProperty];
			var endDate = context.InputPropertyValues[SecondaryProperty];
            var userIdValue = context.InputPropertyValues[_userIdProperty];

            var target = context.Target as Rotation;

			if (startDate is DateTime parsedStartDate && 
				endDate is DateTime parsedEndDate &&
                userIdValue != null)
			{
				if (int.TryParse(userIdValue.ToString(), out int userId))
				{
					var command = _overlapConflictCommandFactory.CheckOverlapConflicts(userId, parsedStartDate, parsedEndDate, target.IsNew ? null : target.Id);
					if (command.OverlapConflict)
					{
						context.AddErrorResult(PrimaryProperty, "The rotation dates overlap with an already existing rotation.");
					}
				}
			}
		}
	}
}
