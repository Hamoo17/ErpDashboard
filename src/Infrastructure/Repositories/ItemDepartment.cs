using ErpDashboard.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Infrastructure.Repositories
{
    public class ItemDepartment : IItemDepartment
    {
        public Task<bool> IsDepartmentExist(int dept_id)
        {
           return Task.FromResult(false);
        }

        public Task<bool> IsDepartmentUsed(int dept_id)
        {
            return Task.FromResult(false);
        }
    }
}
