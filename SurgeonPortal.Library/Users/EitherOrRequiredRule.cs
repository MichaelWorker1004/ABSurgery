using Csla;
using Csla.Core;
using Csla.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Users
{
    public class EitherOrRequiredRule : Csla.Rules.BusinessRule
    {
        private IPropertyInfo SecondaryProperty { get; set; }
        public EitherOrRequiredRule(IPropertyInfo primaryProperty,
                IPropertyInfo secondaryProperty,
                int priority)
          : base(primaryProperty)
        {
            SecondaryProperty = secondaryProperty;
            InputProperties = new List<IPropertyInfo> { primaryProperty, secondaryProperty };
            Priority = priority;
        }

        protected override void Execute(IRuleContext context)
        {
            var propertyValue = context.InputPropertyValues[PrimaryProperty];
            var secondaryValue = context.InputPropertyValues[SecondaryProperty];

            var target = (IBusinessBase)context.Target;

            if ((propertyValue == null &&
                secondaryValue == null) ||
                (string.IsNullOrWhiteSpace(propertyValue?.ToString()) &&
                 string.IsNullOrWhiteSpace(secondaryValue?.ToString())))
            {
                context.AddErrorResult($"At least one value must be provided ({PrimaryProperty.FriendlyName} or {SecondaryProperty.FriendlyName})");
            }
        }
    }
}
