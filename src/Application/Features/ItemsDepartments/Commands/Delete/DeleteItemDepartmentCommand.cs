using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ErpDashboard.Application.Features.ItemsDepartments.Commands.Delete
{
    public class DeleteItemDepartmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteItemDepartmentCommandHandler : IRequestHandler<DeleteItemDepartmentCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteItemDepartmentCommand> _localizer;
        private readonly ICustomIUnitOfWork<int> _customIUnitOf;
        public DeleteItemDepartmentCommandHandler(ICustomIUnitOfWork<int> customIUnitOf , IStringLocalizer<DeleteItemDepartmentCommand> localizer)
        {

            _customIUnitOf = customIUnitOf;
            _localizer = localizer;

        }
        public async Task<Result<int>> Handle(DeleteItemDepartmentCommand command, CancellationToken cancellationToken)
        {
            var deptIsUsed = await _customIUnitOf.Repository<TbDepartment>().Entities.AnyAsync(c=>c.Id == command.Id);
            if(!deptIsUsed)
            {
                var dept = await _customIUnitOf.Repository<TbDepartment>().GetByIdAsync(command.Id);
                if(dept != null)
                {
                    await _customIUnitOf.Repository<TbDepartment>().DeleteAsync(dept);
                    await _customIUnitOf.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(command.Id, _localizer["Department Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync( _localizer["Department NotFound"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);

            }
        }
    }
}
