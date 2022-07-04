using ErpDashboard.Application.Features.Categories.Command.AddEdit;
using ErpDashboard.Application.Features.Categories.Queries.GetAll;
using ErpDashboard.Client.Infrastructure.Extensions;
using ErpDashboard.Client.Infrastructure.Routes;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.Categories
{
    public class CategoriesManager : ICategoriesManager
    {
        private readonly HttpClient _httpClient;
        public CategoriesManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(CategoriesEndPoints.Delete(id));
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<CategoryResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(CategoriesEndPoints.GetAll);
            return await response.ToResult<List<CategoryResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditCategoryCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync(CategoriesEndPoints.Save, command);
            return await response.ToResult<int>();

        }
    }
}
