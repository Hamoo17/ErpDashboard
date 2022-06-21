using ErpDashboard.Application.Features.PlanDays.Command.AddEdit;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Validators.Features.PlanDay.Command.AddEdit
{
    public class AddEditPlanDayCommandValidator:AbstractValidator<AddEditPlanDaysCommand>
    {
        public AddEditPlanDayCommandValidator()
        {
            RuleFor(z=>z.Name).NotEmpty().WithMessage("PlanDay Is Required")
                .Must(x=> !IsExist(x)).WithMessage("This Name Is Already Exist");
            RuleFor(x => x.DayCount).GreaterThan(0).WithMessage("Day Count Must Be Greater Than 0")
                .NotEmpty().WithMessage("DayCount Is Required");

        }
        public bool IsExist(string Name) 
        {

            return false;
        }
    }
}
