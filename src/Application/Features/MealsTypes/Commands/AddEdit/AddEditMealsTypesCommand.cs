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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.MealsTypes.Commands.AddEdit
{
    public class AddEditMealsTypesCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
    internal class AddEditMealsTypesCommandHandelr : IRequestHandler<AddEditMealsTypesCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditMealsTypesCommandHandelr> _localizer;
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _userService;

        public AddEditMealsTypesCommandHandelr(IMapper mapper, IStringLocalizer<AddEditMealsTypesCommandHandelr> localizer, ICustomIUnitOfWork<int> unitOfWork, ICurrentUserService userService)
        {
            _mapper = mapper;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        public async Task<Result<int>> Handle(AddEditMealsTypesCommand Command, CancellationToken cancellationToken)
        {
            var CompanyID = _userService.CompanyID;
            if (!CompanyID.HasValue)
            {
                return await Result<int>.FailAsync(_localizer["NO COMPANY WITH THIS ID"]);
            }
            if (Command.Id == 0)
            {
                var MappedTypes = _mapper.Map<TbMealsType>(Command);
                MappedTypes.ComId = CompanyID;
                await _unitOfWork.Repository<TbMealsType>().AddAsync(MappedTypes);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(Command.Id, _localizer["Meal Types Saved"]);

            }
            else
            {

                var MealsTypesExist = await _unitOfWork.Repository<TbMealsType>().Entities.AnyAsync(c => c.Id == Command.Id);
                if (MealsTypesExist)
                {
                    var MappedTypes = _mapper.Map<TbMealsType>(MealsTypesExist);
                    await _unitOfWork.Repository<TbMealsType>().UpdateAsync(MappedTypes, Command.Id);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(Command.Id, _localizer["Meal Types Updated"]);
                }
                else
                {
                    return await Result<int>.SuccessAsync(Command.Id, _localizer["Meal Types dosnot Exist"]);
                }

            }
        }
    }
}
