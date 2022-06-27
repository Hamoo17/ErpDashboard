using AutoMapper;
using ErpDashboard.Application.Features.Items.Commands.AddEdit;
using ErpDashboard.Application.Features.Items.Quaries.Dto;
using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Mappings
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<AddEditItemCommand, TbItem>().ReverseMap();
            CreateMap<GetItemResponse, TbItem>().ReverseMap();

        }
    }
}
