using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Application.Features.Products.Queries.GetAllPaged;
using ErpDashboard.Client.Infrastructure.Extensions;
using ErpDashboard.Client.Infrastructure.Routes;
using ErpDashboard.Shared.Wrapper;
using System.Net.Http;
using System.Net.Http.Json;

namespace ErpDashboard.Client.Infrastructure.Managers.Customers
{
    public class CustomersManager : ICustomersManager
    {
        private readonly HttpClient _HttpClient;

        public CustomersManager(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

		public async Task<PaginatedResult<GetAllCustomerViewModal>> GetAllAsync(GetAllCustomersQuery request)
        {
            var response = await _HttpClient.GetAsync(CustomersEndpoint.GetAll(request.PageNumber, request.PageSize, request.SearchString, request.OrderBy));
            return await response.ToPaginatedResult<GetAllCustomerViewModal>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditCustomerCommand Command)
        {
            var response = await _HttpClient.PostAsJsonAsync(CustomersEndpoint.Save, Command);
            return await response.ToResult<int>();
        }
    }
}
