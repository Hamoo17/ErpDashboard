using AutoMapper;
using ErpDashboard.Application.Features.Items.Commands.AddEdit;
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

namespace ErpDashboard.Application.Features.Items.Commands.Delete
{
    public class DeleteItemCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

}
    internal class DeleteItemHandler : IRequestHandler<DeleteItemCommand, Result<int>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<DeleteItemHandler> _localizer;
        public DeleteItemHandler(ICustomIUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteItemHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteItemCommand command, CancellationToken cancellationToken)
        {
            var IsItemUsed = await _unitOfWork.Repository<TbItem>().Entities.AllAsync(c => c.Id == command.Id);
            if (!IsItemUsed)
            {
                var item = await _unitOfWork.Repository<TbItem>().GetByIdAsync(command.Id);
                if (item != null)
                {
                    await _unitOfWork.Repository<TbItem>().DeleteAsync(item);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(item.Id, _localizer["Item Deleted"]);
                }

                else
                {
                    return await Result<int>.FailAsync(_localizer["Item Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}
