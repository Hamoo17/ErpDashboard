using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Interfaces.Repositories
{
    internal interface IItemDepartment
    {
        Task<bool> IsDepartmentExist(int dept_id);
        Task<bool> IsDepartmentUsed(int dept_id);


    }
}
