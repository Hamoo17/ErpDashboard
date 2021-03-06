using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace ErpDashboard.Application.Features.MealsCategory.Commands.AddEdit
{
    public partial class AddEditMealsCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string EnName { get; set; }
        [Required]
        public string Symbol { get; set; }
        public bool IsSnack { get; set; }
    }

    internal class AddEditMealsCategoryCommandHandler : IRequestHandler<AddEditMealsCategoryCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditMealsCategoryCommandHandler> _localizer;
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _userService;
        public AddEditMealsCategoryCommandHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditMealsCategoryCommandHandler> localizer, ICurrentUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _userService = userService;
        }

        public async Task<Result<int>> Handle(AddEditMealsCategoryCommand command, CancellationToken cancellationToken)
        {
            var CompanyID = _userService.CompanyID;
            if (!CompanyID.HasValue)
            {
                return await Result<int>.FailAsync(_localizer["NO COMPANY WITH THIS ID"]);
            }
            if (command.Id == 0)
            {
                var NameExist = await _unitOfWork.Repository<TbMealsCategory>().Entities.AnyAsync(c=>c.EnName == command.EnName);
                if (!NameExist)
                {
                    TbMealsCategory MealCat = _mapper.Map<TbMealsCategory>(command);
                    await _unitOfWork.Repository<TbMealsCategory>().AddAsync(MealCat);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(MealCat.Id, _localizer["Meal Category Saved"]);
				}
                else
                {
                    return await Result<int>.FailAsync(_localizer["Category Name Exist"]);
                }
            }
            else
            {
                var MealCat = await _unitOfWork.Repository<TbMealsCategory>().GetByIdAsync(command.Id);
                if (MealCat != null)
                {
                    MealCat.EnName = command.EnName ?? MealCat.EnName;
                    MealCat.Symbol = command.Symbol ?? command.Symbol;
                    MealCat.Issnack = command.IsSnack;
                    await _unitOfWork.Repository<TbMealsCategory>().UpdateAsync(MealCat, MealCat.Id);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(MealCat.Id, _localizer["Meal Category Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Meal Category Not Found!"]);
                }
            }
        }
    }
}