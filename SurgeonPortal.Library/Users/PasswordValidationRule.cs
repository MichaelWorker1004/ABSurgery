using Csla.Core;
using Csla.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Users
{

    public class PasswordValidationRule : Csla.Rules.BusinessRule
    {
        public PasswordValidationRule(IPropertyInfo primaryProperty)
          : base(primaryProperty)
        {
            // If you are  going to add InputProperties make sure to uncomment line below as InputProperties is NULL by default
            //if (InputProperties == null) InputProperties = new List<IPropertyInfo>();

            // Add additional constructor code here 



            // Mark rule for IsAsync if Execute method implemets asyncronous calls
            // IsAsync = true; 
        }

        protected override void Execute(IRuleContext context)
        {
            // Asyncronous rules 
            // If rule is async make sure that ALL excution paths call context.Complete


            // Add actual rule code here. 
            //if (broken condition)
            //{
            //  context.AddErrorResult("Broken rule message");
            //}
        }
    }
}
