using ErpDashboard.Application.Features.Subscriptions.Queries.Dto;
using ErpDashboard.Shared.Wrapper;

namespace ErpDashboard.Client.Infrastructure.Managers.Subscriptions
{
    public interface ISubscriptionManager : IManager
    {
        Task<IResult<CustomerInfoDto>> GetSidByPhone(string Phone);

    }
}
