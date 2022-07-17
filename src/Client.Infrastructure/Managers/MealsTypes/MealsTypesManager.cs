using ErpDashboard.Application.Features.MealsTypes.Commands.AddEdit;
using ErpDashboard.Application.Features.MealsTypes.Queries.Dto;
using ErpDashboard.Client.Infrastructure.Routes;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpDashboard.Client.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.MealsTypes
{
    public class MealsTypesManager : IMealsTypesManager
    {
        private readonly HttpClient _httpClient;
        public MealsTypesManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var responce = await _httpClient.DeleteAsync(MealsTypesEndPoints.Delete(id));
            return await responce.ToResult<int>();
        }

        public async Task<IResult<List<MealsTypesResponse>>> GetAllAsync()
        {
            var responce = await _httpClient.GetAsync(MealsTypesEndPoints.GetAll);
            return await responce.ToResult<List<MealsTypesResponse>>();
        }

        public async Task<IResult<MealsTypesResponse>> GetByIdAsync(int id)
        {
            var responce = await _httpClient.GetAsync(MealsTypesEndPoints.GetById(id));
            return await responce.ToResult<MealsTypesResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditMealsTypesCommand command)
        {
            var responce = await _httpClient.PostAsJsonAsync(MealsTypesEndPoints.Save , command);
            return await responce.ToResult<int>();
        }
    }
}
