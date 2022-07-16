using AutoMapper;
using ErpDashboard.Application.Features.Categories.Queries.GetAll;
using ErpDashboard.Application.Features.Items.Commands.AddEdit;
using ErpDashboard.Application.Features.Items.Quaries.Dto;
using ErpDashboard.Application.Features.MealsTypes.Commands.AddEdit;
using ErpDashboard.Application.Features.MealsTypes.Queries.Dto;
using ErpDashboard.Application.Features.Recipe.Queries.Dto;
using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Mappings
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<AddEditItemCommand, TbItem>().ReverseMap();
            CreateMap<GetItemResponse, TbItem>().ReverseMap();
            CreateMap<ItemComponentDetailResponse, TbItemComponentsLine>().ReverseMap();
            CreateMap<ItemComponentHdrResponse, TbItemComponentsHdr>().ReverseMap();
            CreateMap<CategoryResponse, TbCategory>().ReverseMap();
            CreateMap<AddEditMealsTypesCommand, TbMealsType>().ReverseMap();
            CreateMap<MealsTypesResponse, TbMealsType>().ReverseMap();

        }
    }
}
