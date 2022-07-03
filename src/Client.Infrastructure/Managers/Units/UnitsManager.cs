using ErpDashboard.Application.Features.Units.Commands.AddEdit;
using ErpDashboard.Application.Features.Units.Queries.Dto;
using ErpDashboard.Client.Infrastructure.Extensions;
using ErpDashboard.Client.Infrastructure.Routes;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.Units
{
    public class UnitsManager : IUnitsManager
    {
        private readonly HttpClient _httpClient;

        public UnitsManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var Response = await _httpClient.DeleteAsync(UnitsEndPoints.Delete(id));
            return await Response.ToResult<int>();
        }

        public async Task<IResult<List<UnitResponse>>> GetAllAsync()
        {
            var Response = await _httpClient.GetAsync(UnitsEndPoints.GetAll);
            return await Response.ToResult<List<UnitResponse>>();
        }

        public async Task<IResult<UnitResponse>> GetByIdAsync(int id)
        {
            var Response = await _httpClient.GetAsync(UnitsEndPoints.GetById(id));
            return await Response.ToResult<UnitResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditUnitsCommand Command)
        {
            var Response = await _httpClient.PostAsJsonAsync(UnitsEndPoints.Save, Command);
            return await Response.ToResult<int>();
        }
    }
}
