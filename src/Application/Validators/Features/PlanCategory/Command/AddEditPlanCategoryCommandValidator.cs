using ErpDashboard.Application.Features.PlanCategory.Command.AddEdit;
using FluentValidation;

namespace ErpDashboard.Application.Validators.Features.PlanCategory.Command
{
    public class AddEditPlanCategoryCommandValidator : AbstractValidator<AddEditPlanCategoryCommand>
    {
        public AddEditPlanCategoryCommandValidator()
        {
            RuleFor(x => x.TypeName).NotEmpty().WithMessage("Category Name is Required");
            RuleFor(x => x.Symbol).NotEmpty().WithMessage("Required");

        }
    }
}
