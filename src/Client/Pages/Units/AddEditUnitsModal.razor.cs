using Blazored.FluentValidation;
using ErpDashboard.Application.Features.Units.Commands.AddEdit;
using ErpDashboard.Client.Infrastructure.Managers.Units;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ErpDashboard.Client.Pages.Units
{
    public partial class AddEditUnitsModal
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject] public IUnitsManager _planManager { get; set; }
        [Parameter] public AddEditUnitsCommand Model { get; set; } = new();
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
