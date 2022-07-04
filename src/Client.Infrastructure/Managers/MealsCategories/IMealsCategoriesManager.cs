using ErpDashboard.Application.Features.MealsCategory.Commands.AddEdit;
using ErpDashboard.Application.Features.MealsCategory.Queries.Dto;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.MealsCategories
{
    public interface IMealsCategoriesManager : IManager
    {
         Task<IResult<int>> SaveAsync(AddEditMealsCategoryCommand command);
         Task<IResult<int>> DeleteAsync(int id);
         Task<IResult<GetMealCategoryResponse>> GetByIdAsync(int id);
         Task<IResult<List<GetMealCategoryResponse>>> GetAllAsync();

    }
}
