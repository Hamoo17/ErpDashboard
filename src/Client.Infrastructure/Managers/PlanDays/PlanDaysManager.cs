using ErpDashboard.Application.Features.PlanDays.Query.Dto;
using ErpDashboard.Client.Infrastructure.Extensions;
using ErpDashboard.Client.Infrastructure.Routes;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public async Task<IResult<List<PlanDayDto>>> GetPlanDaysAsync()
        {
            var Response = await _httpclient.GetAsync(PlanDayEndPoint.GetAll);
            return await Response.ToResult<List<PlanDayDto>>();
        }
    }
}
