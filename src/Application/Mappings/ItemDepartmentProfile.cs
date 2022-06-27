using AutoMapper;
using ErpDashboard.Application.Features.ItemsDepartments.Commands.AddEdit;
using ErpDashboard.Application.Features.ItemsDepartments.Queries.Dto;
using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Mappings
{
    public class ItemDepartmentProfile : Profile
    {
        public ItemDepartmentProfile()
        {
            CreateMap<AddEditItemDepartmentCommand, TbDepartment>().ReverseMap();
            CreateMap<GetItemDepartmentResponse, TbDepartment>().ReverseMap();

        }
    }
}
