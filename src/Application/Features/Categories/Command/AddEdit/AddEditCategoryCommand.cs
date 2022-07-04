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

namespace ErpDashboard.Application.Features.Categories.Command.AddEdit
{
    public class AddEditCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    internal class AddEditCategoryCommandHandler : IRequestHandler<AddEditCategoryCommand , Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditCategoryCommandHandler> _localizer;
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _userService;
        public AddEditCategoryCommandHandler(IMapper mapper, IStringLocalizer<AddEditCategoryCommandHandler> localizer, ICustomIUnitOfWork<int> unitOfWork, ICurrentUserService userService)
        {
            _mapper = mapper;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<Result<int>> Handle(AddEditCategoryCommand command, CancellationToken cancellationToken)
        {
            var CompanyID = _userService.CompanyID;
            if (!CompanyID.HasValue)
            {
                return await Result<int>.FailAsync(_localizer["NO COMPANY WITH THIS ID"]);
            }
            var CatNameExist = await _unitOfWork.Repository<TbCategory>().Entities.AnyAsync(c => c.EnName == command.Name);
            if (!CatNameExist)
            {
                if (command.Id == 0)
                {
                    TbCategory cat = new TbCategory()
                    {
                        Id = command.Id,
                        EnName = command.Name,
                        ComId = _userService.CompanyID,
                        Active = true
                    };
                    await _unitOfWork.Repository<TbCategory>().AddAsync(cat);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(cat.Id, _localizer[" Category Saved"]);
                }
                else
                {
                    var CatExist = _unitOfWork.Repository<TbCategory>().Entities.FirstOrDefault(c => c.Id == command.Id);
                    if (CatExist != null)
                    {
                        CatExist.EnName = command.Name;
                        await _unitOfWork.Repository<TbCategory>().UpdateAsync(CatExist, command.Id);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync(CatExist.Id, _localizer[" Category Updated"]);
                    }
                    else
                    {
                        return await Result<int>.FailAsync(_localizer["Category Not Exist!"]);
                    }
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Category Not Exist!"]);
            }
        }
    }
}
