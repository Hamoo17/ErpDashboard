using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Validators.Features.Customers
{
    public class AddEditCustomerCommandValidator : AbstractValidator<AddEditCustomerCommand>
    {
        public AddEditCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().NotNull().WithMessage("Required Data");
            RuleFor(x => x.Weight).NotEmpty().WithMessage("Required Data");
            RuleFor(x => x.Height).NotEmpty().WithMessage("Required Data");
            RuleFor(x => x.RegType).NotEmpty().NotNull().WithMessage("Required Data");
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage("Required Data");
            RuleFor(x => x.BirthDate).NotNull().NotEmpty().GreaterThan(new DateTime(1930,1,1)).WithMessage("Required Data");
        }
    }
}
