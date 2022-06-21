using ErpDashboard.Application.Features.PlanCategory.Command.AddEdit;
using ErpDashboard.Application.Features.PlanCategory.Query.Dto;
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

namespace ErpDashboard.Client.Infrastructure.Managers.PlanCategory
{
    public class PlanCtegoryManager : IPlanCtegoryManager
    {
        private readonly HttpClient _httpClient;

        public PlanCtegoryManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
           var Response = await _httpClient.DeleteAsync(PlanCategoryEndPoints.Delete(id));
            return await Response.ToResult<int>();
        }

        public async Task<IResult<List<PlanCategoryDto>>> GetAllAsync()
        {
            var Response = await _httpClient.GetAsync(PlanCategoryEndPoints.GetAll);
            return await Response.ToResult<List<PlanCategoryDto>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditPlanCategoryCommand Command)
        {
            var Response = await _httpClient.PostAsJsonAsync(PlanCategoryEndPoints.Save,Command);
            return await Response.ToResult<int>();
        }
    }
}
