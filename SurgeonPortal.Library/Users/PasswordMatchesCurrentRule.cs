using Csla.Core;
using Csla.Rules;
using SurgeonPortal.Library.Contracts.Users;
using System.Collections.Generic;

namespace SurgeonPortal.Library.Users
{

	public class PasswordMatchesCurrentRule : BusinessRule
    {
        private readonly IPasswordValidationCommandFactory _passwordValidationCommandFactory;
        private IPropertyInfo _userIdProperty;
        public PasswordMatchesCurrentRule(IPasswordValidationCommandFactory passwordValidationCommandFactory,
            IPropertyInfo primaryProperty,
            IPropertyInfo userIdProperty,
            int priority)
          : base(primaryProperty)
        {
            _passwordValidationCommandFactory = passwordValidationCommandFactory;
            _userIdProperty = userIdProperty;
            InputProperties = new List<IPropertyInfo> { primaryProperty, userIdProperty };
            Priority = priority;
            IsAsync = false;
        }

        protected override void Execute(IRuleContext context)
        {
            var propertyValue = context.InputPropertyValues[PrimaryProperty];
            var userIdValue = context.InputPropertyValues[_userIdProperty];
            
            if (propertyValue != null &&
                userIdValue != null)
            {
                if (int.TryParse(userIdValue.ToString(), out int userId))
                {
                    var newPassword = propertyValue.ToString();
                    if (!string.IsNullOrWhiteSpace(newPassword))
                    {
                        var command = _passwordValidationCommandFactory.Validate(userId, newPassword);
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
}
