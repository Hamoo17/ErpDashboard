using ErpDashboard.Application.Features.Dashboards.Queries.GetData;
using ErpDashboard.Shared.Wrapper;

namespace ErpDashboard.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}