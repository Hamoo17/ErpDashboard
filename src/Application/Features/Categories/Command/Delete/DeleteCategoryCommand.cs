using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Categories.Command.Delete
{
    public class DeleteCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteCategoryCommandHandler> _localizer;
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        public DeleteCategoryCommandHandler(IStringLocalizer<DeleteCategoryCommandHandler> localizer, ICustomIUnitOfWork<int> unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var CatUsed = await _unitOfWork.Repository<TbItem>().Entities.AnyAsync(c => c.CategoryId == request.Id);
            if (!CatUsed)
            {
                var cat = await _unitOfWork.Repository<TbCategory>().GetByIdAsync(request.Id);
                if (cat != null)
                {
                    await _unitOfWork.Repository<TbCategory>().DeleteAsync(cat);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(cat.Id, _localizer["Category Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Category Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}
