using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Client.Infrastructure.Extensions;
using ErpDashboard.Client.Infrastructure.Routes;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.Customers
{
    public class CustomersManager : ICustomersManager
    {
        private readonly HttpClient _HttpClient;

        public CustomersManager(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public async Task<IResult<List<GetAllCustomerViewModal>>> GetAllAsync()
        {
            var Response = await _HttpClient.GetAsync(CustomersEndpoint.GetAll);
            return await Response.ToResult <List<GetAllCustomerViewModal>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditCustomerCommand Command)
        {
            var response = await _HttpClient.PostAsJsonAsync(CustomersEndpoint.Save, Command);
            return   await response.ToResult<int>();
        }
    }
}
