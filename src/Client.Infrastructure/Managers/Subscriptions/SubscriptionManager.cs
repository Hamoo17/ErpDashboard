using ErpDashboard.Application.Features.Subscriptions.Queries.Dto;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ErpDashboard.Client.Infrastructure.Routes;
using ErpDashboard.Client.Infrastructure.Extensions;

namespace ErpDashboard.Client.Infrastructure.Managers.Subscriptions
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly HttpClient _httpClient;

        public SubscriptionManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<CustomerInfoDto>> GetSidByPhone(string Phone)
        {
            var Response = await _httpClient.GetAsync(SubscriptionsEndPoint.GetSidByPhone(Phone));
            return await Response.ToResult<CustomerInfoDto>();
        }
    }
}
