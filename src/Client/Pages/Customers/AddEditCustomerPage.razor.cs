using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.Quers.GetAllAreas;
using ErpDashboard.Application.Features.Customer.Quers.GetAllCustomers;
using ErpDashboard.Application.Models;
using ErpDashboard.Client.Infrastructure.Managers.Customers;
using ErpDashboard.Client.Infrastructure.Mappings;
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
        private List<GetAllAreaViewModal> AreasList { get; set; } = new();

        private async Task Save()
        {
            if (Model.customerAdresses == null || Model.customerAdresses.Count == 0)
            {
                _snackBar.Add("You Must Add One Customer Adress At Lest", Severity.Warning);
                return;
            }

            if (Model.customerPhons==null|| Model.customerPhons.Count==0)
            {
                _snackBar.Add("You Must Add One Customer Phons At Lest", Severity.Warning);
                return;
            }
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
        protected override async Task OnInitializedAsync()
        {
            var response = await _CustomerManager.GetAllAreasAsync();
            if (response.Succeeded)
            {
                AreasList = response.Data;
            }
           
        }
        private string GetAreaName(int id) 
        {
            return AreasList.FirstOrDefault(x => x.Id == id)?.Name;
        }
        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task InvokeModal(int id =0 , int Customerid =0)
        {
          

            var parameters = new DialogParameters();
            if (id != 0)
            {
                var phone = Model.customerPhons.FirstOrDefault(x => x.Id == id);
                if (phone != null)
                {
                    parameters.Add(nameof(AddEditCustomerPhons.Model), new CustomerPhoneClientDto
                    {
                        Id = phone.Id,
                        Phone = phone.Phone,
                        PhoneType = phone.PhoneType,
                        CustomerId = phone.CustomerId
                    });
                }
                else
                {
                    parameters.Add(nameof(AddEditCustomerPhons.Model), new CustomerPhoneClientDto
                    {
                        CustomerId = Customerid
                    });
                }
            }

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCustomerPhons>(id == 0 ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                //  Reset();
                var phone = result.Data as CustomerPhoneClientDto;
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
                        if (Model.customerPhons == null)
                        {
                            Model.customerPhons = new();
                        }
                        var mapperPhone = _CustomerManager.GetPhoneDto(phone);
                        Model.customerPhons.Add(mapperPhone);
                    }
                    StateHasChanged();
                }
            }
        }

        private async Task InvokeAdressModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                var Adress = Model.customerAdresses.FirstOrDefault(x => x.Id == id);
                if (Adress != null)
                {
                    parameters.Add(nameof(AddEditCustomerAdressPage.Model), new AdressDto
                    {
                        Id = Adress.Id,
                        Adress = Adress.Adress,
                        AreaId = Adress.AreaId,
                    });
                }
            }

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCustomerAdressPage>(id == 0 ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                //  Reset();
                var Adress = result.Data as AdressDto;
                if (Adress != null)
                {
                    if (Adress.Id != 0)
                    {
                        var ExistAdress = Model.customerAdresses.FirstOrDefault(x => x.Id == Adress.Id);
                        ExistAdress.Id = Adress.Id;
                        ExistAdress.Adress = Adress.Adress;
                        ExistAdress.AreaId = Adress.AreaId;

                    }
                    else
                    {
                        if (Model.customerAdresses == null)
                        {
                            Model.customerAdresses = new();
                        }
                        Model.customerAdresses.Add(Adress);
                    }
                    StateHasChanged();
                }
            }
        }

        private void DeleteAdress(int id)
        {
            var deletedAdress=Model.customerAdresses.FirstOrDefault(x=>x.Id==id);
            Model.customerAdresses.Remove(deletedAdress);
            StateHasChanged();
        }


    }

}
