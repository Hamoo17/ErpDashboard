using AutoMapper;
using ErpDashboard.Application.Features.PlanDays.Command.AddEdit;
using ErpDashboard.Application.Features.PlanDays.Query.Dto;
using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Mappings
{
    public class PlanDayProfile : Profile
    {
        public PlanDayProfile()
        {
            CreateMap<AddEditPlanDaysCommand, TbPlanDay>().ReverseMap();
            CreateMap<PlanDayDto, TbPlanDay>().ReverseMap();
        }
    }
}
