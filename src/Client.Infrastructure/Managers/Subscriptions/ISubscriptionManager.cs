using ErpDashboard.Application.Features.Subscriptions.Queries.Dto;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.Subscriptions
{
    public interface ISubscriptionManager : IManager
    {
        Task<IResult<CustomerInfoDto>> GetSidByPhone(string Phone);

    }
}
