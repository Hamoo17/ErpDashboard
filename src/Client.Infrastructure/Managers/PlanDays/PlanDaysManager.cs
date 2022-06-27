using ErpDashboard.Application.Features.PlanDays.Command.AddEdit;
using ErpDashboard.Application.Features.PlanDays.Query.Dto;
using ErpDashboard.Client.Infrastructure.Extensions;
using ErpDashboard.Client.Infrastructure.Routes;
using ErpDashboard.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.PlanDays
{
    public class PlanDaysManager : IPlanDaysManager
    {
        private readonly HttpClient _httpclient;
        public PlanDaysManager(HttpClient httpclient)
        {
            _httpclient = httpclient;
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var Response = await _httpclient.DeleteAsync(PlanDayEndPoint.Delete(id));
            return await Response.ToResult<int>();

        }

        public async Task<IResult<List<PlanDayDto>>> GetAllAsync()
        {
            var Response = await _httpclient.GetAsync(PlanDayEndPoint.GetAll);
            return await Response.ToResult<List<PlanDayDto>>();
        }


        public async Task<IResult<int>> SaveAsync(AddEditPlanDaysCommand Command)
        {
            var Response = await _httpclient.PostAsJsonAsync(PlanDayEndPoint.Save, Command);
            return await Response.ToResult<int>();

        }
    }
}
