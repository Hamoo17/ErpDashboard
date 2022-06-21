using AutoMapper;
using ErpDashboard.Application.Features.ItemsDepartments.Commands.AddEdit;
using ErpDashboard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Mappings
{
    public class ItemDepartmentProfile : Profile
    {
        public ItemDepartmentProfile()
        {
            CreateMap<AddEditItemDepartmentCommand , TbDepartment>().ReverseMap();
            //CreateMap<itemdeparmentResponse, TbDepartment>().ReverseMap();

        }
    }
}
