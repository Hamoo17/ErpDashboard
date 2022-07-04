using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
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

namespace ErpDashboard.Application.Features.Units.Commands.AddEdit
{
    public class AddEditUnitsCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public int DefQty { get; set; }
    }
    internal class AddEditUnitsHandler : IRequestHandler<AddEditUnitsCommand, Result<int>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly IStringLocalizer<AddEditUnitsCommand> _localizer;
        private readonly IMapper _Mapper;
        public AddEditUnitsHandler(ICustomIUnitOfWork<int> unitOfWork, ICurrentUserService currentUser, IStringLocalizer<AddEditUnitsCommand> localizer, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _localizer = localizer;
            _Mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditUnitsCommand command, CancellationToken cancellationToken)
        {
            var CompanyID = _currentUser.CompanyID;
            if (!CompanyID.HasValue)
            {
                return await Result<int>.FailAsync(_localizer["NO COMPANY WITH THIS ID"]);
            }

            var UnitNameExist = await _unitOfWork.Repository<TbUnit>().Entities.AnyAsync(c => c.EnName == command.EnName);
            if (!UnitNameExist)
            {
                if (command.Id == 0)
                {

                    var UnitMapped = _Mapper.Map<TbUnit>(command);
                    UnitMapped.ComId = CompanyID;
                    await _unitOfWork.Repository<TbUnit>().AddAsync(UnitMapped);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(_localizer["Unit Added"]);
                }
                else
                {
                    var unit = await _unitOfWork.Repository<TbUnit>().GetByIdAsync(command.Id);
                    if (unit != null)
                    {
                        var UnitMapped = _Mapper.Map<TbUnit>(command);
                        await _unitOfWork.Repository<TbUnit>().UpdateAsync(UnitMapped , command.Id);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync(_localizer["Unit Updated"]);
                    }
                    else
                    {
                        return await Result<int>.FailAsync(_localizer["Unit Not Found"]);
                    }
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Unit Name Exist"]);
            }

        }
    }
}
