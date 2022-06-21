using ErpDashboard.Application.Features.PlanDays.Query.Dto;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.PlanDays
{
    public interface IPlanDaysManager:IManager
    {
         Task<IResult< List<PlanDayDto>>> GetPlanDaysAsync();
    }
}
