using ErpDashboard.Application.Features.MealsTypes.Commands.AddEdit;
using ErpDashboard.Application.Features.MealsTypes.Queries.Dto;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.MealsTypes
{
    public interface IMealsTypesManager : IManager
    {
        Task<IResult<int>> SaveAsync(AddEditMealsTypesCommand command);
        Task<IResult<int>> DeleteAsync(int id);
        Task<IResult<MealsTypesResponse>> GetByIdAsync(int id);
        Task<IResult<List<MealsTypesResponse>>> GetAllAsync();
    }
}
