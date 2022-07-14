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

namespace ErpDashboard.Application.Features.MealsTypes.Commands.Delete
{
    public class DeleteMealsTypesCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteMealsTypesCommandHandler : IRequestHandler<DeleteMealsTypesCommand , Result<int>>
    {
        private readonly IStringLocalizer<DeleteMealsTypesCommandHandler> _localizer;
        private readonly ICustomIUnitOfWork<int> _unitOfWork;

        public DeleteMealsTypesCommandHandler(IStringLocalizer<DeleteMealsTypesCommandHandler> localizer, ICustomIUnitOfWork<int> unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteMealsTypesCommand command, CancellationToken cancellationToken)
        {
            var TypeUsed = await _unitOfWork.Repository<TbMealsType>().Entities.AnyAsync(c=>c.Id == command.Id);
            if (!TypeUsed)
            {
                var ItemToDelete = await _unitOfWork.Repository<TbMealsType>().GetByIdAsync(command.Id);
                if (ItemToDelete != null)
                {
                   await _unitOfWork.Repository<TbMealsType>().DeleteAsync(ItemToDelete);
                   await _unitOfWork.Commit(cancellationToken);
                   return await Result<int>.SuccessAsync(command.Id, _localizer["Meal Types Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Meal Types Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}
