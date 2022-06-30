using AutoMapper;
using ErpDashboard.Application.Features.PlanCategory.Command.AddEdit;
using ErpDashboard.Application.Features.PlanCategory.Query.Dto;
using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Mappings
{
    public class PlanCategoryProfile : Profile
    {
        public PlanCategoryProfile()
        {
            CreateMap<AddEditPlanCategoryCommand, TbPlanCategory>().ReverseMap();
            CreateMap<customercategoryviewmodel, TbPlanCategory>().ReverseMap();
        }
    }
}
