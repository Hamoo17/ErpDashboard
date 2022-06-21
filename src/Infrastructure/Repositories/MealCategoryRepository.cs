using ErpDashboard.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Infrastructure.Repositories
{
    public class MealCategoryRepository : IMealCategoryRepository
    {
        public Task<bool> IsMealCatUsed(int MealCatId)
        {
            return Task.FromResult(false);
        }
    }
}
