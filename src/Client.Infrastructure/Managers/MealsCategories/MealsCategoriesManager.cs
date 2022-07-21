using ErpDashboard.Application.Features.MealsCategory.Commands.AddEdit;
using ErpDashboard.Application.Features.MealsCategory.Queries.Dto;
using ErpDashboard.Client.Infrastructure.Extensions;
using ErpDashboard.Client.Infrastructure.Routes;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.MealsCategories
{
    public class MealsCategoriesManager : IMealsCategoriesManager
    {
        private readonly HttpClient _httpClient;
        public MealsCategoriesManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(MealsCategoriesEndPoints.Delete(id));
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<MealsTypesResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(MealsCategoriesEndPoints.GetAll);
            return await response.ToResult<List<MealsTypesResponse>>();
        }

        public async Task<IResult<GetMealCategoryResponse>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(MealsCategoriesEndPoints.GetById(id));
            return await response.ToResult<GetMealCategoryResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditMealsCategoryCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync(MealsCategoriesEndPoints.Save ,command);
            return await response.ToResult<int>();
        }
    }
}
