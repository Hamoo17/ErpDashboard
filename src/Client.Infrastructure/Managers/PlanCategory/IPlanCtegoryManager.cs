using ErpDashboard.Application.Features.PlanCategory.Command.AddEdit;
using ErpDashboard.Application.Features.PlanCategory.Query.Dto;
using ErpDashboard.Shared.Wrapper;

namespace ErpDashboard.Client.Infrastructure.Managers.PlanCategory
{
    public interface IPlanCtegoryManager : IManager
    {
        Task<IResult<int>> SaveAsync(AddEditPlanCategoryCommand Command);
        Task<IResult<int>> DeleteAsync(int id);
        Task<IResult<List<PlanCategoryDto>>> GetAllAsync();
    }
}
