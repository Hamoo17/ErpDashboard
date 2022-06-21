using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ErpDashboard.Application.Features.ItemsDepartments.Commands.AddEdit
{
    public class AddEditItemDepartmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
    internal class AddEditItemDepartmentCommandHandler : IRequestHandler<AddEditItemDepartmentCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly ICustomIUnitOfWork<int> _customIUnitOf;
        private readonly IStringLocalizer<AddEditItemDepartmentCommand> _localizer;


        public AddEditItemDepartmentCommandHandler(ICustomIUnitOfWork<int> customIUnitOf, IMapper mapper,  IStringLocalizer<AddEditItemDepartmentCommand> localizer)
        {
            _customIUnitOf = customIUnitOf;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<Result<int>> Handle(AddEditItemDepartmentCommand command, CancellationToken cancellationToken)
        {
            var IsDeptExist= await _customIUnitOf.Repository<TbDepartment>().Entities.AnyAsync(c=>c.Name ==command.Name);
            if (!IsDeptExist)
            {
                if (command.Id == 0)
                {
                    var ItemDept = _mapper.Map<TbDepartment>(command); // maping
                    await _customIUnitOf.Repository<TbDepartment>().AddAsync(ItemDept); // add to db
                    await _customIUnitOf.Commit(cancellationToken); // commit
                    return await Result<int>.SuccessAsync(ItemDept.Id, _localizer["Department Saved"]); // message
                }
                else
                {
                    var ItemDept = await _customIUnitOf.Repository<TbDepartment>().GetByIdAsync(command.Id); // check
                    if (ItemDept != null)
                    {
                        ItemDept.Name = command.Name ?? ItemDept.Name; //asign
                        await _customIUnitOf.Repository<TbDepartment>().UpdateAsync(ItemDept, command.Id); //update
                        await _customIUnitOf.Commit(cancellationToken); // commit
                        return await Result<int>.SuccessAsync(ItemDept.Id, _localizer["Department Updated"]); // message
                    }
                    else
                    {
                        return await Result<int>.FailAsync(_localizer["Department Not Found!"]);
                    }
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["DepartMent Exist"]);
            }
        }
    }
}
