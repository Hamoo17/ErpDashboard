using AutoMapper;
using ErpDashboard.Application.Features.PlanCategory.Command.AddEdit;
using ErpDashboard.Application.Features.PlanCategory.Query.Dto;
using ErpDashboard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Mappings
{
    public class PlanCategoryProfile :Profile
    {
        public PlanCategoryProfile()
        {
            CreateMap<AddEditPlanCategoryCommand, TbPlanCategory>().ReverseMap();
            CreateMap<PlanCategoryDto, TbPlanCategory>().ReverseMap();
        }
    }
}
