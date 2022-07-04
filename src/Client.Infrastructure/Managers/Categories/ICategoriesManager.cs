using ErpDashboard.Application.Features.Categories.Command.AddEdit;
using ErpDashboard.Application.Features.Categories.Queries.GetAll;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.Categories
{

    public interface ICategoriesManager : IManager
    {
        public Task<IResult<int>> SaveAsync(AddEditCategoryCommand command);
        public Task<IResult<int>> DeleteAsync(int id);

        public Task<IResult<List<CategoryResponse>>> GetAllAsync();
    }
}
