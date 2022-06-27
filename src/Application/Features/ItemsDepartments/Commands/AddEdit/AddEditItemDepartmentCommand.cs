using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

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
        private readonly ICurrentUserService _currentUserService;


        public AddEditItemDepartmentCommandHandler(ICustomIUnitOfWork<int> customIUnitOf, IMapper mapper, IStringLocalizer<AddEditItemDepartmentCommand> localizer, ICurrentUserService currentUserService)
        {
            _customIUnitOf = customIUnitOf;
            _mapper = mapper;
            _localizer = localizer;
            _currentUserService = currentUserService;
        }
        public async Task<Result<int>> Handle(AddEditItemDepartmentCommand command, CancellationToken cancellationToken)
        {
            var CompanyID = _currentUserService.CompanyID;
            if (!CompanyID.HasValue)
            {
                return await Result<int>.FailAsync(_localizer["NO COMPANY WITH THIS ID"]);
            }
            var IsDeptExist = await _customIUnitOf.Repository<TbDepartment>().Entities.AnyAsync(c => c.Name == command.Name);
            if (!IsDeptExist)
            {
                if (command.Id == 0)
                {
                    var ItemDept = _mapper.Map<TbDepartment>(command); // maping
                    ItemDept.ComId = CompanyID;
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
