using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpDashboard.Application.Models;
using ErpDashboard.Application.Features.MealsCategory.Queries.Dto;
using ErpDashboard.Application.Features.MealsCategory.Commands.AddEdit;
using AutoMapper;

namespace ErpDashboard.Application.Mappings
{
    public class MealCategoryProfile : Profile
    {
        public MealCategoryProfile()
        {
            CreateMap<GetMealCategoryResponse,TbMealsCategory>().ReverseMap();
            CreateMap<AddEditMealsCategoryCommand,TbMealsCategory>().ReverseMap();
        }
    }
}
