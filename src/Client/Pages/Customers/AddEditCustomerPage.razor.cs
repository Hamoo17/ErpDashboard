using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Client.Infrastructure.Managers.Customers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ErpDashboard.Client.Pages.Customers
{
    public partial class AddEditCustomerPage
    {
        [Parameter] public AddEditCustomerCommand Model { get; set; } = new();
        [Inject] public ICustomersManager _CustomerManager { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        private async Task Save()
        {
            Model.Notes = Model.Notes ?? string.Empty;
            var respons = await _CustomerManager.SaveAsync(Model);
            if (respons.Succeeded)
            {
                _snackBar.Add(respons.Messages[0], MudBlazor.Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var msg in respons.Messages)
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
