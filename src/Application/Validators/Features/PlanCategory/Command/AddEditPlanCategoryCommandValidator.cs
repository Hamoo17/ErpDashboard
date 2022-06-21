using ErpDashboard.Application.Features.PlanCategory.Command.AddEdit;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Validators.Features.PlanCategory.Command
{
    public class AddEditPlanCategoryCommandValidator : AbstractValidator<AddEditPlanCategoryCommand>
    {
        public AddEditPlanCategoryCommandValidator()
        {
            RuleFor(x=> x.TypeName).NotEmpty().WithMessage("Category Name is Required");
            RuleFor(x => x.Symbol).NotEmpty().WithMessage("Required");
            
        }
    }
}
