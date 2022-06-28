using ErpDashboard.Application.Features.PlanDays.Command.AddEdit;
using ErpDashboard.Application.Features.PlanDays.Query.Dto;
using ErpDashboard.Shared.Wrapper;

namespace ErpDashboard.Client.Infrastructure.Managers.PlanDays
{
    public interface IPlanDaysManager : IManager
    {
        Task<IResult<int>> SaveAsync(AddEditPlanDaysCommand Command);
        Task<IResult<int>> DeleteAsync(int id);
        Task<IResult<List<PlanDayDto>>> GetAllAsync();

    }
}
