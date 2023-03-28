using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.Core;
using Csla.Rules;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using SurgeonPortal.Shared;

namespace SurgeonPortal.Library.Users
{

    public class PasswordMatchesCurrentRule : Csla.Rules.BusinessRule
    {
        private readonly IPasswordValidationCommandFactory _passwordValidationCommandFactory;

        public PasswordMatchesCurrentRule(IPasswordValidationCommandFactory passwordValidationCommandFactory,
            IPropertyInfo primaryProperty,
            int priority)
          : base(primaryProperty)
        {
            _passwordValidationCommandFactory = passwordValidationCommandFactory;
            InputProperties = new List<IPropertyInfo> { primaryProperty };
            Priority = priority;
            IsAsync = false;
        }

        protected override void Execute(IRuleContext context)
        {
            var propertyValue = context.InputPropertyValues[PrimaryProperty];
            var target = (IBusinessBase)context.Target;
            
            if (propertyValue != null)
            {
                var newPassword = propertyValue.ToString();
                if (!string.IsNullOrWhiteSpace(newPassword))
                {
                    var command = _passwordValidationCommandFactory.Validate(IdentityHelper.UserId, newPassword);
                    if (command.PasswordsMatch.HasValue)
                    {
                        if (command.PasswordsMatch.Value == true)
                        {
                            context.AddErrorResult(PrimaryProperty, $"The new password cannot be the same as the current password.");
                        }
                    }
                }
            }
        }
    }
}
