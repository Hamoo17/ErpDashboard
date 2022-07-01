using AutoMapper;
using ErpDashboard.Application.Features.Units.Commands.AddEdit;
using ErpDashboard.Application.Features.Units.Queries.Dto;
using ErpDashboard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Mappings
{
    public class UnitsProfile : Profile
    {
        public UnitsProfile()
        {
            CreateMap<AddEditUnitsCommand, TbUnit>().ReverseMap();
            CreateMap<UnitResponse, TbUnit>().ReverseMap();
        }
    }
}
