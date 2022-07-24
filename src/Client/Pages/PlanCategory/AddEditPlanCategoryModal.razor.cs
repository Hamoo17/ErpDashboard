using Blazored.FluentValidation;
using ErpDashboard.Application.Features.PlanCategory.Command.AddEdit;
using ErpDashboard.Client.Infrastructure.Managers.PlanCategory;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ErpDashboard.Client.Pages.PlanCategory
{
    public partial class AddEditPlanCategoryModal
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject] public IPlanCtegoryManager _planManager { get; set; }
        [Parameter] public AddEditPlanCategoryCommand Model { get; set; } = new();
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private async Task Save()
        {
            var Response = await _planManager.SaveAsync(Model);
            if (Response.Succeeded)
            {
                _snackBar.Add(Response.Messages[0], MudBlazor.Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var msg in Response.Messages)
                {
                    _snackBar.Add(msg, MudBlazor.Severity.Error);
                }
            }
        }
        public void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
