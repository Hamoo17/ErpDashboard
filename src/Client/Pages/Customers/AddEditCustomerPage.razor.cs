using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.Quers.GetAllCustomers;
using ErpDashboard.Application.Models;
using ErpDashboard.Client.Infrastructure.Managers.Customers;
using ErpDashboard.Client.Pages.Customers.Compnents;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ErpDashboard.Client.Pages.Customers
{
    public partial class AddEditCustomerPage
    {
        [Parameter] public AddEditCustomerCommand Model { get; set; } = new();
        [Inject] public ICustomersManager _CustomerManager { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        private PhonsDto _Phone;
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

        private async Task InvokeModal(int id =0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                var phone = Model.customerPhons.FirstOrDefault(x => x.Id == id);
                if (phone != null)
                {
                    parameters.Add(nameof(AddEditCustomerPhons.Model), new PhonsDto
                    {
                        Id = phone.Id,
                        Phone = phone.Phone,
                        PhoneType = phone.PhoneType,
                    });
                }
            }

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCustomerPhons>(id == 0 ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                //  Reset();
                var phone = result.Data as PhonsDto;
                if (phone != null)
                {
                    if (phone.Id != 0)
                    {
                        var Existphone = Model.customerPhons.FirstOrDefault(x => x.Id == phone.Id);
                        Existphone.Id = phone.Id;
                        Existphone.Phone = phone.Phone;
                        Existphone.PhoneType = phone.PhoneType;

                    }
                    else
                    {
                        Model.customerPhons.Add(phone);
                    }
                    StateHasChanged();
                }
            }
        }
    }

}
