using AutoMapper;
using ErpDashboard.Application.Features.PlanDays.Command.AddEdit;
using ErpDashboard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Mappings
{
    public class PlanDayProfile:Profile
    {
        public PlanDayProfile()
        {
            CreateMap<AddEditPlanDaysCommand,TbPlanDay>().ReverseMap();
        }
    }
}
