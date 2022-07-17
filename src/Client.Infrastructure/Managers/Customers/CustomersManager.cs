using AutoMapper;
using ErpDashboard.Application.Features.Customer.Command.AddEdit;
using ErpDashboard.Application.Features.Customer.GetAllCustomers;
using ErpDashboard.Application.Features.Customer.Quers.GetAllAreas;
using ErpDashboard.Application.Features.Customer.Quers.GetAllCustomerCategory;
using ErpDashboard.Application.Features.Customer.Quers.GetAllCustomers;
using ErpDashboard.Application.Features.PlanCategory.Query.Dto;
using ErpDashboard.Application.Features.Products.Queries.GetAllPaged;
using ErpDashboard.Client.Infrastructure.Extensions;
using ErpDashboard.Client.Infrastructure.Mappings;
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

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var Response = await _HttpClient.DeleteAsync(CustomersEndpoint.Delete(id));
            return await Response.ToResult<int>();

        }

        public async Task<IResult<List<GetAllAreaViewModal>>> GetAllAreasAsync()
        {
            var response = await _HttpClient.GetAsync(CustomersEndpoint.GetAreas);
            return await response.ToResult<List<GetAllAreaViewModal>>();
        }

        public async Task<PaginatedResult<GetAllCustomerViewModal>> GetAllAsync(GetAllCustomersQuery request)
        {
            var response = await _HttpClient.GetAsync(CustomersEndpoint.GetAll(request.PageNumber, request.PageSize, request.SearchString, request.OrderBy));
            return await response.ToPaginatedResult<GetAllCustomerViewModal>();
        }

        public async Task<IResult<List<GetAllCustomerCategoryViewModel>>> GetAllCustomerCategoryAsync()
        {
            var response = await _HttpClient.GetAsync(CustomersEndpoint.GetCustomerCategory);
            return await response.ToResult<List<GetAllCustomerCategoryViewModel>>();
        }

        public PhonsDto GetPhoneDto(CustomerPhoneClientDto ClientDto)
        {
            return new PhonsDto() { Id = ClientDto.Id, Phone = ClientDto.Phone, PhoneType = ClientDto.PhoneType, CustomerId = ClientDto.CustomerId };
           
        }

        public async Task<bool> IsPhoneExist(int CustomerId, string Phone)
        {
            var response = await _HttpClient.GetAsync(CustomersEndpoint.isPhoneExist(CustomerId, Phone));
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditCustomerCommand Command)
        {
            var response = await _HttpClient.PostAsJsonAsync(CustomersEndpoint.Save, Command);
            return await response.ToResult<int>();
        }

    }
}
