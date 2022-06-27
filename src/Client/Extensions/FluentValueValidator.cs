using FluentValidation;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ErpDashboard.Client.Extensions
{
    public class FluentValueValidator<T> : AbstractValidator<T>
    {
        public FluentValueValidator(Action<IRuleBuilderInitial<T, T>> rule)
        {
            rule(RuleFor(x => x));
           
        }
        
        private IEnumerable<string> ValidateValue(T arg)
        {
            
            if (arg== null)
            {
                return new string[0];
            }
            var result = Validate(arg);
            if (result.IsValid)
                return new string[0];
            return result.Errors.Select(e => e.ErrorMessage);
        }

        public Func<T, IEnumerable<string>> Validation => ValidateValue;
    }
}
