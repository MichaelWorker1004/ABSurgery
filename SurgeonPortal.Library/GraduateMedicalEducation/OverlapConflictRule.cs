﻿using Csla.Core;
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
		private IPropertyInfo SecondaryProperty { get; set; }

		public OverlapConflictRule(
			IPropertyInfo primaryProperty,
			IPropertyInfo secondaryProperty,
			int priority)
			: base(primaryProperty)
		{
			_overlapConflictCommandFactory = new OverlapConflictCommandFactory();
			InputProperties = new List<IPropertyInfo> { primaryProperty, secondaryProperty };
			SecondaryProperty = secondaryProperty;
			Priority = priority;
			IsAsync = false;
		}

		protected override void Execute(IRuleContext context)
		{
			var startDate = context.InputPropertyValues[PrimaryProperty];
			var endDate = context.InputPropertyValues[SecondaryProperty];

			var target = context.Target as Rotation;

			if (startDate is DateTime parsedStartDate && endDate is DateTime parsedEndDate)
			{
				var command = _overlapConflictCommandFactory.CheckOverlapConflicts(IdentityHelper.UserId, parsedStartDate, parsedEndDate, target.IsNew ? null : target.Id);
				if(command.OverlapConflict)
				{
					context.AddErrorResult(PrimaryProperty, "The rotation dates overlap with an already existing rotation.");
				}
			}
		}
	}
}
