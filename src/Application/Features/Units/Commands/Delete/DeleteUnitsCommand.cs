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

namespace ErpDashboard.Application.Features.Units.Commands.Delete
{
    public class DeleteUnitsCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteUnitsHandler : IRequestHandler<DeleteUnitsCommand, Result<int>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<DeleteUnitsHandler> _localizer;
        public DeleteUnitsHandler(ICustomIUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteUnitsHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
        public async Task<Result<int>> Handle(DeleteUnitsCommand command, CancellationToken cancellationToken)
        {
            var UnitUsed = await _unitOfWork.Repository<TbItem>().Entities
                .AnyAsync(c => c.ItemUnit1 == command.Id || c.ItemUnit2 == command.Id || c.ItemUnit3 == command.Id);
            if (!UnitUsed)
            {
                var unit = await _unitOfWork.Repository<TbUnit>().GetByIdAsync(command.Id);
                if (unit != null)
                {
                    await _unitOfWork.Repository<TbUnit>().DeleteAsync(unit);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(unit.Id, _localizer["Unit Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Unit Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Unit Used!"]);
            }
        }
    }
}
