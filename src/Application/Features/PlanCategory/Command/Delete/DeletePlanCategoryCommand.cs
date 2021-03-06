using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;

namespace ErpDashboard.Application.Features.PlanCategory.Command.Delete
{
    public class DeletePlanCategoryCommand : IRequest<Result<int>>
    {
        public int ID { get; set; }

    }
    internal class DeletePlanCategoryCommandHandler : IRequestHandler<DeletePlanCategoryCommand, Result<int>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;

        public DeletePlanCategoryCommandHandler(ICustomIUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeletePlanCategoryCommand request, CancellationToken cancellationToken)
        {
            var plancategory = await _unitOfWork.Repository<TbPlanCategory>().GetByIdAsync(request.ID);
            if (plancategory != null)
            {
                await _unitOfWork.Repository<TbPlanCategory>().DeleteAsync(plancategory);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(request.ID, "Plancategory Removed");
            }
            else
                return await Result<int>.FailAsync("Plancategory Removed");
        }
    }
}
