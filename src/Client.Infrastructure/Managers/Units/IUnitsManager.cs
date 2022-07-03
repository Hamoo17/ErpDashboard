using ErpDashboard.Application.Features.Units.Commands.AddEdit;
using ErpDashboard.Application.Features.Units.Queries.Dto;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.Units
{
    public interface IUnitsManager : IManager
    {
        Task<IResult<int>> SaveAsync(AddEditUnitsCommand Command);
        Task<IResult<int>> DeleteAsync(int id);
        Task<IResult<List<UnitResponse>>> GetAllAsync();
        Task<IResult<UnitResponse>> GetByIdAsync(int id);
    }
}
