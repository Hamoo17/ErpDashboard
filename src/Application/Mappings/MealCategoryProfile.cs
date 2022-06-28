using AutoMapper;
using ErpDashboard.Application.Features.MealsCategory.Commands.AddEdit;
using ErpDashboard.Application.Features.MealsCategory.Queries.Dto;
using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Mappings
{
    public class MealCategoryProfile : Profile
    {
        public MealCategoryProfile()
        {
            CreateMap<GetMealCategoryResponse, TbMealsCategory>().ReverseMap();
            CreateMap<AddEditMealsCategoryCommand, TbMealsCategory>().ReverseMap();
        }
    }
}
